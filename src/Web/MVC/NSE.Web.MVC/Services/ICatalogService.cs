using NSE.Web.MVC.Models.Base;
using NSE.Web.MVC.Models.Product;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Services
{
    public interface ICatalogService
    {
        [Get("/api/products")]
        Task<APIResponseBase<IEnumerable<ProductViewModel>>> GetProductsAsync();

        [Get("/api/products/{id}")]
        Task<APIResponseBase<ProductViewModel>> GetProductAsync(Guid id);
    }
}
