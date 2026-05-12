using SmartShoppingAssistant.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.DataAccess.Repositories
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<CartItem> GetByIdWithProductAsync(int id);
        Task<List<CartItem>> GetAllWithProductsAsync();
        Task DeleteAllAsync();

        // adaugat
        Task<List<CartItem>> GetAllWithProductWithCategoriesAsync();
    }
}
