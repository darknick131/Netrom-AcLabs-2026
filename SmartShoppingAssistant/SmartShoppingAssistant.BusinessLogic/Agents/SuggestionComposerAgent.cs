using System.ComponentModel;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using SmartShoppingAssistant.BusinessLogic.Models;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistant.BusinessLogic.Tools;

namespace SmartShoppingAssistant.BusinessLogic.Agents;

public class SuggestionComposerAgent(IChatClient chatClient, IProductService productService) : ISuggestionComposerAgent
{
    public ChatClientAgent Build(string cartJson, string categoriesJson, string promotionAnalysisJson = "")
    {
        return new ChatClientAgent(
            chatClient,
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
                        5. Provide a summary of your suggestions.
                        """,
                    ResponseFormat = ChatResponseFormat.ForJsonSchema<AnalysisResponse>(),
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
            null!,
            null!
        );
    }
}
