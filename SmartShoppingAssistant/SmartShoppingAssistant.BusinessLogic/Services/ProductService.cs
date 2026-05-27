using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.BusinessLogic.DTOs.Common;
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
            var product = await productRepository.GetProductByIdWithCategoriesAsync(id);
            return ProductMapper.ToProductGetDTO(product);
        }

        public async Task<PagedResult<ProductGetDTO>> GetAllAsync(QueryParams queryParams)
        {
            IQueryable<Product> query = productRepository.GetAllAsQueryable()
                .Include(p => p.Categories);

            if (queryParams.CategoryId.HasValue)
                query = query.Where(p => p.Categories.Any(c => c.Id == queryParams.CategoryId.Value));

            if (!string.IsNullOrWhiteSpace(queryParams.Search))
            {
                var term = queryParams.Search.Trim();
                query = query.Where(p =>
                    p.Name.Contains(term) ||
                    (p.Description != null && p.Description.Contains(term)));
            }

            var allowedSort = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "id", "name", "price" };
            var sortBy = allowedSort.Contains(queryParams.SortBy ?? "") ? queryParams.SortBy! : "name";
            var desc = queryParams.SortDir?.ToLower() == "desc";

            query = sortBy.ToLower() switch
            {
                "id"    => desc ? query.OrderByDescending(p => p.Id)    : query.OrderBy(p => p.Id),
                "price" => desc ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                _       => desc ? query.OrderByDescending(p => p.Name)  : query.OrderBy(p => p.Name),
            };

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return new PagedResult<ProductGetDTO>
            {
                Items = items.Select(ProductMapper.ToProductGetDTO).ToList(),
                TotalCount = totalCount,
                Page = queryParams.Page,
                PageSize = queryParams.PageSize,
            };
        }

        public async Task<ProductGetDTO> CreateAsync(ProductCreateDTO productCreateDTO)
        {
            var product = ProductMapper.ToEntity(productCreateDTO);

            foreach (var categoryId in productCreateDTO.CategoryIds)
            {
                var category = await categoryRepository.GetByIdAsync(categoryId);
                product.Categories.Add(category);
            }

            var created = await productRepository.AddAsync(product);
            var withCategories = await productRepository.GetProductByIdWithCategoriesAsync(created.Id);
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
