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
                new ProductCategory { ProductId = 4, CategoryId = 3 },
                new ProductCategory { ProductId = 5, CategoryId = 1 },
                new ProductCategory { ProductId = 6, CategoryId = 1 },
                new ProductCategory { ProductId = 7, CategoryId = 1 },
                new ProductCategory { ProductId = 8, CategoryId = 2 },
                new ProductCategory { ProductId = 9, CategoryId = 2 },
                new ProductCategory { ProductId = 10, CategoryId = 2 },
                new ProductCategory { ProductId = 11, CategoryId = 3 },
                new ProductCategory { ProductId = 12, CategoryId = 3 },
                new ProductCategory { ProductId = 13, CategoryId = 3 },
                new ProductCategory { ProductId = 14, CategoryId = 4 },
                new ProductCategory { ProductId = 15, CategoryId = 4 },
                new ProductCategory { ProductId = 16, CategoryId = 5 },
                new ProductCategory { ProductId = 17, CategoryId = 5 }
            );
        }
    }
}
