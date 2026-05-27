using Microsoft.AspNetCore.Mvc;
using SmartShoppingAssistant.BusinessLogic.DTOs.Common;
using SmartShoppingAssistant.BusinessLogic.DTOs.Product;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;

namespace SmartShoppingAssistant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        // GET /api/products/3
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductGetDTO>> GetById(int id)
        {
            try
            {
                var product = await productService.GetByIdAsync(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET /api/products
        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductGetDTO>>> GetAll([FromQuery] QueryParams queryParams)
        {
            if (queryParams.PageSize > 50) queryParams.PageSize = 50;
            var result = await productService.GetAllAsync(queryParams);
            return Ok(result);
        }


        // POST /api/products
        [HttpPost]
        public async Task<ActionResult<ProductGetDTO>> Create([FromBody] ProductCreateDTO dto)
        {
            try
            {
                var createdProduct = await productService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT /api/products/3

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductGetDTO>> Update(int id, [FromBody] ProductUpdateDTO dto)
        {
            try
            {
                var updatedProduct = await productService.UpdateAsync(id, dto);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        // DELETE /api/products/3
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await productService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
