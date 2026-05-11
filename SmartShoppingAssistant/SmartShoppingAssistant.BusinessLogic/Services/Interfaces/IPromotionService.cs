using SmartShoppingAssistant.BusinessLogic.DTOs.Promotions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.Services.Interfaces
{
    public interface IPromotionService
    {
        Task<PromotionGetDTO> GetByIdAsync(int id);
        Task<List<PromotionGetDTO>> GetAllAsync();
        Task<PromotionGetDTO> CreateAsync(PromotionCreateDTO dto);
        Task<PromotionGetDTO> UpdateAsync(int id, PromotionUpdateDTO dto);
        Task DeleteAsync(int id);
        Task<List<PromotionGetDTO>> GetForProductAsync(int productId);
    }
}
