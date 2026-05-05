using SmartShoppingAssistant.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SmartShoppingAssistant.DataAccess.Repositories
{
    public class ProductRepository(SmartShoppingAssistantDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        //get products with category

        public async Task<Product> GetProductByIdWithCategoriesAsync(int id)
        {
            var product = await context.Set<Product>()
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                throw new KeyNotFoundException($"Product with id {id} not found.");

            return product;
        }

        // get all products with all categories
        public async Task<List<Product>> GetAllProductsWithCategoriesAsync()
        {
            return await context.Set<Product>()
                .Include(p => p.Categories)
                .ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsByCategoryAsync(int categoryId)
        {
            var products = await context.Products
                .Where(p => p.Categories.Any(c => c.Id == categoryId))
                .Include(p => p.Categories)
                .ToListAsync();

            return products;
        }


    }
}
