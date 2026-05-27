using SmartShoppingAssistant.BusinessLogic.DTOs.Common;
using SmartShoppingAssistant.BusinessLogic.DTOs.Promotions;

namespace SmartShoppingAssistant.BusinessLogic.Services.Interfaces
{
    public interface IPromotionService
    {
        Task<PromotionGetDTO> GetByIdAsync(int id);
        Task<PagedResult<PromotionGetDTO>> GetAllAsync(QueryParams queryParams);
        Task<PromotionGetDTO> CreateAsync(PromotionCreateDTO dto);
        Task<PromotionGetDTO> UpdateAsync(int id, PromotionUpdateDTO dto);
        Task DeleteAsync(int id);
        Task<List<PromotionGetDTO>> GetForProductAsync(int productId);
    }
}
