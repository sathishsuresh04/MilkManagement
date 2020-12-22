using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MilkManagement.Api.Mapper;
using MilkManagement.Api.ViewModels;
using MilkManagement.Core.Entities;
using MilkManagement.Core.Services;

namespace MilkManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>return all categories</returns>
        [HttpGet("")]
        public async Task<ActionResult> GetCategories()
        {
            var categoryList = await _categoryService.GetCategories();
            var categoryViewModels = ObjectMapper.Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categoryList);
            return Ok(categoryViewModels);
        }


    }

}