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

        IRepository _repository;

        public AuthorizationService(IRepository repository)
        {
            _repository = repository;
        }

        public UserModel GetUser(ClaimsPrincipal claimsPrincipal)
        {
            var userModel = new UserModel();

            userModel.Login = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Name)
                               .Select(c => c.Value).SingleOrDefault();
            userModel.Password = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Surname)
                               .Select(c => c.Value).SingleOrDefault();
            userModel.Id = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                               .Select(c => c.Value).SingleOrDefault();
            userModel.Type = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Role)
                               .Select(c => c.Value).SingleOrDefault();
            userModel.Mail = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();

            var user = _repository.SearchFor<SageEmployee>(x => x.Employee == userModel.Id).FirstOrDefault();
            if (user != null)
                userModel.Name = user.Name;
            return userModel;
        }

        public IEnumerable<Claim> SetClaims(string name, string pass, string id, string type, string mail)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, name));
            claims.Add(new Claim(ClaimTypes.Surname, pass));
            claims.Add(new Claim(ClaimTypes.Role, type));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, id));
            claims.Add(new Claim(ClaimTypes.Email, mail));
            return claims;
        }

        public Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }

        public AuthorizationResponse CheckUser(string name, string password)
        {
            var token = GetAuthToken();
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

        public static string GetAuthToken()
        {
            var username = ConfigurationManager.AppSettings["SageUsername"];
            var password = ConfigurationManager.AppSettings["SagePassword"];
            var sageApiHost = ConfigurationManager.AppSettings["SageApiHost"];
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