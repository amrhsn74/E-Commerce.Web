using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // BaseURL/api/controller => 'Products'
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        // GET: baseURL/api/products
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        // GET: baseURL/api/products/{id}
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductByIdAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("types")]
        // GET: baseURL/api/products/types
        public async Task<ActionResult<IEnumerable<string>>> GetTypes()
        {
            var types = await serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }

        [HttpGet("brands")]
        // GET: baseURL/api/products/brands
        public async Task<ActionResult<IEnumerable<string>>> GetBrands()
        {
            var brands = await serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
    }
}
