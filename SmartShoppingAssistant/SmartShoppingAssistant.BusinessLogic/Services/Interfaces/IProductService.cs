using SmartShoppingAssistant.BusinessLogic.DTOs.Common;
using SmartShoppingAssistant.BusinessLogic.DTOs.Product;

namespace SmartShoppingAssistant.BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductGetDTO> GetByIdAsync(int id);
        Task<PagedResult<ProductGetDTO>> GetAllAsync(QueryParams queryParams);
        Task<ProductGetDTO> CreateAsync(ProductCreateDTO productCreateDTO);
        Task<ProductGetDTO> UpdateAsync(int id, ProductUpdateDTO productUpdateDTO);
        Task DeleteAsync(int id);
    }
}
