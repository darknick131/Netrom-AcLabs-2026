using SmartShoppingAssistant.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.DataAccess.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        //get products with category

        Task<Product> GetProductByIdWithCategoriesAsync(int id);

        // get all products with all categories
        Task<List<Product>> GetAllProductsWithCategoriesAsync();

        // get all products by one category

        Task<List<Product>> GetAllProductsByCategoryAsync(int categoryId);
    }
}
