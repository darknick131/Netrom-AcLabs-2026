using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.DTOs.Product
{
    public class ProductCreateDTO
    {
        // Nu ne trebuie : id
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
