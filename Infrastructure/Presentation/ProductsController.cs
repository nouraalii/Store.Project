using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    //Api Controller 
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        //endpoint : public non-static method

        [HttpGet] //GET : /api/products
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecificationsParameters specParams)
        {
            var result = await serviceManager.productService.GetAllProductsAsync(specParams);
            if (result == null) return NotFound(); //400
            return Ok(result); //200

        }

        [HttpGet("{id}")] //GET : /api/products/12
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await serviceManager.productService.GetProductByIdAsync(id);
            if (result == null) return NotFound(); //400
            return Ok(result); //200
        }

        [HttpGet("brands")] // GET: api/products/brands
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await serviceManager.productService.GetAllBrandsAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("types")] // GET: api/products/types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await serviceManager.productService.GetAllTypesAsync();
            if (result == null) return NotFound();
            return Ok(result);

        }
    }
}
