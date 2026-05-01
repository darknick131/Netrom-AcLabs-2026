using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.DTOs
{
    public class ProductUpdateDTO
    {
        // id ul vine din url, nu din body
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
