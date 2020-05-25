using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MilkManagement.Core.Entities;
using MilkManagement.Core.Services;
using MilkManagement.Web.Mapper;
using MilkManagement.Web.ViewModels;

namespace MilkManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService,IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAllProducts()
        {
            var products = await _productService.GetProductList();
            var artistResources =ObjectMapper.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

            return Ok(artistResources);
        }



    }
}