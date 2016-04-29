using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Threading;
using Sage.Messaging;
using Sage.WebApi.Infratructure.Constants;
using System.Configuration;

namespace Sage.WebApi.Providers
{
    public static class ExtendedClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(string name, string pass)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, name));
            claims.Add(new Claim(ClaimTypes.Surname, pass));
            return claims;
        }

        public static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }

    }

    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider() { }

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public bool CheckUser(string name, string password, out string resXml)
        {
            MessageTypeDescriptor mtd;
            IMessageBoard mb;
            mtd = new MessageTypeDescriptor();
            var xmlMessage = string.Format(Messages.MessageTypeDescriptor, name, password);
            mtd.Xml = xmlMessage;
            mb = MessageBoardFactory.CreateMessageBoardFromDefaultCatalogFile();
            var catalogPath = ConfigurationManager.AppSettings["catalogPath"];
            mb.RoutingCatalogPersist.LoadFrom(catalogPath);
            var xmlString = Messages.Locations;
            var result = mb.SendMessage(mtd.Xml, xmlString);
            resXml = result;
            if (result.Contains("Status='failed'"))
                return false;
            return true;

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(ExtendedClaimsProvider.GetClaims(context.UserName, context.Password), DefaultAuthenticationTypes.ExternalCookie);
            var ctx = context.OwinContext;
            string resXml;
            if (!CheckUser(context.UserName, context.Password, out resXml))
            {
                context.SetError(resXml);
                return;
            }
            var authenticationManager = ctx.Authentication;
            var claimsPrincipal = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = claimsPrincipal;
            var ticket = new AuthenticationTicket(identity, null);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

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