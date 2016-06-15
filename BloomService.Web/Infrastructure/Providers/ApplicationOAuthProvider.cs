using BloomService.Domain.Extensions;
using BloomService.Domain.Models.Requests;
using BloomService.Domain.Models.Responses;
using BloomService.Web.Managers;
using BloomService.Web.Services.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BloomService.Web.Providers
{
    public static class ExtendedClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(string name, string pass, string id, string type, string mail)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, name));
            claims.Add(new Claim(ClaimTypes.Surname, pass));
            claims.Add(new Claim(ClaimTypes.Role, type));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, id));
            claims.Add(new Claim(ClaimTypes.Email, mail));
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
        private AuthorizationType type;
        private string Name;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public AuthorizationResponse CheckUser(string name, string password)
        {
            var token = AuthorizationService.GetAuthToken();
            var settings = BloomServiceConfiguration.FromWebConfig(ConfigurationManager.AppSettings);
            var request = new RestRequest(EndPoints.AuthorizationEndPoint, Method.POST) { RequestFormat = DataFormat.Json };
            var requestBody = new AuthorizationRequest()
            {
                Name = name,
                Password = password
            };
            request.AddBody(requestBody);
            request.AddHeader("Authorization", token);
            var restClient = new RestClient(settings.SageApiHost);
            var response = restClient.Execute<AuthorizationResponse>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;
            var results = new JavaScriptSerializer().Deserialize<AuthorizationResponse>(response.Content);
            return results;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = CheckUser(context.UserName, context.Password);
            if (user == null)
            {
                return;
            }
            if (user.Id == null)
                user.Id = "";
            if (user.Mail == null)
                user.Mail = "";
            type = user.Type;
            Name = user.Id;
            var identity = new ClaimsIdentity(ExtendedClaimsProvider.GetClaims(context.UserName, context.Password, user.Id, user.Type.ToString(), user.Mail), DefaultAuthenticationTypes.ExternalCookie);
            var ctx = context.OwinContext;
            var authenticationManager = ctx.Authentication;
            var claimsPrincipal = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = claimsPrincipal;
            var ticket = new AuthenticationTicket(identity, null);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            context.AdditionalResponseParameters.Add("Type", type);
            context.AdditionalResponseParameters.Add("Id", Name);
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