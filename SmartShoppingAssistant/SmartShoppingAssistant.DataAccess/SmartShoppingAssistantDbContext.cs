using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Configurations;
using SmartShoppingAssistant.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.DataAccess
{
    public class SmartShoppingAssistantDbContext(DbContextOptions<SmartShoppingAssistantDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // or facem asta comentata pt fiecare configuratie or facem chestia aia cu assembly si ne scuteste de munca de a adauga fiecare configuratie in parte

            // modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartShoppingAssistantDbContext).Assembly);
        }
    }
}
