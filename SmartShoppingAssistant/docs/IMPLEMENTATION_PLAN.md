# Plan de Implementare — SmartShoppingAssistant

## Cuprins

0. [Arhitectura agenților (multi-agent)](#0-arhitectura-agenților-multi-agent)
1. [Status — ce este gata și ce lipsește](#1-status--ce-este-gata-și-ce-lipsește)
2. [Secretele API — User Secrets via Visual Studio](#2-secretele-api--user-secrets-via-visual-studio)
3. [Fix Program.cs — înregistrări lipsă](#3-fix-programcs--înregistrări-lipsă)
4. [Seeding Promotions](#4-seeding-promotions)
5. [AnalysisController — endpoint nou](#5-analysiscontroller--endpoint-nou)
6. [Referință — structuri deja implementate](#6-referință--structuri-deja-implementate)

---

## 0. Arhitectura agenților (multi-agent)

### Diagrama fluxului

```
                    ┌─────────────┐
                    │    Cart     │ (JSON)
                    └──────┬──────┘
                           │
                    ┌──────▼──────────────────┐
                    │  PromotionCheckerAgent   │
                    │  tool: GetPromotions     │──► IPromotionService.GetForProductAsync()
                    │       ForProduct()       │
                    └──────┬──────────────────┘
                           │ PromotionAnalysis (JSON)
                           │
         ┌─────────────────▼──────────────────────┐
         │          SuggestionComposerAgent        │
Cart ───►│  tool: GetProductsByCategory()          │──► IProductService.GetAllAsync(categoryId)
Cat. ───►│  input: cart + categories + analysis    │
         └─────────────────┬──────────────────────┘
                           │ SuggestionResponse (JSON)
                    ┌──────▼──────┐
                    │  Analysis   │
                    │  Controller │──► GET /api/analysis
                    └─────────────┘
```

### Pattern comun — `ChatClientAgent`

Ambii agenți folosesc **același tipar**.

#### De ce `Build(string json)` în loc de `AnalyzeAsync()`?

Agentul **nu știe** de unde vin datele. El primește context gata serializat ca string JSON.
- Agentul este **stateless** — poate fi refolosit pentru orice coș, nu doar cel curent
- Controller-ul decide **ce date** trimite și **când** apelează fiecare agent
- Agentul devine ușor de **testat** — îi dai un JSON hardcodat și verifici output-ul

```
Controller                  Agent
─────────                   ─────
1. Preia datele
   (cart, categories)
2. Serializează → JSON ──► Build(json) → ChatClientAgent
3. Apelează InvokeAsync()        ↳ construiește ChatOptions cu:
4. Primește text JSON                - Instructions (system prompt + datele primite)
5. Deserializează                    - ResponseFormat (schema tipului T)
   în obiect C#                      - Tools (funcții pe care LLM-ul le poate apela)
```

#### Cum funcționează un Tool (`AIFunctionFactory.Create`)?

```
LLM decide: "Am nevoie de promoțiile pentru productId=3"
    ↓
Apelează tool-ul "GetPromotionsForProduct" cu parametrul productId=3
    ↓
Lambda-ul din AIFunctionFactory.Create() rulează:
    ShoppingTools.GetPromotionsForProduct(3, promotionService)
    ↓
Serviciul face query în BD → returnează List<PromotionGetDTO>
    ↓
Rezultatul este injectat înapoi în contextul LLM-ului
    ↓
LLM continuă analiza cu datele reale din BD
```

#### De ce `ResponseFormat = ChatResponseFormat.ForJsonSchema<T>()`?

Fără el, LLM-ul poate returna text liber: "Here are the promotions...".
Cu el, LLM-ul este **forțat** să returneze JSON valid conform schemei clasei `T`.
Schema e generată automat din proprietățile C# și atributele `[JsonPropertyName]`.

#### De ce `UseFunctionInvocation()` în Program.cs?

```csharp
builder.Services.AddSingleton<IChatClient>(
    new OpenAIClient(openAiApiKey)
        .GetChatClient(openAiModel)
        .AsIChatClient()
        .AsBuilder()
        .UseFunctionInvocation()  // <-- IMPORTANT
        .Build());
```

Fără `UseFunctionInvocation()`, LLM-ul returnează un mesaj "tool_call" și **așteaptă**
să fie executat manual. Middleware-ul interceptează aceste cereri și le execută automat
în buclă până când LLM-ul returnează JSON-ul final.

---

## 1. Status — ce este gata și ce lipsește

### ✅ Gata — nu trebuie modificat

| Fișier | Observație |
|---|---|
| `Agents/PromotionCheckerAgent.cs` | Identic cu labs-ul oficial |
| `Agents/IPromotionCheckerAgent.cs` | Identic cu labs-ul oficial |
| `Agents/SuggestionComposerAgent.cs` | Implementat — același pattern ca Agentul 1 |
| `Agents/ISuggestionComposerAgent.cs` | Implementat |
| `Models/PromotionAnalysis.cs` | Identic — Deal + PromotionAnalysis cu JsonPropertyName |
| `Models/SuggestionResponse.cs` | Implementat — ProductSuggestion cu JsonPropertyName |
| `Tools/ShoppingTools.cs` | Ambele metode implementate |
| `DataAccess/Repositories/PromotionRepository.cs` | GetForProductAsync implementat corect |
| `Services/PromotionService.GetForProductAsync` | Delegă la repository |
| `Program.cs` — IChatClient | OpenAI + UseFunctionInvocation configurat |
| Toate CRUD controllers | Cart, Product, Category, Promotion |
| `DataAccess/Seed/CategorySeed.cs` | 3 categorii seed-uite |
| `DataAccess/Seed/ProductSeed.cs` | 4 produse seed-uite |
| `DataAccess/Seed/ProductCategorySeed.cs` | Relații seed-uite |

### ❌ Lipsește — de implementat

| # | Ce lipsește | Fișier | Prioritate |
|---|---|---|---|
| 1 | User Secrets — cheia OpenAI nu e configurată | `secrets.json` via VS | **Critic** — app crasha |
| 2 | `IPromotionRepository` nu e înregistrat în DI | `Program.cs` | **Critic** — app crasha |
| 3 | Agenții nu sunt înregistrați în DI | `Program.cs` | **Critic** |
| 4 | `PromotionSeed.cs` nu există | `DataAccess/Seed/` | Demo fără promoții |
| 5 | `AnalysisController` nu există | `Api/Controllers/` | Endpoint principal |

---

## 2. Secretele API — User Secrets via Visual Studio

**User Secrets** = echivalentul `.env` în .NET. Cheile stau în afara repo-ului,
pe mașina ta locală, niciodată în git.

### Unde se stochează

Windows: `%APPDATA%\Microsoft\UserSecrets\<guid>\secrets.json`

Conținut `secrets.json`:
```json
{
  "OpenAI:ApiKey": "sk-...",
  "OpenAI:ModelId": "gpt-4o"
}
```

Același format ca `appsettings.json` — .NET le suprapune automat la startup în Development.

### Cum deschizi fișierul din Visual Studio

**Solution Explorer → click dreapta pe `SmartShoppingAssistant.Api` → „Manage User Secrets"**

Visual Studio face automat două lucruri:
1. Adaugă `<UserSecretsId>guid</UserSecretsId>` în `.csproj`
2. Deschide `secrets.json` pentru editare directă

Adaugi cele două chei și salvezi. Nu apare în git, nu apare în repo.

### De ce `dotnet ef database update` crasha fără cheie

`dotnet ef` pornește `Program.cs` pentru a construi contextul EF. Linia ta:
```csharp
?? throw new InvalidOperationException("OpenAI API key is not configured.");
```
aruncă excepție înainte să ajungă la `AddDbContext`. Odată cu User Secrets configurate,
migrarea rulează normal.

---

## 3. Fix Program.cs — înregistrări lipsă

**Fișier:** `SmartShoppingAssistant.Api/Program.cs`

### Problema curentă

```csharp
// linia 33 — doar interfața generică; PromotionService cere IPromotionRepository concret
builder.Services.AddScoped<IRepository<Promotion>, BaseRepository<Promotion>>();

// linia 56 — comentat, agenții nu sunt disponibili în DI
//builder.Services.AddScoped<IPromotionCheckerAgent, PromotionCheckerAgent>();
```

### Fix — adaugă după înregistrările existente

```csharp
// Fix 1: PromotionService primește IPromotionRepository prin constructor, nu IRepository<Promotion>
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();

// Fix 2: înregistrare agenți prin interfețele lor
builder.Services.AddScoped<IPromotionCheckerAgent, PromotionCheckerAgent>();
builder.Services.AddScoped<ISuggestionComposerAgent, SuggestionComposerAgent>();
```

> **Nu șterge** linia cu `IRepository<Promotion>` — `CartService` poate depinde de ea.
> **De ce prin interfață?** `AnalysisController` cere `IPromotionCheckerAgent` în constructor.
> DI rezolvă implementarea concretă (`PromotionCheckerAgent`) pentru că ai mapat-o la interfață.

---

## 4. Seeding Promotions

**Fișiere:** `DataAccess/Seed/PromotionSeed.cs` (nou) + `SmartShoppingAssistantDbContext.cs` (modificat)

Seeding = date inițiale introduse în BD printr-o **migrare EF Core**.
Necesare ca agentul să aibă promoții reale de verificat la demo.

ID-urile referite sunt din seeding-ul existent:
- Products: `1`=Coca Cola, `2`=Pepsi, `3`=Lay's, `4`=Iaurt
- Categories: `1`=Băuturi, `2`=Snacks, `3`=Lactate

### 4a. Fișier nou: `DataAccess/Seed/PromotionSeed.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Entities.Enums;

namespace SmartShoppingAssistant.DataAccess.Seed
{
    public static class PromotionSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Promotion>().HasData(
                new Promotion
                {
                    Id = 1,
                    Name = "Cumpără 5 Coca-Cola, primești 1 gratis",
                    Type = PromotionType.Quantity,
                    Threshold = 5,
                    Reward = PromotionReward.FreeItems,
                    RewardValue = 1,
                    ProductId = 1,
                    CategoryId = null,
                    IsActive = true
                },
                new Promotion
                {
                    Id = 2,
                    Name = "10% reducere la comenzi peste 100 RON",
                    Type = PromotionType.CartTotal,
                    Threshold = 100.00m,
                    Reward = PromotionReward.PercentDiscount,
                    RewardValue = 10,
                    ProductId = null,
                    CategoryId = null,
                    IsActive = true
                },
                new Promotion
                {
                    Id = 3,
                    Name = "Cumpără 3 Snacks-uri, primești 1 gratis",
                    Type = PromotionType.Quantity,
                    Threshold = 3,
                    Reward = PromotionReward.FreeItems,
                    RewardValue = 1,
                    ProductId = null,
                    CategoryId = 2,
                    IsActive = true
                }
            );
        }
    }
}
```

### 4b. Adaugă în `SmartShoppingAssistantDbContext.cs` — `OnModelCreating`

```csharp
CategorySeed.Seed(modelBuilder);
ProductSeed.Seed(modelBuilder);
ProductCategorySeed.Seed(modelBuilder);
PromotionSeed.Seed(modelBuilder);   // ← adaugă asta
```

### 4c. Migrare EF Core

**Necesită User Secrets configurate (pasul 2) înainte de rulare.**

```bash
# din folderul SmartShoppingAssistant.Api/
dotnet ef migrations add PromotionSeed --project ../SmartShoppingAssistant.DataAccess
dotnet ef database update
```

---

## 5. AnalysisController — endpoint nou

**Fișier nou:** `SmartShoppingAssistant.Api/Controllers/AnalysisController.cs`

Controller-ul **orchestrează** cei doi agenți în secvență și returnează rezultatul combinat.
Serializarea/deserializarea JSON este explicită — agenții returnează text, nu obiecte C#.

```csharp
using Microsoft.AspNetCore.Mvc;
using SmartShoppingAssistant.BusinessLogic.Agents;
using SmartShoppingAssistant.BusinessLogic.Models;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using System.Text.Json;

namespace SmartShoppingAssistant.Api.Controllers
{
    [ApiController]
    [Route("api/analysis")]
    public class AnalysisController(
        IPromotionCheckerAgent promotionCheckerAgent,
        ISuggestionComposerAgent suggestionComposerAgent,
        ICartService cartService,
        ICategoryService categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Analyze(CancellationToken cancellationToken)
        {
            // 1. Preia datele necesare
            var cart = await cartService.GetCartAsync();
            var categories = await categoryService.GetAllAsync();

            var cartJson = JsonSerializer.Serialize(cart);
            var categoriesJson = JsonSerializer.Serialize(categories);

            // 2. Agent 1 — verifică promoțiile
            var checkerAgent = promotionCheckerAgent.Build(cartJson);
            var checkerResult = await checkerAgent.InvokeAsync(
                "Analyze the cart promotions.", cancellationToken);
            var promotionAnalysisJson = checkerResult.Messages.Last().Text;
            var promotionAnalysis = JsonSerializer.Deserialize<PromotionAnalysis>(promotionAnalysisJson)!;

            // 3. Agent 2 — compune sugestiile
            var composerAgent = suggestionComposerAgent.Build(cartJson, categoriesJson, promotionAnalysisJson);
            var composerResult = await composerAgent.InvokeAsync(
                "Suggest products for the cart.", cancellationToken);
            var suggestions = JsonSerializer.Deserialize<SuggestionResponse>(composerResult.Messages.Last().Text)!;

            return Ok(new { promotionAnalysis, suggestions });
        }
    }
}
```

### Response shape

```json
{
  "promotionAnalysis": {
    "activeDeals": [
      { "promotionId": 1, "productId": 1, "description": "Buy 5 Coca-Cola get 1 free", "action": null, "savings": 5.99 }
    ],
    "nearMissDeals": [
      { "promotionId": 2, "productId": 0, "description": "10% off orders over 100 RON", "action": "Add 15 RON more to unlock 10% discount", "savings": null }
    ]
  },
  "suggestions": {
    "suggestions": [
      { "productId": 2, "productName": "Pepsi 500ml", "price": 5.49, "category": "Băuturi", "reason": "Adds to cart total, helping unlock the 100 RON discount" }
    ]
  }
}
```

---

## 6. Referință — structuri deja implementate

### PromotionCheckerAgent (nu modifica)

```csharp
public class PromotionCheckerAgent(IChatClient chatClient, IPromotionService promotionService)
    : IPromotionCheckerAgent
{
    public ChatClientAgent Build(string cartJson)
    {
        return new ChatClientAgent(chatClient,
            new ChatClientAgentOptions
            {
                Name = "PromotionChecker",
                Description = "Checks promotions for cart items",
                ChatOptions = new ChatOptions
                {
                    Instructions = $"""
                        You check promotions. Here is the current cart:
                        {cartJson}
                        1. Call GetPromotionsForProduct for each product in the cart.
                        2. Compare each promotion's rules against the cart quantities/totals.
                        3. For near-miss deals, calculate the savings the user would get.
                        """,
                    ResponseFormat = ChatResponseFormat.ForJsonSchema<PromotionAnalysis>(),
                    Tools =
                    [
                        AIFunctionFactory.Create(
                            ([Description("The product ID to check")] int productId) =>
                                ShoppingTools.GetPromotionsForProduct(productId, promotionService),
                            "GetPromotionsForProduct",
                            "Get all active promotions that apply to a specific product."
                        )
                    ]
                }
            },
            null!, null!);
    }
}
```

### SuggestionComposerAgent (nu modifica)

```csharp
public class SuggestionComposerAgent(IChatClient chatClient, IProductService productService)
    : ISuggestionComposerAgent
{
    public ChatClientAgent Build(string cartJson, string categoriesJson, string promotionAnalysisJson)
    {
        return new ChatClientAgent(chatClient,
            new ChatClientAgentOptions
            {
                Name = "SuggestionComposer",
                Description = "Composes product suggestions based on the cart, categories and promotion analysis",
                ChatOptions = new ChatOptions
                {
                    Instructions = $"""
                        You are a smart shopping suggestion agent.
                        Here is the current cart:
                        {cartJson}

                        Here is the promotion analysis (active deals and near-miss deals):
                        {promotionAnalysisJson}

                        Here are the available categories:
                        {categoriesJson}

                        1. Call GetProductsByCategory for each category present in the cart to find relevant products.
                        2. Prioritize products that unlock a near-miss deal.
                        3. Also suggest products from the same categories as cart items.
                        4. Return at most 5 suggestions.
                        """,
                    ResponseFormat = ChatResponseFormat.ForJsonSchema<SuggestionResponse>(),
                    Tools =
                    [
                        AIFunctionFactory.Create(
                            ([Description("The category ID to get products for")] int categoryId) =>
                                ShoppingTools.GetProductsByCategory(categoryId, productService),
                            "GetProductsByCategory",
                            "Get all available products that belong to a specific category."
                        )
                    ]
                }
            },
            null!, null!);
    }
}
```

### ShoppingTools (nu modifica)

```csharp
public static class ShoppingTools
{
    // Folosit de PromotionCheckerAgent
    public static async Task<List<PromotionGetDTO>> GetPromotionsForProduct(
        [Description("The product ID to check")] int productId,
        IPromotionService promotionService)
        => await promotionService.GetForProductAsync(productId);

    // Folosit de SuggestionComposerAgent
    public static async Task<List<ProductGetDTO>> GetProductsByCategory(
        [Description("The category ID to get products for")] int categoryId,
        IProductService productService)
        => await productService.GetAllAsync(categoryId);
}
```

### PromotionAnalysis (nu modifica)

```csharp
[Description("Promotion analysis for the current cart")]
public sealed class PromotionAnalysis
{
    [JsonPropertyName("activeDeals")]   public List<Deal> ActiveDeals { get; set; } = [];
    [JsonPropertyName("nearMissDeals")] public List<Deal> NearMissDeals { get; set; } = [];
}

public sealed class Deal
{
    [JsonPropertyName("promotionId")]  public int PromotionId { get; set; }
    [JsonPropertyName("productId")]    public int ProductId { get; set; }
    [JsonPropertyName("description")]  public string Description { get; set; } = "";
    [JsonPropertyName("action")]       public string? Action { get; set; }
    [JsonPropertyName("savings")]      public decimal? Savings { get; set; }
}
```

### SuggestionResponse (nu modifica)

```csharp
[Description("Suggested products for the current cart")]
public sealed class SuggestionResponse
{
    [JsonPropertyName("suggestions")]
    public List<ProductSuggestion> Suggestions { get; set; } = [];
}

public sealed class ProductSuggestion
{
    [JsonPropertyName("productId")]    public int ProductId { get; set; }
    [JsonPropertyName("productName")]  public string ProductName { get; set; } = "";
    [JsonPropertyName("price")]        public decimal Price { get; set; }
    [JsonPropertyName("category")]     public string Category { get; set; } = "";
    [JsonPropertyName("reason")]       public string Reason { get; set; } = "";
}
```

---

## Rezumat fișiere afectate

| Fișier | Acțiune | Status |
|---|---|---|
| `secrets.json` (via VS → Manage User Secrets) | Adaugi `OpenAI:ApiKey` + `OpenAI:ModelId` | ❌ |
| `Api/Program.cs` | Fix DI — `IPromotionRepository` + agenți | ❌ |
| `DataAccess/Seed/PromotionSeed.cs` | **Nou** — 3 promoții seed-uite | ❌ |
| `DataAccess/SmartShoppingAssistantDbContext.cs` | Adaugi `PromotionSeed.Seed(modelBuilder)` | ❌ |
| `Api/Controllers/AnalysisController.cs` | **Nou** — orchestrare agenți | ❌ |
| `Agents/PromotionCheckerAgent.cs` | ✅ Gata | ✅ |
| `Agents/SuggestionComposerAgent.cs` | ✅ Gata | ✅ |
| `Models/SuggestionResponse.cs` | ✅ Gata | ✅ |
| `Tools/ShoppingTools.cs` | ✅ Gata | ✅ |
