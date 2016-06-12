using System.Web.Mvc;
using BloomService.Domain.Extensions;
using System.Configuration;
using RestSharp;
using BloomService.Domain.Models.Requests;
using BloomService.Domain.Models.Responses;
using System.Web.Script.Serialization;

namespace BloomService.Web.Controllers
{
    public class AuthorizationController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Authorization/Login/{name}/{password}")]
        public ActionResult Login(string name, string password)
        {
            var token = NinjectWebCommon.GetAuthToken();
            var settings = BloomServiceConfiguration.FromWebConfig(ConfigurationManager.AppSettings);
            var request = new RestRequest(settings.AuthorizationEndPoint, Method.POST) { RequestFormat = DataFormat.Json };
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
            if (results != null)
                return Json("success", JsonRequestBehavior.AllowGet);
            return Json("failed", JsonRequestBehavior.AllowGet);
        }
    }
}