using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BloomService.Domain.Models.Responses;
using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Services.Abstract;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;

namespace BloomService.Web.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAuthorizationService _authorization;

        public ApplicationOAuthProvider()
        {
            _authorization = ComponentContainer.Current.Get<IAuthorizationService>();
            if (_authorization == null)
                throw new ArgumentNullException("_authorization");
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // OAuth2 supports the notion of client authentication
            // this is not used here
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = _authorization.CheckUser(context.UserName, context.Password);
            if (user == null || user.Type!=AuthorizationType.Manager)
            {
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, context.UserName ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Type.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Mail ?? string.Empty)
            };

            context.Validated(new ClaimsIdentity(claims, DefaultAuthenticationTypes.ExternalCookie));
            return Task.FromResult<object>(null);
        }
    }
}