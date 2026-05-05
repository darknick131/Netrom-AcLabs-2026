using SmartShoppingAssistant.BusinessLogic.DTOs.Promotions;
using SmartShoppingAssistant.BusinessLogic.Mappers;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.Services
{
    public class PromotionService(IRepository<Promotion> promotionRepository) : IPromotionService
    {
        public async Task<PromotionGetDTO> GetByIdAsync(int id)
        {
            var promotion = await promotionRepository.GetByIdAsync(id);
            return PromotionMapper.ToPromotionGetDTO(promotion);
        }

        public async Task<List<PromotionGetDTO>> GetAllAsync()
        {
            var promotions = await promotionRepository.GetAllAsync();
            return promotions.Select(PromotionMapper.ToPromotionGetDTO).ToList();
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
    }
}
