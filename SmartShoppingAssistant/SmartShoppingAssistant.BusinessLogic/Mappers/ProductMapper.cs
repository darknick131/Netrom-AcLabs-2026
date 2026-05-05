using System;
using System.Collections.Generic;
using System.Text;
using SmartShoppingAssistant.BusinessLogic.DTOs.Product;
using SmartShoppingAssistant.DataAccess.Entities;

namespace SmartShoppingAssistant.BusinessLogic.Mappers
{
    public static class ProductMapper
    {
        // Mapam din entitate in DTO
        public static ProductGetDTO ToProductGetDTO(Product product)
        {
            return new ProductGetDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,

                 Categories = product.Categories
                                    .Select(c => CategoryMapper.ToCategoryGetDTO(c))
                                    .ToList()
            };
        }

        // mapam din DTO in entitate
        public static Product ToEntity(ProductCreateDTO productCreateDTO)
        {
            return new Product
            {
                Name = productCreateDTO.Name,
                Description = productCreateDTO.Description,
                Price = productCreateDTO.Price,
                ImageUrl = productCreateDTO.ImageUrl
            };
        }

        // pentru PUT -> update
        public static void ApplyUpdate(Product product, ProductUpdateDTO productUpdateDTO)
        {
            product.Name = productUpdateDTO.Name;
            // pentru campurile optionale, daca nu sunt furnizate in DTO, le setam la string.Empty
            product.Description = productUpdateDTO.Description;
            product.Price = productUpdateDTO.Price;
            product.ImageUrl = productUpdateDTO.ImageUrl;
        }

    }
}
