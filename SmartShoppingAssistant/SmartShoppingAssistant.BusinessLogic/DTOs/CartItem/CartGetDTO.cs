using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.DTOs.CartItem
{
    public class CartGetDTO
    {
        public List<CartItemGetDTO> Items { get; set; } = new();
        public decimal Subtotal { get; set; }   // suma fara reduceri -> itemtypetotal pt fiecare item
        public decimal Discount { get; set; }   // suma ce se obtine din reduceri
        public decimal Total { get; set; }      // Subtotal - Discount

        public List<AppliedPromotionDTO> AppliedPromotions { get; set; } = new List<AppliedPromotionDTO>();
    }
}
