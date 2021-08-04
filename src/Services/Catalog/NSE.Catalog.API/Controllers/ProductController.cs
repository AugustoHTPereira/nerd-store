using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalog.API.Models;
using NSE.Core.Controller.Base;
using NSE.Core.Services.Identity;
using System;
using System.Threading.Tasks;

namespace NSE.Catalog.API.Controllers
{
    [Route("api/products")]
    public class ProductController : ApiController
    {
        private readonly IProductRepository ProductRepository;

        public ProductController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var products = await ProductRepository.SelectAsync();
            return ApiResponse("Success", products);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var product = await ProductRepository.SelectAsync(id);
            return ApiResponse("Success", product);
        }
    }
}
