using SmartShoppingAssistant.BusinessLogic.DTOs.Promotions;
using SmartShoppingAssistant.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.Mappers
{
    public class PromotionMapper
    {
        public static PromotionGetDTO ToPromotionGetDTO(Promotion promotion)
        {
            return new PromotionGetDTO
            {
                Id = promotion.Id,
                Name = promotion.Name,
                Type = promotion.Type,
                Threshold = promotion.Threshold,
                Reward = promotion.Reward,
                RewardValue = promotion.RewardValue,
                ProductId = promotion.ProductId,
                CategoryId = promotion.CategoryId,
                IsActive = promotion.IsActive
            };
        }

        public static Promotion ToEntity(PromotionCreateDTO dto)
        {
            return new Promotion
            {
                Name = dto.Name,
                Type = dto.Type,
                Threshold = dto.Threshold,
                Reward = dto.Reward,
                RewardValue = dto.RewardValue,
                ProductId = dto.ProductId,
                CategoryId = dto.CategoryId,
                IsActive = dto.IsActive
            };
        }

        public static void ApplyUpdate(Promotion promotion, PromotionUpdateDTO dto)
        {
            promotion.Name = dto.Name;
            promotion.Type = dto.Type;
            promotion.Threshold = dto.Threshold;
            promotion.Reward = dto.Reward;
            promotion.RewardValue = dto.RewardValue;
            promotion.ProductId = dto.ProductId;
            promotion.CategoryId = dto.CategoryId;
            promotion.IsActive = dto.IsActive;
        }
    }
}
