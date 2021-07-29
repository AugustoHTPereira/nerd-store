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
        [Route("{code:int}")]
        public IActionResult Index([FromRoute] int code)
        {
            if (code == 500)
                return View("InternalServerError", new ErrorViewModel
                {
                    RequestId = Guid.NewGuid().ToString(),
                    Message = "Failure"
                });

            return View();
        }
    }
}
