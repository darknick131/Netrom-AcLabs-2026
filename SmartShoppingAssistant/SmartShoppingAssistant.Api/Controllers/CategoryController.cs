using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartShoppingAssistant.BusinessLogic.DTOs;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;

namespace SmartShoppingAssistant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        // GET /api/category/3
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGetDTO>> GetById(int id)
        {
            try
            {
                var category = await categoryService.GetByIdAsync(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET /api/category
        public async Task<ActionResult<List<CategoryGetDTO>>> GetAll()
        {
            try
            {
                var categories = await categoryService.GetAllAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST /api/category
        [HttpPost]
        public async Task<ActionResult<CategoryGetDTO>> Create([FromBody] CategoryCreateDTO dto)
        {
            try
            {
                var createdCategory = await categoryService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT /api/category/3
        [HttpPost("{id}")]
        public async Task<ActionResult<CategoryGetDTO>> Update(int id, [FromBody] CategoryUpdateDTO dto)
        {
            try
            {
                var updatedCategory = await categoryService.UpdateAsync(id, dto);
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE /api/category/3
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
