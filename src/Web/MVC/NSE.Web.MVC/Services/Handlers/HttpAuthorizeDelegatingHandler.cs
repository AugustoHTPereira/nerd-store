using NSE.Web.MVC.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Services.Handlers
{
    public class HttpAuthorizeDelegatingHandler : DelegatingHandler
    {
        private readonly IUser User;

        public HttpAuthorizeDelegatingHandler(IUser user)
        {
            User = user;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", User.Token);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
