using AutoMapper;
using ECommerce.API.Helper;
using ECommerce.Core.DTOs.Categories;
using ECommerce.Core.Entities.Product;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        { }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await work.CategoryRepository.GetAllAsync();
                if(categories == null || !categories.Any())
                    return NotFound(new ResponseAPI(404));
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400));
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await work.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                    return NotFound(new ResponseAPI(404));
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400));
            }
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory(AddCategoryDto dto)
        {
            try
            {
                var category = mapper.Map<Category>(dto);

                await work.CategoryRepository.AddAsync(category);

                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400));
            }
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            try
            {
                var category = mapper.Map<Category>(dto);

                await work.CategoryRepository.UpdateAsync(category);

                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, "Something wrong when updating category try again."));
            }
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await work.CategoryRepository.DeleteAsync(id);
                return Ok(new ResponseAPI(200));

            }
            catch (Exception ex)
            {
                return NotFound(new ResponseAPI(404, "Category not found."));
            }
        }

    }
}
