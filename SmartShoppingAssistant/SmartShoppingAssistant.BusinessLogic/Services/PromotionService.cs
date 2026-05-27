using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.BusinessLogic.DTOs.Common;
using SmartShoppingAssistant.BusinessLogic.DTOs.Promotions;
using SmartShoppingAssistant.BusinessLogic.Mappers;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Repositories;

namespace SmartShoppingAssistant.BusinessLogic.Services
{
    public class PromotionService(IPromotionRepository promotionRepository) : IPromotionService
    {
        public async Task<PromotionGetDTO> GetByIdAsync(int id)
        {
            var promotion = await promotionRepository.GetByIdAsync(id);
            return PromotionMapper.ToPromotionGetDTO(promotion);
        }

        public async Task<PagedResult<PromotionGetDTO>> GetAllAsync(QueryParams queryParams)
        {
            var query = promotionRepository.GetAllAsQueryable();

            if (!string.IsNullOrWhiteSpace(queryParams.Search))
            {
                var term = queryParams.Search.Trim();
                query = query.Where(p => p.Name.Contains(term));
            }

            var allowedSort = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "id", "name", "threshold", "rewardvalue" };
            var sortBy = allowedSort.Contains(queryParams.SortBy ?? "") ? queryParams.SortBy! : "name";
            var desc = queryParams.SortDir?.ToLower() == "desc";

            query = sortBy.ToLower() switch
            {
                "id"          => desc ? query.OrderByDescending(p => p.Id)          : query.OrderBy(p => p.Id),
                "threshold"   => desc ? query.OrderByDescending(p => p.Threshold)   : query.OrderBy(p => p.Threshold),
                "rewardvalue" => desc ? query.OrderByDescending(p => p.RewardValue) : query.OrderBy(p => p.RewardValue),
                _             => desc ? query.OrderByDescending(p => p.Name)        : query.OrderBy(p => p.Name),
            };

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return new PagedResult<PromotionGetDTO>
            {
                Items = items.Select(PromotionMapper.ToPromotionGetDTO).ToList(),
                TotalCount = totalCount,
                Page = queryParams.Page,
                PageSize = queryParams.PageSize,
            };
        }

        public async Task<PromotionGetDTO> CreateAsync(PromotionCreateDTO dto)
        {
            var promotion = PromotionMapper.ToEntity(dto);
            var created = await promotionRepository.AddAsync(promotion);
            return PromotionMapper.ToPromotionGetDTO(created);
        }

        public async Task<PromotionGetDTO> UpdateAsync(int id, PromotionUpdateDTO dto)
        {
            var promotion = await promotionRepository.GetByIdAsync(id);
            PromotionMapper.ApplyUpdate(promotion, dto);
            var updated = await promotionRepository.UpdateAsync(promotion);
            return PromotionMapper.ToPromotionGetDTO(updated);
        }

        public async Task DeleteAsync(int id)
        {
            await promotionRepository.DeleteAsync(id);
        }

        public async Task<List<PromotionGetDTO>> GetForProductAsync(int productId)
        {
            var promotions = await promotionRepository.GetForProductAsync(productId);
            return promotions.Select(PromotionMapper.ToPromotionGetDTO).ToList();
        }
    }
}
