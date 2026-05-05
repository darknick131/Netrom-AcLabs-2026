using SmartShoppingAssistant.BusinessLogic.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.DTOs.Product
{
    public class ProductGetDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public List<CategoryGetDTO> Categories { get; set; } = new List<CategoryGetDTO>();
    }
}
