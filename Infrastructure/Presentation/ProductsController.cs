﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.ErrorModels;

namespace Presentation
{
    //Api Controller 
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        //endpoint : public non-static method

        [HttpGet] //GET : /api/products
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(PaginationResponse < ProductResultDto >))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ErrorDetails))]
        public async Task<ActionResult<PaginationResponse<ProductResultDto>>> GetAllProducts([FromQuery] ProductSpecificationsParameters specParams)
        {
            var result = await serviceManager.productService.GetAllProductsAsync(specParams);
            return Ok(result); //200

        }

        [HttpGet("{id}")] //GET : /api/products/12
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResultDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var result = await serviceManager.productService.GetProductByIdAsync(id);
            return Ok(result); //200
        }

        [HttpGet("brands")] // GET: api/products/brands
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandResultDto>> GetAllBrands()
        {
            var result = await serviceManager.productService.GetAllBrandsAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("types")] // GET: api/products/types
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<TypeResultDto>> GetAllTypes()
        {
            var result = await serviceManager.productService.GetAllTypesAsync();
            if (result == null) return NotFound();
            return Ok(result);

        }
    }
}
