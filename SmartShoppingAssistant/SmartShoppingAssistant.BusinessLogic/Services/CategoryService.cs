using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.BusinessLogic.DTOs.Categories;
using SmartShoppingAssistant.BusinessLogic.DTOs.Common;
using SmartShoppingAssistant.BusinessLogic.Mappers;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Repositories;

namespace SmartShoppingAssistant.BusinessLogic.Services
{
    public class CategoryService(IRepository<Category> categoryRepository) : ICategoryService
    {
        public async Task<CategoryGetDTO> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            return CategoryMapper.ToCategoryGetDTO(category);
        }

        public async Task<PagedResult<CategoryGetDTO>> GetAllAsync(QueryParams queryParams)
        {
            var query = categoryRepository.GetAllAsQueryable();

            if (!string.IsNullOrWhiteSpace(queryParams.Search))
            {
                var term = queryParams.Search.Trim();
                query = query.Where(c =>
                    c.Name.Contains(term) ||
                    c.Description.Contains(term));
            }

            var allowedSort = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "id", "name", "description" };
            var sortBy = allowedSort.Contains(queryParams.SortBy ?? "") ? queryParams.SortBy! : "name";
            var desc = queryParams.SortDir?.ToLower() == "desc";

            query = sortBy.ToLower() switch
            {
                "id"          => desc ? query.OrderByDescending(c => c.Id)          : query.OrderBy(c => c.Id),
                "description" => desc ? query.OrderByDescending(c => c.Description) : query.OrderBy(c => c.Description),
                _             => desc ? query.OrderByDescending(c => c.Name)        : query.OrderBy(c => c.Name),
            };

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return new PagedResult<CategoryGetDTO>
            {
                Items = items.Select(CategoryMapper.ToCategoryGetDTO).ToList(),
                TotalCount = totalCount,
                Page = queryParams.Page,
                PageSize = queryParams.PageSize,
            };
        }

        public async Task<CategoryGetDTO> CreateAsync(CategoryCreateDTO categoryCreateDTO)
        {
            var category = CategoryMapper.ToEntity(categoryCreateDTO);
            var created = await categoryRepository.AddAsync(category);
            return CategoryMapper.ToCategoryGetDTO(created);
        }

        public async Task<CategoryGetDTO> UpdateAsync(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            CategoryMapper.ApplyUpdate(category, categoryUpdateDTO);
            var updated = await categoryRepository.UpdateAsync(category);
            return CategoryMapper.ToCategoryGetDTO(updated);
        }

        public async Task DeleteAsync(int id)
        {
            await categoryRepository.DeleteAsync(id);
        }
    }
}
