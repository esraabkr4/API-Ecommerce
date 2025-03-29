using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) :ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResultDTO>>> GetAllProducts()
        {
            var Products= await _serviceManager._IProductService.GetAllProductsAsync();
            return Ok(Products);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductResultDTO>>> GetAllBrands()
        {
            var Brands = await _serviceManager._IProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductResultDTO>>> GetAllTypes()
        {
            var Types = await _serviceManager._IProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDTO>> GetProductById(int id)
        {
            var Product = await _serviceManager._IProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }
    }
}
