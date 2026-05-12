using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using OpenAI;
using SmartShoppingAssistant.BusinessLogic.Agents;
using SmartShoppingAssistant.BusinessLogic.Services;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistant.DataAccess;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// il ia din appsettings.json   -> ConnectionStrings:SmartShoppingAssistantContext
var connectionString = builder.Configuration.GetConnectionString("SmartShoppingAssistantContext");

builder.Services.AddDbContext<SmartShoppingAssistantDbContext>(options => options.UseSqlServer(connectionString));

// Register the repository for Product entity
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Register the repository for Category entity
builder.Services.AddScoped<IRepository<Category>, BaseRepository<Category>>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


// Register the repository for Promotion entity
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
builder.Services.AddScoped<IPromotionService, PromotionService>();

// cart entity
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<ICartService, CartService>();


// Agents
var openAiApiKey = builder.Configuration["OpenAI:ApiKey"]
                        ?? throw new InvalidOperationException("OpenAI API key is not configured.");

var openAiModel = builder.Configuration["OpenAI:ModelId"] ?? "gpt-4o";

builder.Services.AddSingleton<IChatClient>(
    new OpenAIClient(openAiApiKey)
            .GetChatClient(openAiModel)
            .AsIChatClient()
            .AsBuilder()
            .UseFunctionInvocation()
            .Build()
            );

builder.Services.AddScoped<IPromotionCheckerAgent, PromotionCheckerAgent>();
builder.Services.AddScoped<ISuggestionComposerAgent, SuggestionComposerAgent>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection(); // dezactivat pentru development local pe WSL2

app.UseAuthorization();

app.MapControllers();

app.Run();
