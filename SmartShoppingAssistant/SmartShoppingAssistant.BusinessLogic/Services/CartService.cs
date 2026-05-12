using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;
using SmartShoppingAssistant.BusinessLogic.Agents;
using SmartShoppingAssistant.BusinessLogic.DTOs.CartItem;
using SmartShoppingAssistant.BusinessLogic.Mappers;
using SmartShoppingAssistant.BusinessLogic.Models;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Entities.Enums;
using SmartShoppingAssistant.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SmartShoppingAssistant.BusinessLogic.Services
{
    public class CartService(
        ICartItemRepository cartItemRepository,
        IPromotionRepository promotionRepository,
        IPromotionCheckerAgent promotionCheckerAgent,
        ISuggestionComposerAgent suggestionComposerAgent,
        ICategoryService categoryService) :     ICartService
    {
        public async Task<CartGetDTO> GetCartAsync()
        {
            var cartItems = await cartItemRepository.GetAllWithProductsAsync();
            var itemDtos = cartItems.Select(CartItemMapper.ToCartItemGetDTO).ToList();

            var subtotal = itemDtos.Sum(i => i.ItemTypeTotal);
            var discount = await CalculateDiscountAsync(cartItems, subtotal);

            return new CartGetDTO
            {
                Items = itemDtos,
                Subtotal = subtotal,
                Discount = discount,
                Total = subtotal - discount
            };
        }

        public async Task<CartItemGetDTO> AddItemAsync(CartItemCreateDTO dto)
        {
            // verificam daca mai e produsul in cos
            var allItems = await cartItemRepository.GetAllWithProductsAsync();
            var existing = allItems.FirstOrDefault(ci => ci.ProductId == dto.ProductId);

            if (existing != null)
            {
                existing.Quantity += dto.Quantity;
                await cartItemRepository.UpdateAsync(existing);
                return CartItemMapper.ToCartItemGetDTO(existing);
            }

            var newItem = CartItemMapper.ToEntity(dto);
            var created = await cartItemRepository.AddAsync(newItem);
            var withProduct = await cartItemRepository.GetByIdWithProductAsync(created.Id);
            return CartItemMapper.ToCartItemGetDTO(withProduct);
        }

        public async Task<CartItemGetDTO> UpdateItemQuantityAsync(int itemId, CartItemUpdateDTO dto)
        {
            var item = await cartItemRepository.GetByIdWithProductAsync(itemId);
            item.Quantity = dto.Quantity;
            await cartItemRepository.UpdateAsync(item);
            return CartItemMapper.ToCartItemGetDTO(item);
        }

        public async Task RemoveItemAsync(int itemId)
        {
            await cartItemRepository.DeleteAsync(itemId);
        }

        public async Task ClearCartAsync()
        {
            await cartItemRepository.DeleteAllAsync();
        }

        private async Task<decimal> CalculateDiscountAsync(List<CartItem> cartItems, decimal subtotal)
        {
            var promotions = await promotionRepository.GetAllAsync();
            if (!promotions.Any())
                return 0m;

            return promotions
                .Where(p => p.IsActive)
                .Sum(p => CalculateSinglePromotion(p, cartItems, subtotal));
        }

        private decimal CalculateSinglePromotion(Promotion promotion, List<CartItem> cartItems, decimal subtotal)
        {
            List<CartItem> applicableItems;

            if (promotion.ProductId.HasValue)
            {
                applicableItems = cartItems
                    .Where(ci => ci.ProductId == promotion.ProductId.Value)
                    .ToList();
            }
            else if (promotion.CategoryId.HasValue)
            {
                applicableItems = cartItems
                    .Where(ci => ci.Product.Categories.Any(c => c.Id == promotion.CategoryId.Value))
                    .ToList();
            }
            else
            {
                applicableItems = cartItems;
            }

            if (!applicableItems.Any())
                return 0m;

            var applicableQuantity = applicableItems.Sum(ci => ci.Quantity);
            var applicableTotal = applicableItems.Sum(ci => ci.Product.Price * ci.Quantity);

            bool thresholdMet = promotion.Type switch
            {
                PromotionType.Quantity => applicableQuantity >= promotion.Threshold,
                PromotionType.CartTotal => subtotal >= promotion.Threshold,
                _ => false
            };

            if (!thresholdMet)
                return 0m;

            return promotion.Reward switch
            {
                // N iteme gratis: reducere = pretul minim din iteme aplicabile * N
                PromotionReward.FreeItems => applicableItems.Min(ci => ci.Product.Price) * promotion.RewardValue,
                // N% reducere pe totalul itemelor aplicabile
                PromotionReward.PercentDiscount => applicableTotal * (promotion.RewardValue / 100m),
                _ => 0m
            };
        }

        public async Task<AnalysisResponse> AnalyzeCartAsync()
        {
            var cart = await cartItemRepository.GetAllWithProductWithCategoriesAsync();
            var categories = await categoryService.GetAllAsync();

            var cartJson = JsonSerializer.Serialize(cart.Select(c => new
            {
                c.ProductId,
                c.Product.Name,
                c.Product.Price,
                c.Quantity,
                LineTotal = c.Product.Price * c.Quantity,
                Categories = c.Product.Categories.Select(cat => new { CategoryId = cat.Id, CategoryName = cat.Name }).ToList(),
            }));

            var categoryJson = JsonSerializer.Serialize(categories.Select(c => new
            {
                CategoryId = c.Id,
                CategoryName = c.Name
            }));

            var promotionAgent = promotionCheckerAgent.Build(cartJson);
            var suggestionAgent = suggestionComposerAgent.Build(cartJson, categoryJson);

            var workflow = new WorkflowBuilder(promotionAgent)
                .AddEdge(promotionAgent, suggestionAgent)
                .WithOutputFrom(suggestionAgent)
                .Build();

            var chatMessage = new List<ChatMessage>
            {
                new(ChatRole.User, "Analyze the cart and suggest improvements")
            };

            await using var result = await InProcessExecution.RunStreamingAsync(workflow, chatMessage);

            await result.TrySendMessageAsync(new TurnToken(emitEvents: true));

            var jsonBuilder = new StringBuilder();

            await foreach (var message in result.WatchStreamAsync())
            {
                if (message is AgentResponseUpdateEvent update && update.ExecutorId.StartsWith("SuggestionComposer"))
                {
                    jsonBuilder.Append(update.Update.Text);
                }
                else if (message is WorkflowErrorEvent errorEvent)
                {
                    throw new InvalidOperationException(errorEvent.Exception?.Message);
                }
            }

            var json = jsonBuilder.ToString();

            return JsonSerializer.Deserialize<AnalysisResponse>(json)
                ?? throw new InvalidOperationException("Failed to deserialize analysis response.");
        }
    }
}
