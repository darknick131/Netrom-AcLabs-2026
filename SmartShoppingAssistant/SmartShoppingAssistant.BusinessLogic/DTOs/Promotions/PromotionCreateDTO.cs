using SmartShoppingAssistant.DataAccess.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.DTOs.Promotions
{
    public class PromotionCreateDTO
    {
        public string Name { get; set; } = null!;
        public PromotionType Type { get; set; }
        public decimal Threshold { get; set; }
        public PromotionReward Reward { get; set; }
        public int RewardValue { get; set; }

        // daca ambele sunt null, inseamna ca promotionul se aplica la toate produsele din cos
        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
