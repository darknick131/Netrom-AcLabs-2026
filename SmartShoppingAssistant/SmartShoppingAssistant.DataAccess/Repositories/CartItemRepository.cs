using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.DataAccess.Repositories
{
    public class CartItemRepository(SmartShoppingAssistantDbContext context): BaseRepository<CartItem>(context), ICartItemRepository
    {
        public async Task<CartItem> GetByIdWithProductAsync(int id)
        {
            var item = await context.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.Id == id);

            if (item == null)
                throw new KeyNotFoundException($"CartItem with ID {id} was not found.");

            return item;
        }

        public async Task<List<CartItem>> GetAllWithProductsAsync()
        {
            return await context.CartItems
                .Include(ci => ci.Product)
                    .ThenInclude(p => p.Categories)
                .ToListAsync();
        }

        public async Task DeleteAllAsync()
        {
            var allItems = await context.CartItems.ToListAsync();
            context.CartItems.RemoveRange(allItems);
            await context.SaveChangesAsync();
        }


        // adaugat
        private IQueryable<CartItem> WithProductWithCategories()
        {
            return context.CartItems
                .Include(ci => ci.Product)
                    .ThenInclude(p => p.Categories);
        }


        public async Task<List<CartItem>> GetAllWithProductWithCategoriesAsync()
        {
            return await WithProductWithCategories().ToListAsync();
        }
    }
}
