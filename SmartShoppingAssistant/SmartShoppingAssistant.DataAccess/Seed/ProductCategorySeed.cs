using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Entities;

namespace SmartShoppingAssistant.DataAccess.Seed
{
    public static class ProductCategorySeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductId = 1, CategoryId = 1 },
                new ProductCategory { ProductId = 2, CategoryId = 1 },
                new ProductCategory { ProductId = 3, CategoryId = 2 },
                new ProductCategory { ProductId = 4, CategoryId = 3 }
            );
        }
    }
}
