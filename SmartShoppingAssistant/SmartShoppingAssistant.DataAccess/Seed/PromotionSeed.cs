using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Entities.Enums;

namespace SmartShoppingAssistant.DataAccess.Seed
{
    public static class PromotionSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Promotion>().HasData(
                new Promotion
                {
                    Id = 1,
                    Name = "Cumpără 5 Coca-Cola, primești 1 gratis",
                    Type = PromotionType.Quantity,
                    Threshold = 5,
                    Reward = PromotionReward.FreeItems,
                    RewardValue = 1,
                    ProductId = 1,
                    CategoryId = null,
                    IsActive = true
                },
                new Promotion
                {
                    Id = 2,
                    Name = "10% reducere la comenzi peste 100 RON",
                    Type = PromotionType.CartTotal,
                    Threshold = 100.00m,
                    Reward = PromotionReward.PercentDiscount,
                    RewardValue = 10,
                    ProductId = null,
                    CategoryId = null,
                    IsActive = true
                },
                new Promotion
                {
                    Id = 3,
                    Name = "Cumpără 3 Snacks-uri, primești 1 gratis",
                    Type = PromotionType.Quantity,
                    Threshold = 3,
                    Reward = PromotionReward.FreeItems,
                    RewardValue = 1,
                    ProductId = null,
                    CategoryId = 2,
                    IsActive = true
                }
            );
        }
    }
}