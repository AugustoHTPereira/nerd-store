using NSE.Web.MVC.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Services
{
    public abstract class ServiceBase
    {
        protected async Task<APIResponseBase<T>> HandleHttpResponseAsync<T>(HttpResponseMessage response)
            where T : class
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return JsonSerializer.Deserialize<APIResponseBase<T>>(await response.Content.ReadAsStringAsync());

                case HttpStatusCode.NotFound:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.Forbidden:
                    throw new HttpRequestException(message: "Http request error", statusCode: response.StatusCode, inner: null);
            }

            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<APIResponseBase<T>>(await response.Content.ReadAsStringAsync());
        }

        protected async Task<APIResponseBase> HandleHttpResponseAsync(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return JsonSerializer.Deserialize<APIResponseBase>(await response.Content.ReadAsStringAsync());

                case HttpStatusCode.NotFound:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.Forbidden:
                    throw new HttpRequestException(message: "Http request error", statusCode: response.StatusCode, inner: null);
            }

            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<APIResponseBase>(await response.Content.ReadAsStringAsync());
        }
    }
}
