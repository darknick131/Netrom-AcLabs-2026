using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.DTOs.Categories
{
    public class CategoryUpdateDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
