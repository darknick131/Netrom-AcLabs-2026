using SmartShoppingAssistant.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using SmartShoppingAssistant.BusinessLogic.Mappers;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Repositories;
using SmartShoppingAssistant.BusinessLogic.DTOs;
using SmartShoppingAssistant.BusinessLogic.Mappers;

namespace SmartShoppingAssistant.BusinessLogic.Services
{
    public class CategoryService(IRepository<Category> categoryRepository) : ICategoryService
    {
        public async Task<CategoryGetDTO> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            return CategoryMapper.ToCategoryGetDTO(category);
        }

        public async Task<List<CategoryGetDTO>> GetAllAsync()
        {
            var categories = await categoryRepository.GetAllAsync();
            return categories.Select(CategoryMapper.ToCategoryGetDTO).ToList();
        }

        public async Task<CategoryGetDTO> CreateAsync(CategoryCreateDTO categoryCreateDTO)
        {
            var category = CategoryMapper.ToEntity(categoryCreateDTO);
            var createdCategory = await categoryRepository.AddAsync(category);
            return CategoryMapper.ToCategoryGetDTO(createdCategory);
        }

        public async Task<CategoryGetDTO> UpdateAsync(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            CategoryMapper.ApplyUpdate(category, categoryUpdateDTO);
            var updatedCategory = await categoryRepository.UpdateAsync(category);
            return CategoryMapper.ToCategoryGetDTO(updatedCategory);
        }

        public async Task DeleteAsync(int id)
        {
            await categoryRepository.DeleteAsync(id);
        }
    }
}
