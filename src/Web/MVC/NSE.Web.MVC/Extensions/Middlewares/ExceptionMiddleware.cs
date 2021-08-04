using Microsoft.AspNetCore.Http;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Extensions.Middlewares
{
    public class ExceptionMiddleware
    {
        private RequestDelegate RequestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            RequestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await RequestDelegate(context);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    context.Response.Redirect($"/login?returnTo={context.Request.Path}");
                    return;
                }

                context.Response.Redirect($"/Error/{(int)ex.StatusCode}");
            }
            catch (BrokenCircuitException)
            {
                context.Response.Redirect("/Error/unavailable-service");
            }
            catch (Exception)
            {
                context.Response.Redirect("/Error/500");
            }
        }
    }
}
