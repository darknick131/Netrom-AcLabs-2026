using System.ComponentModel;
using SmartShoppingAssistant.BusinessLogic.DTOs.Common;
using SmartShoppingAssistant.BusinessLogic.DTOs.Product;
using SmartShoppingAssistant.BusinessLogic.DTOs.Promotions;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;

namespace SmartShoppingAssistant.BusinessLogic.Tools
{
    public static class ShoppingTools
    {
        [Description("Get all active promotions that apply to a specific product (by product ID or its categories)")]
        public static async Task<List<PromotionGetDTO>> GetPromotionsForProduct(
            [Description("The product ID to check promotions for")] int productId,
            IPromotionService promotionService)
        {
            return await promotionService.GetForProductAsync(productId);
        }

        [Description("Get all available products that belong to a specific category")]
        public static async Task<List<ProductGetDTO>> GetProductsByCategory(
            [Description("The category ID to get products for")] int categoryId,
            IProductService productService)
        {
            var result = await productService.GetAllAsync(new QueryParams { CategoryId = categoryId, PageSize = 1000 });
            return result.Items;
        }
    }
}
