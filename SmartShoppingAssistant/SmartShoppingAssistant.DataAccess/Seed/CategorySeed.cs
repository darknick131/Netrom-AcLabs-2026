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
                new Category { Id = 3, Name = "Lactate", Description = "Produse lactate" },
                new Category { Id = 4, Name = "Panificație", Description = "Pâine, cozonaci și produse de patiserie" },
                new Category { Id = 5, Name = "Legume și Fructe", Description = "Legume și fructe proaspete" },
                new Category { Id = 6, Name = "Carne și Mezeluri", Description = "Produse din carne și mezeluri" },
                new Category { Id = 7, Name = "Îngrijire Personală", Description = "Produse de igienă și cosmetice" }
            );
        }
    }
}
