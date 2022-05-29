using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.VisualStudio.Web.CodeGeneration;
using MVCWeb.Services;
using MVCWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MVCWeb.Helper.Handler
{
    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IIdentiyService _identityService;
        private readonly ILogger<ResourceOwnerPasswordTokenHandler> _logger;
        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContext, IIdentiyService identityService, ILogger<ResourceOwnerPasswordTokenHandler> logger)
        {
            _httpContext = httpContext;
            _identityService = identityService;
            _logger = logger;
        }
        protected async override  Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _httpContext.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var tokenResponse = await _identityService.GetAccessTokenByRefreshToken();

                if (tokenResponse is not null)
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
                    response = await base.SendAsync(request, cancellationToken);


                }

                //return response;

            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //return exp;
                throw new UnauthorizedAccessException();
            }

            return response;

        }
    }
}
