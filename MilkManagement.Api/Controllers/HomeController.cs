using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MilkManagement.Core.Configuration;
using MilkManagement.Core.Entities;
using MilkManagement.Core.Services;

namespace MilkManagement.Api.Controllers
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
            return null;
            //  return  await _productService.GetProductById(Guid.NewGuid());
        }
    }
}