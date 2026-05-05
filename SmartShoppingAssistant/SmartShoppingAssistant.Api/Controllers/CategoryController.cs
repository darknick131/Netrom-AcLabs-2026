using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartShoppingAssistant.BusinessLogic.DTOs.Categories;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;

namespace SmartShoppingAssistant.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        // GET /api/categories/3
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

        // GET /api/categories
        [HttpGet]
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

        // PUT /api/categories/3
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryGetDTO>> Update(int id, [FromBody] CategoryUpdateDTO dto)
        {
            try
            {
                var updatedCategory = await categoryService.UpdateAsync(id, dto);
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
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
