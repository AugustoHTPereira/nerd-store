using Microsoft.AspNetCore.Http;
using NSE.Web.MVC.Models.Base;
using NSE.Web.MVC.Models.User;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Services
{
    public interface IAuthService
    {
        Task<APIResponseBase> LoginAsync(HttpContext context, LoginViewModel request);
        Task<APIResponseBase> RegisterAsync(RegisterViewModel request);
        Task LogoutAsync(HttpContext context);
    }
}
