using SmartShoppingAssistant.BusinessLogic.DTOs.Categories;
using SmartShoppingAssistant.BusinessLogic.DTOs.Common;

namespace SmartShoppingAssistant.BusinessLogic.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryGetDTO> GetByIdAsync(int id);
        Task<PagedResult<CategoryGetDTO>> GetAllAsync(QueryParams queryParams);
        Task<CategoryGetDTO> CreateAsync(CategoryCreateDTO categoryCreateDTO);
        Task<CategoryGetDTO> UpdateAsync(int id, CategoryUpdateDTO categoryUpdateDTO);
        Task DeleteAsync(int id);
    }
}
