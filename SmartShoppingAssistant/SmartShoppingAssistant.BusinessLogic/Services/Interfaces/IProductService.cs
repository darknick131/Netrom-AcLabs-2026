using SmartShoppingAssistant.BusinessLogic.DTOs;

namespace SmartShoppingAssistant.BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductGetDTO> GetByIdAsync(int id);
        Task<List<ProductGetDTO>> GetAllAsync();
        Task<ProductGetDTO> CreateAsync(ProductCreateDTO productCreateDTO);
        Task<ProductGetDTO> UpdateAsync(int id, ProductUpdateDTO productUpdateDTO);
        Task DeleteAsync(int id);
    }
}
