using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MilkManagement.Core.Configuration;
using MilkManagement.Core.Models;
using MilkManagement.Core.Repositories;
using MilkManagement.Core.Services;
using MilkManagement.Web.Models;

namespace MilkManagement.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IProductService _productService;
        private  ConnectionStringConfiguration _connectionStringConfiguration;
     
        /// <summary>
        /// 
        /// </summary>
        public HomeController(IProductService productService,ConnectionStringConfiguration connectionStringConfiguration)
        {
            _productService = productService;
            _connectionStringConfiguration = connectionStringConfiguration;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Product> Index()
        {
           
            return  await _productService.GetProductById(Guid.NewGuid());
        }
    }
}