using System.Web.Mvc;
using BloomService.Domain.Extensions;
using RestSharp;
using BloomService.Domain.Models.Requests;
using BloomService.Domain.Models.Responses;
using System.Web.Script.Serialization;
using BloomService.Web.Services.Concrete;

namespace BloomService.Web.Controllers
{
    public class AuthorizationController : BaseController
    {
        private readonly BloomServiceConfiguration _configuration;

        public AuthorizationController(BloomServiceConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Authorization/Login/{name}/{password}")]
        public ActionResult Login(string name, string password)
        {
            var request = new RestRequest(_configuration.AuthorizationEndPoint, Method.POST) { RequestFormat = DataFormat.Json };
            var requestBody = new AuthorizationRequest()
            {
                Name = name,
                Password = password
            };
            request.AddBody(requestBody);
            request.AddHeader("Authorization", AuthorizationService.GetAuthToken());
            var restClient = new RestClient(_configuration.SageApiHost);
            var response = restClient.Execute<AuthorizationResponse>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;
            var results = new JavaScriptSerializer().Deserialize<AuthorizationResponse>(response.Content);
            if (results != null)
                return Json("success", JsonRequestBehavior.AllowGet);

            return Json("failed", JsonRequestBehavior.AllowGet);
        }
    }
}