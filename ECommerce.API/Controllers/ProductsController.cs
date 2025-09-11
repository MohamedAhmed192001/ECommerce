using AutoMapper;
using ECommerce.API.Helper;
using ECommerce.Core.DTOs.Images;
using ECommerce.Core.DTOs.Products;
using ECommerce.Core.Entities.Product;
using ECommerce.Core.Interfaces;
using ECommerce.Core.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{

    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        { }

        [HttpGet("get-all")]
        public async Task<IActionResult> get([FromQuery] ProductParams productParams)
        {
            try
            {
                var Products = await work.ProductRepository
                    .GetAllAsync(productParams);

                

                return Ok(new Pagination<ViewProductDto>(productParams.PageNumber, productParams.pageSize, productParams.TotatlCount, Products));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await work.ProductRepository
                .GetByIdAsync(id, x => x.Category, x => x.Photos);

                if (product == null)
                    return NotFound(new ResponseAPI(404));

                var productDto = mapper.Map<ViewProductDto>(product);

                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }


        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(AddProductDto dto)
        {
            try
            {
                await work.ProductRepository.AddAsync(dto);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            try
            {
                await work.ProductRepository.UpdateAsync(dto);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await work.ProductRepository
                    .GetByIdAsync(id, x => x.Photos);
                await work.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
    }
}
