using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Entities;

namespace SmartShoppingAssistant.DataAccess.Seed
{
    public static class ProductSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Coca Cola 500ml", Description = "Băutură carbogazoasă", Price = 5.99m, ImageUrl = null },
                new Product { Id = 2, Name = "Pepsi 500ml", Description = "Băutură carbogazoasă", Price = 5.49m, ImageUrl = null },
                new Product { Id = 3, Name = "Lay's Sare", Description = "Chipsuri cu sare", Price = 7.99m, ImageUrl = null },
                new Product { Id = 4, Name = "Iaurt natural", Description = "Iaurt 400g", Price = 4.50m, ImageUrl = null }
            );
        }
    }
}
