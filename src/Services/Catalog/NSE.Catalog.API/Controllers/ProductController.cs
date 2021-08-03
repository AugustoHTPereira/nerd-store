using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalog.API.Models;
using NSE.Core.Services.Identity;
using System;
using System.Threading.Tasks;

namespace NSE.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository ProductRepository;

        public ProductController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await ProductRepository.SelectAsync());
        }

        [ClaimAuthorize("Catalog", "Read")]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(await ProductRepository.SelectAsync(id));
        }
    }
}
