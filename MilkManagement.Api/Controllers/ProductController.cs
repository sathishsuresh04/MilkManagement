using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MilkManagement.Core.Entities;
using MilkManagement.Core.Services;
using MilkManagement.Api.Mapper;
using MilkManagement.Api.ViewModels;
using System;

namespace MilkManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetProducts()
        {

            var products = await _productService.GetProducts();
            var productViewModels = ObjectMapper.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

            return Ok(productViewModels);
        }

        [HttpGet("productswithcategory")]
        public async Task<ActionResult> GetProductsWithCategory()
        {

            var products = await _productService.GetProductsWithCategory();
            var productViewModels = ObjectMapper.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

            return Ok(productViewModels);
        }
        [HttpGet("id")]
        public async Task<ActionResult> Getproductbyid(Guid id)
        {

            var products = await _productService.GetProductById(id);
            if (products == null) return NotFound();
            var productViewModels = ObjectMapper.Mapper.Map<Product, ProductViewModel>(products);
            return Ok(productViewModels);
        }




    }
}