﻿using Learnings.Application.Dtos.ProductsDto;
using Learnings.Application.ResponseBase;
using Learnings.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learnings.Api.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseBase<AddProductDto>>> Create([FromBody] AddProductDto dto)
        {
            var response = await _productService.CreateProduct(dto);
            return StatusCode((int)response.Status, response);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseBase<List<AddProductDto>>>> GetAll()
        {
            var response = await _productService.GetAllProducts();
            return StatusCode((int)response.Status, response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBase<AddProductDto>>> GetProductById([FromRoute(Name = "id")] Guid productId)
        {
            var response = await _productService.GetSingleProduct(productId);
            return StatusCode((int)response.Status, response);
        }
    }
}
