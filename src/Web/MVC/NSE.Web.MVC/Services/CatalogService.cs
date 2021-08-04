using NSE.Web.MVC.Models.Product;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Services
{
    public class CatalogService : ServiceBase, ICatalogService
    {
        private readonly HttpClient Client;

        public CatalogService(HttpClient client)
        {
            Client = client;
        }

        public async Task<IEnumerable<ProductViewModel>> SelectProductsAsync()
        {
            var response = await Client.GetAsync("/api/Products");
            var products = await HandleHttpResponseAsync<IEnumerable<ProductViewModel>>(response);
            return products.Data;
        }

        public async Task<ProductViewModel> SelectProductAsync(Guid id)
        {
            var response = await Client.GetAsync($"/api/Products/{id}");
            var product = await HandleHttpResponseAsync<ProductViewModel>(response);
            return product.Data;
        }
    }
}
