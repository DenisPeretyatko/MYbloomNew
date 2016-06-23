using System.Linq;

using BloomService.Web.Services.Abstract;
using System.Security.Claims;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using BloomService.Domain.Models.Responses;
using BloomService.Domain.Extensions;
using BloomService.Domain.Models.Requests;
using System.Web.Script.Serialization;
using BloomService.Domain.Repositories.Abstract;
using BloomService.Web.Models;
using BloomService.Domain.Entities.Concrete;
using BloomService.Web.Managers;

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

        public AuthorizationResponse CheckUser(string name, string password)
        {
            var token = GetAuthToken();
            var request = new RestRequest(EndPoints.AuthorizationEndPoint, Method.POST) { RequestFormat = DataFormat.Json };
            var requestBody = new AuthorizationRequest() { Name = name, Password = password };
            request.AddBody(requestBody);
            request.AddHeader("Authorization", token);
            var restClient = new RestClient(_configuration.SageApiHost);
            var response = restClient.Execute<AuthorizationResponse>(request);
            return response.StatusCode != System.Net.HttpStatusCode.OK ? null 
                : new JavaScriptSerializer().Deserialize<AuthorizationResponse>(response.Content);
        }

        public string GetAuthToken()
        {
            var username = _configuration.SageUsername;
            var password = _configuration.SagePassword;
            var sageApiHost = _configuration.SageApiHost; 
            var restClient = new RestClient(sageApiHost);
            var request = new RestRequest("oauth/token", Method.POST);
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("grant_type", "password");
            var response = restClient.Execute(request);
            var json = JObject.Parse(response.Content);
            var result = string.Format("Bearer {0}", json.First.First);
            return result;
        }
    }
}