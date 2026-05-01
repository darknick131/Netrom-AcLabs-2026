using SmartShoppingAssistant.BusinessLogic.DTOs;
using SmartShoppingAssistant.BusinessLogic.Mappers;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Repositories;

namespace SmartShoppingAssistant.BusinessLogic.Services
{
    public class ProductService(IRepository<Product> productRepository, IRepository<Category> categoryRepository) : IProductService
    {
        public async Task<ProductGetDTO> GetByIdAsync(int id)
        {
            var product =  await productRepository.GetByIdAsync(id);

            return ProductMapper.ToProductGetDTO(product);
        }

        public async Task<List<ProductGetDTO>> GetAllAsync()
        {
            var products = await productRepository.GetAllAsync();

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

            // save to database
            var createdProduct = await productRepository.AddAsync(product);

            // entity -> dto
            return ProductMapper.ToProductGetDTO(createdProduct);
        }

        public async Task<ProductGetDTO> UpdateAsync(int id, ProductUpdateDTO productUpdateDTO)
        {

            var product = await productRepository.GetByIdAsync(id);

            ProductMapper.ApplyUpdate(product, productUpdateDTO);

            var updated = await productRepository.UpdateAsync(product);

            return ProductMapper.ToProductGetDTO(updated);
        }

        public async Task DeleteAsync(int id)
        {
            await productRepository.DeleteAsync(id);
        }

    }
}
