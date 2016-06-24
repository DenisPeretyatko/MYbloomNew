using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Script.Serialization;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Extensions;
using BloomService.Domain.Models.Requests;
using BloomService.Domain.Models.Responses;
using BloomService.Domain.Repositories.Abstract;
using BloomService.Web.Managers;
using BloomService.Web.Models;
using BloomService.Web.Services.Abstract;
using Microsoft.Owin.Security;
using RestSharp;

namespace BloomService.Web.Services.Concrete
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IRepository _repository;
        private readonly BloomServiceConfiguration _configuration;

        public AuthorizationService(IRepository repository, BloomServiceConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public UserModel GetUser(ClaimsPrincipal claimsPrincipal)
        {
            var userModel = new UserModel
            {
                Login = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault(),
                Password = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Surname)
                    .Select(c => c.Value).SingleOrDefault(),
                Id = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault(),
                Type = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value).SingleOrDefault(),
                Mail = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Email)
                    .Select(c => c.Value).SingleOrDefault()
            };

            var user = _repository.SearchFor<SageEmployee>(x => x.Employee == userModel.Id).FirstOrDefault();
            if (user != null)
                userModel.Name = user.Name;
            return userModel;
        }

        private AuthorizationResponse CheckUser(string name, string password)
        {
            //var request = new RestRequest(EndPoints.AuthorizationEndPoint, Method.POST) { RequestFormat = DataFormat.Json };
            //var requestBody = new AuthorizationRequest() { Name = name, Password = password };
            //request.AddBody(requestBody);
            //request.AddHeader("Authorization", string.Format("Basic {0}:{1}", _configuration.SageUsername, _configuration.SagePassword));
            //var restClient = new RestClient(_configuration.SageApiHost);
            //var response = restClient.Execute<AuthorizationResponse>(request);

            //return response.StatusCode != System.Net.HttpStatusCode.OK ? null
            //    : new JavaScriptSerializer().Deserialize<AuthorizationResponse>(response.Content);
            var temp = new AuthorizationResponse
            {
                Id = "3242342342",
                Type = AuthorizationType.Manager,
                Mail = "ewgwegweg"
            };
            return temp;
        }

        public AuthorizationResponse Authorization(string login, string password)
        {
            var user = CheckUser(login, password);
            if (user == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Mail ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Type.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Mail ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims, OwinConfig.OAuthOptions.AuthenticationType);

            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            var result = new AuthorizationResponse
            {
                Id = user.Id ?? string.Empty,
                Type = user.Type,
                Mail = user.Mail ?? string.Empty,
                Token = OwinConfig.OAuthOptions.AccessTokenFormat.Protect(ticket)
            };

            return result;
        }

    }
}