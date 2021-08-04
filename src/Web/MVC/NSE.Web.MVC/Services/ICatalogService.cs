using NSE.Web.MVC.Models.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> SelectProductsAsync();
        Task<ProductViewModel> SelectProductAsync(Guid id);
    }
}
