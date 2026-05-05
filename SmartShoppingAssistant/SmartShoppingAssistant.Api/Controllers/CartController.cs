using Microsoft.AspNetCore.Mvc;
using SmartShoppingAssistant.BusinessLogic.DTOs.CartItem;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;

namespace SmartShoppingAssistant.Api.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cart = await cartService.GetCartAsync();
            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] CartItemCreateDTO dto)
        {
            try
            {
                var item = await cartService.AddItemAsync(dto);
                return CreatedAtAction(nameof(GetCart), item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("items/{itemId}")]
        public async Task<IActionResult> UpdateItem(int itemId, [FromBody] CartItemUpdateDTO dto)
        {
            try
            {
                var item = await cartService.UpdateItemQuantityAsync(itemId, dto);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            try
            {
                await cartService.RemoveItemAsync(itemId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            await cartService.ClearCartAsync();
            return NoContent();
        }
    }
}
