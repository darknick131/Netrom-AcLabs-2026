using System;
using System.Collections.Generic;
using System.Text;
using SmartShoppingAssistant.BusinessLogic.DTOs.Categories;


namespace SmartShoppingAssistant.BusinessLogic.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryGetDTO> GetByIdAsync(int id);

        Task<List<CategoryGetDTO>> GetAllAsync();

        Task<CategoryGetDTO> CreateAsync(CategoryCreateDTO categoryCreateDTO);

        Task<CategoryGetDTO> UpdateAsync(int id, CategoryUpdateDTO categoryUpdateDTO);

        Task DeleteAsync(int id);
    }
}
