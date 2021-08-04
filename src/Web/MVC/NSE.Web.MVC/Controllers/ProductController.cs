using Microsoft.AspNetCore.Mvc;
using NSE.Web.MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICatalogService CatalogService;

        public ProductController(ICatalogService catalogService)
        {
            CatalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await CatalogService.SelectProductsAsync();
            return View(products);
        }

        [HttpGet]
        [Route("products/{id:Guid}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            var product = await CatalogService.SelectProductAsync(id);
            return View(product);
        }
    }
}
