using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartShoppingAssistant.DataAccess.Entities;
namespace SmartShoppingAssistant.DataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        // are 3 proprietati: Id, Name(100), Description(500)
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            // isi da seama daca nu punem cheia primara dar e bn sa punem
            // Id
            builder.HasKey(c => c.Id);

            // proprietati
            // Name
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Description
            builder.Property(c => c.Description)
                .HasMaxLength(500);

            // // tot in astea de configurare se definesc relatii

            // builder.HasMany(c => c.Products)
            //     .WithMany(c => c.Categories);
           
        }
    }
}
