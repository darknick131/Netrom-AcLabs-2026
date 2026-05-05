using System;
using System.Collections.Generic;
using System.Text;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.BusinessLogic.DTOs.Categories;

namespace SmartShoppingAssistant.BusinessLogic.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryGetDTO ToCategoryGetDTO(Category category)
        {
            return new CategoryGetDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public static Category ToEntity(CategoryCreateDTO categoryCreateDTO)
        {
            return new Category
            {
                Name = categoryCreateDTO.Name,
                Description = categoryCreateDTO.Description
            };
        }

        public static void ApplyUpdate(Category category, CategoryUpdateDTO categoryUpdateDTO)
        {
            category.Name = categoryUpdateDTO.Name;
            category.Description = categoryUpdateDTO.Description;
        }
    }
}
