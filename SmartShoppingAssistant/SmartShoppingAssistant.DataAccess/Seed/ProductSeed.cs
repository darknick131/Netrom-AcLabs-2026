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
                new Product { Id = 4, Name = "Iaurt natural", Description = "Iaurt 400g", Price = 4.50m, ImageUrl = null },
                new Product { Id = 5, Name = "Fanta Portocale 500ml", Description = "Băutură carbogazoasă cu aromă de portocale", Price = 5.49m, ImageUrl = null },
                new Product { Id = 6, Name = "Sprite 500ml", Description = "Băutură carbogazoasă cu lămâie și lime", Price = 5.49m, ImageUrl = null },
                new Product { Id = 7, Name = "Apă Plată Bucovina 2L", Description = "Apă minerală plată", Price = 3.99m, ImageUrl = null },
                new Product { Id = 8, Name = "Pringles Original 165g", Description = "Chipsuri crocante în tub", Price = 12.99m, ImageUrl = null },
                new Product { Id = 9, Name = "Doritos Nacho Cheese 150g", Description = "Chipsuri de porumb cu aromă de brânză", Price = 9.99m, ImageUrl = null },
                new Product { Id = 10, Name = "Milka Lapte Alpin 100g", Description = "Ciocolată cu lapte alpin", Price = 6.49m, ImageUrl = null },
                new Product { Id = 11, Name = "Lapte Zuzu 1L", Description = "Lapte proaspăt de vacă 3.5% grăsime", Price = 7.29m, ImageUrl = null },
                new Product { Id = 12, Name = "Brânză Telemea 400g", Description = "Brânză telemea de vacă", Price = 14.99m, ImageUrl = null },
                new Product { Id = 13, Name = "Smântână 20% 400g", Description = "Smântână pentru gătit și salate", Price = 8.49m, ImageUrl = null },
                new Product { Id = 14, Name = "Pâine Albă Feliată 500g", Description = "Pâine albă moale, feliată", Price = 5.99m, ImageUrl = null },
                new Product { Id = 15, Name = "Croissant Simplu", Description = "Croissant cu unt, proaspăt", Price = 3.49m, ImageUrl = null },
                new Product { Id = 16, Name = "Mere Ionatan 1kg", Description = "Mere românești, soiul Ionatan", Price = 6.99m, ImageUrl = null },
                new Product { Id = 17, Name = "Roșii Cherry 500g", Description = "Roșii cherry proaspete", Price = 8.99m, ImageUrl = null }
            );
        }
    }
}
