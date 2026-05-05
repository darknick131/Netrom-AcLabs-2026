using SmartShoppingAssistant.BusinessLogic.DTOs.Product;

namespace SmartShoppingAssistant.BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductGetDTO> GetByIdAsync(int id);
        Task<List<ProductGetDTO>> GetAllAsync(int? categoryId = null);
        Task<ProductGetDTO> CreateAsync(ProductCreateDTO productCreateDTO);
        Task<ProductGetDTO> UpdateAsync(int id, ProductUpdateDTO productUpdateDTO);
        Task DeleteAsync(int id);
    }
}
