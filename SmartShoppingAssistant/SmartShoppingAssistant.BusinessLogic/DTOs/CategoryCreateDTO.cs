using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.DTOs
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
