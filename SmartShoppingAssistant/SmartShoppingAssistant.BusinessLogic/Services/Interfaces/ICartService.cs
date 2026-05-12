using SmartShoppingAssistant.BusinessLogic.DTOs.CartItem;
using SmartShoppingAssistant.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartGetDTO> GetCartAsync();
        Task<CartItemGetDTO> AddItemAsync(CartItemCreateDTO dto);
        Task<CartItemGetDTO> UpdateItemQuantityAsync(int itemId, CartItemUpdateDTO dto);
        Task RemoveItemAsync(int itemId);
        Task ClearCartAsync();
        Task<AnalysisResponse> AnalyzeCartAsync();
    }
}
