# Plan de Implementare — SmartShoppingAssistant

## Cuprins

0. [Arhitectura agenților (multi-agent)](#0-arhitectura-agenților-multi-agent)
1. [Status — ce este gata și ce lipsește](#1-status--ce-este-gata-și-ce-lipsește)
2. [Fix Program.cs — înregistrări lipsă](#2-fix-programcs--înregistrări-lipsă)
3. [Seeding Promotions](#3-seeding-promotions)
4. [DTO-uri — aliniere cu spec-ul](#4-dto-uri--aliniere-cu-spec-ul)
5. [Actualizare CartService — promoții aplicate](#5-actualizare-cartservice--promoții-aplicate)
6. [Actualizare PromotionService — filtru activeOnly](#6-actualizare-promotionservice--filtru-activeonly)
7. [Actualizare Controllere](#7-actualizare-controllere)
8. [AnalysisController — endpoint nou](#8-analysiscontroller--endpoint-nou)
9. [Referință — structuri deja implementate](#9-referință--structuri-deja-implementate)

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
| `Tools/ShoppingTools.cs` | Ambele metode implementate (GetPromotionsForProduct + GetProductsByCategory) |
| `DataAccess/Repositories/PromotionRepository.cs` | GetForProductAsync implementat corect |
| `Services/PromotionService.GetForProductAsync` | Delegă la repository |
| `Program.cs` — IChatClient | OpenAI + UseFunctionInvocation configurat |
| `Controllers/CartController.cs` | CRUD de bază implementat |
| `Controllers/PromotionController.cs` | CRUD de bază implementat |
| `Controllers/ProductController.cs` | CRUD + filtru categoryId implementat |
| `Controllers/CategoryController.cs` | CRUD implementat |
| `DataAccess/Seed/CategorySeed.cs` | 3 categorii seed-uite |
| `DataAccess/Seed/ProductSeed.cs` | 4 produse seed-uite |
| `DataAccess/Seed/ProductCategorySeed.cs` | Relații seed-uite |

### ❌ Lipsește — de implementat

| # | Ce lipsește | Fișier | Prioritate |
|---|---|---|---|
| 1 | `IPromotionRepository` nu e înregistrat în DI | `Program.cs` | **Critic** — app crasha |
| 2 | Agenții nu sunt înregistrați în DI | `Program.cs` | **Critic** |
| 3 | `PromotionSeed.cs` nu există | `DataAccess/Seed/` | Demo fără promoții |
| 4 | `AnalysisController` nu există | `Api/Controllers/` | Endpoint principal |
| 5 | `CartItemGetDTO` — câmpuri cu nume greșite | `DTOs/CartItem/CartItemGetDTO.cs` | Frontend |
| 6 | `CartGetDTO` — lipsesc câmpuri | `DTOs/CartItem/CartGetDTO.cs` | Frontend |
| 7 | `AppliedPromotionDTO` nu există | `DTOs/CartItem/` | Frontend |
| 8 | `PromotionGetDTO` — enum ca număr, nu string | `DTOs/Promotions/PromotionGetDTO.cs` | Frontend |
| 9 | `CartItemMapper` — câmpuri vechi | `Mappers/CartItemMapper.cs` | Depinde de #5 |
| 10 | `CartService` — nu trackuiește promoțiile aplicate | `Services/CartService.cs` | Depinde de #6 |
| 11 | `PromotionService.GetAllAsync` — lipsește filtru | `Services/PromotionService.cs` | Frontend |
| 12 | `CartController` — mutațiile nu returnează coșul | `Controllers/CartController.cs` | Frontend |
| 13 | `PromotionController.GetAll` — lipsește `activeOnly` | `Controllers/PromotionController.cs` | Frontend |

---

## 2. Fix Program.cs — înregistrări lipsă

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
// Fix 1: PromotionService cere IPromotionRepository, nu IRepository<Promotion>
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();

// Fix 2: Înregistrare agenți prin interfețele lor
builder.Services.AddScoped<IPromotionCheckerAgent, PromotionCheckerAgent>();
builder.Services.AddScoped<ISuggestionComposerAgent, SuggestionComposerAgent>();
```

> **De ce prin interfață?** `AnalysisController` cere `IPromotionCheckerAgent` în constructor.
> DI știe să injecteze `PromotionCheckerAgent` concret pentru că l-ai mapat la interfață.
> **Nu șterge** linia cu `IRepository<Promotion>` — `CartService` o poate folosi.

---

## 3. Seeding Promotions

**Fișiere:** `DataAccess/Seed/PromotionSeed.cs` (nou) + `SmartShoppingAssistantDbContext.cs` (modificat)

Seeding = date inițiale introduse în BD printr-o **migrare EF Core**.
Necesare ca agentul să aibă promoții reale de verificat la demo.
ID-urile referite sunt cele deja seed-uite: Products: 1=Coca Cola, 2=Pepsi, 3=Lay's, 4=Iaurt; Categories: 1=Băuturi, 2=Snacks, 3=Lactate.

### 3a. Fișier nou: `DataAccess/Seed/PromotionSeed.cs`

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

### 3b. Adaugă în `SmartShoppingAssistantDbContext.cs` — `OnModelCreating`

```csharp
CategorySeed.Seed(modelBuilder);
ProductSeed.Seed(modelBuilder);
ProductCategorySeed.Seed(modelBuilder);
PromotionSeed.Seed(modelBuilder);   // ← adaugă asta
```

### 3c. Migrare EF Core

Rulezi în terminal din folderul `SmartShoppingAssistant.Api/`:

```bash
dotnet ef migrations add PromotionSeed --project ../SmartShoppingAssistant.DataAccess
dotnet ef database update
```

---

## 4. DTO-uri — aliniere cu spec-ul

### 4a. `CartItemGetDTO` — redenumire câmpuri

**Fișier:** `BusinessLogic/DTOs/CartItem/CartItemGetDTO.cs`

| Câmp actual | Câmp în spec | De ce |
|---|---|---|
| `UnitPrice` | `Price` | Spec returnează `"price"`, nu `"unitPrice"` |
| `ItemTypeTotal` | `Subtotal` | Spec returnează `"subtotal"`, nu `"itemTypeTotal"` |

**Aspect actual:**
```json
{ "unitPrice": 5.99, "itemTypeTotal": 35.94 }
```

**Aspect dorit:**
```json
{ "price": 5.99, "subtotal": 35.94 }
```

---

### 4b. `CartGetDTO` — redenumire + câmp nou

**Fișier:** `BusinessLogic/DTOs/CartItem/CartGetDTO.cs`

| Schimbare | De ce |
|---|---|
| `Discount` → `TotalDiscount` | Spec folosește `"totalDiscount": -5.99` (valoare negativă) |
| Adaugă `List<AppliedPromotionDTO> AppliedPromotions` | Spec include lista promoțiilor aplicate individual |

**Aspect dorit:**
```json
{
  "items": [...],
  "subtotal": 35.94,
  "appliedPromotions": [
    { "promotionName": "Buy 5 Get 1 Free Spaghetti", "discount": -5.99 }
  ],
  "totalDiscount": -5.99,
  "total": 29.95
}
```

---

### 4c. DTO nou: `AppliedPromotionDTO`

**Fișier nou:** `BusinessLogic/DTOs/CartItem/AppliedPromotionDTO.cs`

```csharp
namespace SmartShoppingAssistant.BusinessLogic.DTOs.CartItem
{
    public class AppliedPromotionDTO
    {
        public string PromotionName { get; set; } = null!;
        public decimal Discount { get; set; }   // valoare negativă (ex: -5.99)
    }
}
```

**De ce:** `CartGetDTO` trebuie să returneze lista promoțiilor aplicate individual,
nu doar suma totală. Frontenderii afișează fiecare reducere separat.

---

### 4d. `PromotionGetDTO` — serializare enum ca string

**Fișier:** `BusinessLogic/DTOs/Promotions/PromotionGetDTO.cs`

Adaugă `[JsonConverter(typeof(JsonStringEnumConverter))]` pe proprietățile `Type` și `Reward`.

**Aspect actual:**
```json
{ "type": 0, "reward": 0 }
```

**Aspect dorit:**
```json
{ "type": "Quantity", "reward": "FreeItems" }
```

**De ce:** Frontenderii citesc text, nu numere. Un `0` nu spune nimic; `"Quantity"` este self-documenting.

---

## 5. Actualizare CartService — promoții aplicate

**Fișier:** `BusinessLogic/Services/CartService.cs`

**Schimbare:** Metoda `CalculateDiscountAsync` returnează acum `List<AppliedPromotionDTO>`
în loc de `decimal`. Fiecare promoție activă care generează o reducere e adăugată în
listă cu `Discount` negativ. `GetCartAsync` calculează `TotalDiscount` ca suma listei.

**Structura nouă:**
```csharp
private async Task<List<AppliedPromotionDTO>> CalculatePromotionsAsync(List<CartItem> cartItems, decimal subtotal)
{
    var promotions = await promotionRepository.GetAllAsync();
    var applied = new List<AppliedPromotionDTO>();

    foreach (var p in promotions.Where(p => p.IsActive))
    {
        var discount = CalculateSinglePromotion(p, cartItems, subtotal);
        if (discount > 0)
        {
            applied.Add(new AppliedPromotionDTO
            {
                PromotionName = p.Name,
                Discount = -discount   // negativ
            });
        }
    }
    return applied;
}
```

`GetCartAsync` devine:
```csharp
var appliedPromotions = await CalculatePromotionsAsync(cartItems, subtotal);
var totalDiscount = appliedPromotions.Sum(p => p.Discount);

return new CartGetDTO
{
    Items = itemDtos,
    Subtotal = subtotal,
    AppliedPromotions = appliedPromotions,
    TotalDiscount = totalDiscount,
    Total = subtotal + totalDiscount   // subtotal - |discount|
};
```

> Actualizează și `CartItemMapper.ToCartItemGetDTO()` — redenumește `UnitPrice → Price` și `ItemTypeTotal → Subtotal`.

---

## 6. Actualizare PromotionService — filtru activeOnly

**Fișiere:**
- `BusinessLogic/Services/Interfaces/IPromotionService.cs`
- `BusinessLogic/Services/PromotionService.cs`

```csharp
// interfata
Task<List<PromotionGetDTO>> GetAllAsync(bool activeOnly = false);

// implementare
public async Task<List<PromotionGetDTO>> GetAllAsync(bool activeOnly = false)
{
    var promotions = await promotionRepository.GetAllAsync();
    if (activeOnly)
        promotions = promotions.Where(p => p.IsActive).ToList();
    return promotions.Select(PromotionMapper.ToPromotionGetDTO).ToList();
}
```

**De ce:** Spec-ul arată `GET /api/promotions?activeOnly=true` — clientul (app frontend)
vede doar promoțiile active, adminul le vede pe toate.

---

## 7. Actualizare Controllere

### 7a. `PromotionController.cs`

`GetAll()` primește `[FromQuery] bool activeOnly = false` și îl trimite la service:

```csharp
[HttpGet]
public async Task<IActionResult> GetAll([FromQuery] bool activeOnly = false)
{
    var promotions = await promotionService.GetAllAsync(activeOnly);
    return Ok(promotions);
}
```

---

### 7b. `CartController.cs`

`AddItem`, `UpdateItem` și `RemoveItem` returnează coșul complet (același shape ca `GET /api/cart`).
**Frontenderii nu mai fac un al doilea request GET după fiecare mutație.**

```csharp
[HttpPost("items")]
public async Task<IActionResult> AddItem([FromBody] CartItemCreateDTO dto)
{
    await cartService.AddItemAsync(dto);
    var cart = await cartService.GetCartAsync();
    return Ok(cart);
}

[HttpPut("items/{itemId}")]
public async Task<IActionResult> UpdateItem(int itemId, [FromBody] CartItemUpdateDTO dto)
{
    await cartService.UpdateItemQuantityAsync(itemId, dto);
    var cart = await cartService.GetCartAsync();
    return Ok(cart);
}

[HttpDelete("items/{itemId}")]
public async Task<IActionResult> RemoveItem(int itemId)
{
    await cartService.RemoveItemAsync(itemId);
    var cart = await cartService.GetCartAsync();
    return Ok(cart);
}
```

---

## 8. AnalysisController — endpoint nou

**Fișier nou:** `SmartShoppingAssistant.Api/Controllers/AnalysisController.cs`

Controller-ul **orchestrează** cei doi agenți în secvență și returnează rezultatul combinat.
Serializarea/deserializarea JSON este făcută explicit — agenții returnează text, nu obiecte tipizate.

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

## 9. Referință — structuri deja implementate

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
| `Api/Program.cs` | Modificat — fix DI bug + înregistrare agenți | ❌ |
| `Api/Controllers/CartController.cs` | Modificat — mutații returnează coșul complet | ❌ |
| `Api/Controllers/PromotionController.cs` | Modificat — adaugă query param `activeOnly` | ❌ |
| `Api/Controllers/AnalysisController.cs` | **Nou** — endpoint pentru rularea agenților | ❌ |
| `BusinessLogic/DTOs/CartItem/CartItemGetDTO.cs` | Modificat — redenumire câmpuri | ❌ |
| `BusinessLogic/DTOs/CartItem/CartGetDTO.cs` | Modificat — redenumire + câmp nou | ❌ |
| `BusinessLogic/DTOs/CartItem/AppliedPromotionDTO.cs` | **Nou** — DTO pentru promoție aplicată | ❌ |
| `BusinessLogic/DTOs/Promotions/PromotionGetDTO.cs` | Modificat — enum ca string | ❌ |
| `BusinessLogic/Mappers/CartItemMapper.cs` | Modificat — redenumire câmpuri | ❌ |
| `BusinessLogic/Services/CartService.cs` | Modificat — tracked applied promotions | ❌ |
| `BusinessLogic/Services/Interfaces/IPromotionService.cs` | Modificat — adaugă param `activeOnly` | ❌ |
| `BusinessLogic/Services/PromotionService.cs` | Modificat — implementare filtru `activeOnly` | ❌ |
| `DataAccess/Seed/PromotionSeed.cs` | **Nou** — seeding promoții | ❌ |
| `DataAccess/SmartShoppingAssistantDbContext.cs` | Modificat — apelare `PromotionSeed` | ❌ |
| `Agents/PromotionCheckerAgent.cs` | ✅ Gata — nu modifica | ✅ |
| `Agents/SuggestionComposerAgent.cs` | ✅ Gata — nu modifica | ✅ |
| `Models/SuggestionResponse.cs` | ✅ Gata — nu modifica | ✅ |
| `Tools/ShoppingTools.cs` | ✅ Gata — nu modifica | ✅ |
