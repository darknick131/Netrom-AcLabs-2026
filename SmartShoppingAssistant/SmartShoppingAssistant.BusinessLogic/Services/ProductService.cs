using SmartShoppingAssistant.BusinessLogic.DTOs.Product;
using SmartShoppingAssistant.BusinessLogic.Mappers;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Repositories;

namespace SmartShoppingAssistant.BusinessLogic.Services
{
    public class ProductService(IProductRepository productRepository, IRepository<Category> categoryRepository) : IProductService
    {
        public async Task<ProductGetDTO> GetByIdAsync(int id)
        {
            var product =  await productRepository.GetProductByIdWithCategoriesAsync(id);

            return ProductMapper.ToProductGetDTO(product);
        }

        public async Task<List<ProductGetDTO>> GetAllAsync(int? categoryId = null)
        {
            List<Product> products;

            if (categoryId.HasValue)
                products = await productRepository.GetAllProductsByCategoryAsync(categoryId.Value);
            else
                products = await productRepository.GetAllProductsWithCategoriesAsync();

            // Select each product and convert it to a ProductGetDTO using the mapper, then return the list of DTOs
            return products.Select(ProductMapper.ToProductGetDTO).ToList();
        }

        public async Task<ProductGetDTO> CreateAsync(ProductCreateDTO productCreateDTO)
        {
            // dto -> entity
            var product = ProductMapper.ToEntity(productCreateDTO);

            // fetch and attach categories
            foreach (var categoryId in productCreateDTO.CategoryIds)
            {
                var category = await categoryRepository.GetByIdAsync(categoryId);
                product.Categories.Add(category);
            }

            var createdProduct = await productRepository.AddAsync(product);
            var withCategories = await productRepository.GetProductByIdWithCategoriesAsync(createdProduct.Id);
            return ProductMapper.ToProductGetDTO(withCategories);
        }

        public async Task<ProductGetDTO> UpdateAsync(int id, ProductUpdateDTO productUpdateDTO)
        {
            var product = await productRepository.GetProductByIdWithCategoriesAsync(id);

            ProductMapper.ApplyUpdate(product, productUpdateDTO);

            product.Categories.Clear();
            
            foreach (var categoryId in productUpdateDTO.CategoryIds)
            {
                var category = await categoryRepository.GetByIdAsync(categoryId);
                product.Categories.Add(category);
            }

            var updated = await productRepository.UpdateAsync(product);
            var updatedWithCategories = await productRepository.GetProductByIdWithCategoriesAsync(updated.Id);
            return ProductMapper.ToProductGetDTO(updatedWithCategories);
        }

        public async Task DeleteAsync(int id)
        {
            await productRepository.DeleteAsync(id);
        }

    }
}
