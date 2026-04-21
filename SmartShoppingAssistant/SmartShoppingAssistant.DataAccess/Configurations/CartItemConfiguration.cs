using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartShoppingAssistant.DataAccess.Entities;
namespace SmartShoppingAssistant.DataAccess.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        // are 3 proprietati: Id, ProductId, Quantity
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(ci => ci.Id);

            builder.HasOne(ci => ci.Product)
                .WithOne(p => p.cartItem)
                .HasForeignKey<CartItem>(ci => ci.ProductId);

            builder.Property(ci => ci.Quantity)
                .IsRequired();

        }

    }
}
