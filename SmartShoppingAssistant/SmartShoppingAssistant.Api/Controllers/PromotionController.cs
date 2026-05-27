using Microsoft.AspNetCore.Mvc;
using SmartShoppingAssistant.BusinessLogic.DTOs.Common;
using SmartShoppingAssistant.BusinessLogic.DTOs.Promotions;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;

namespace SmartShoppingAssistant.Api.Controllers
{
    [ApiController]
    [Route("api/promotions")]
    public class PromotionController(IPromotionService promotionService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var promotion = await promotionService.GetByIdAsync(id);
                return Ok(promotion);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<PromotionGetDTO>>> GetAll([FromQuery] QueryParams queryParams)
        {
            if (queryParams.PageSize > 50) queryParams.PageSize = 50;
            var result = await promotionService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PromotionCreateDTO dto)
        {
            try
            {
                var created = await promotionService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PromotionUpdateDTO dto)
        {
            try
            {
                var updated = await promotionService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await promotionService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
