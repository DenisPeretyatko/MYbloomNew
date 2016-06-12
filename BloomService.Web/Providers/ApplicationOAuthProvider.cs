using BloomService.Web.Services.Abstract;
using BloomService.Web.Services.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BloomService.Web.Providers
{

    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private readonly IAuthorizationService _authorizationService;

        public ApplicationOAuthProvider(string publicClientId, IAuthorizationService authorizationService)
        {
            if(authorizationService == null)
            {
                throw new ArgumentNullException("authorizationService");
            }
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }
            _authorizationService = authorizationService;
            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = _authorizationService.CheckUser(context.UserName, context.Password);
            if (user == null)
            {
                return;
            }
            var identity = new ClaimsIdentity(_authorizationService.SetClaims(context.UserName, context.Password, user.Id, user.Type.ToString(), user.Mail), DefaultAuthenticationTypes.ExternalCookie);
            var ctx = context.OwinContext;
            var authenticationManager = ctx.Authentication;
            var claimsPrincipal = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = claimsPrincipal;
            var ticket = new AuthenticationTicket(identity, null);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}