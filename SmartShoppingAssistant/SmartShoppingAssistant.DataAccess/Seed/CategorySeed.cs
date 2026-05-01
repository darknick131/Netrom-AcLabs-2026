using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Entities;

namespace SmartShoppingAssistant.DataAccess.Seed
{
    public static class CategorySeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Băuturi", Description = "Băuturi răcoritoare și sucuri" },
                new Category { Id = 2, Name = "Snacks", Description = "Gustări și dulciuri" },
                new Category { Id = 3, Name = "Lactate", Description = "Produse lactate" }
            );
        }
    }
}
