using Microsoft.AspNetCore.Mvc;
using NSE.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("{code}")]
        public IActionResult Index([FromRoute] string code, [FromQuery] string exception)
        {
            var error = new ErrorViewModel
            {
                Message = exception,
            };

            switch (code)
            {
                case "500":
                    return View("InternalServerError", error);

                case "404":
                    return View("NotFound", error);

                case "403":
                    return View("Forbid", error);

                case "401":
                    return View("Unauthorized", error);

                case "400":
                    return View("BadRequest", error);

                case "unavailable-service":
                    return View("Unavailable");

                default:
                    break;
            }

            return View(error);
        }
    }
}
