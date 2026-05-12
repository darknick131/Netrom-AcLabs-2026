using Microsoft.AspNetCore.Mvc;
using SmartShoppingAssistant.BusinessLogic.Agents;
using SmartShoppingAssistant.BusinessLogic.Models;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using System.Text.Json;

namespace SmartShoppingAssistant.Api.Controllers
{
    [ApiController]
    [Route("api/analysis")]
    public class AgentsController(IPromotionCheckerAgent promotionCheckerAgent, ISuggestionComposerAgent suggestionComposerAgent, ICartService cartService, ICategoryService categoryService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Analyze(CancellationToken cancellationToken)
        {
            var cart = await cartService.GetCartAsync();
            var categories = await categoryService.GetAllAsync();

            var cartJson = JsonSerializer.Serialize(cart);
            var categoriesJson = JsonSerializer.Serialize(categories);

            // Agent 1 — 
            var checkerAgent = promotionCheckerAgent.Build(cartJson);
            var checkerResult = await checkerAgent.RunAsync(
                "Analyze the cart promotions.", cancellationToken: cancellationToken);
            var promotionAnalysisJson = checkerResult.Messages.Last().Text;
            var promotionAnalysis = JsonSerializer.Deserialize<PromotionAnalysis>(promotionAnalysisJson)!;

            // Agent 2
            var composerAgent = suggestionComposerAgent.Build(cartJson, categoriesJson, promotionAnalysisJson);
            var composerResult = await composerAgent.RunAsync(
                "Suggest products for the cart.", cancellationToken: cancellationToken);
            var suggestions = JsonSerializer.Deserialize<SuggestionResponse>(composerResult.Messages.Last().Text)!;

            return Ok(new { promotionAnalysis, suggestions });
        }
    }
}
