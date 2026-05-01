using SmartShoppingAssistant.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.DataAccess.Repositories
{
    public class ProductRepository(SmartShoppingAssistantDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        //get products with category

        public async Task<Product> GetProductByIdWithCategoriesAsync(int id)
        {
            try
            {
                var    
            }
            catch(Exception ex)
            {
                throw new Exception($"Error retrieving product with id {id} and its categories: {ex.Message}", ex);
            }
        }

        // get all products with all categories
        public async Task<List<Product>> GetAllProductsWithCategoriesAsync()
        {

        }
    }
}
