using System.Web.Mvc;
using BloomService.Domain.Extensions;
using RestSharp;
using BloomService.Domain.Models.Requests;
using BloomService.Domain.Models.Responses;
using System.Web.Script.Serialization;
using BloomService.Web.Services.Concrete;
using BloomService.Web.Managers;
using BloomService.Domain.Repositories.Abstract;
using BloomService.Domain.Entities.Concrete;

namespace BloomService.Web.Controllers
{
    public class AuthorizationController : BaseController
    {
        private readonly BloomServiceConfiguration _configuration;
        private readonly IRepository _repository;

        public AuthorizationController(BloomServiceConfiguration configuration, IRepository repository)
        {
            _configuration = configuration;
        }

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("Authorization/Login/{name}/{password}/{deviceToken?}")]
        //public ActionResult Login(string name, string password, string deviceToken)
        //{
        //    var request = new RestRequest(EndPoints.AuthorizationEndPoint, Method.POST) { RequestFormat = DataFormat.Json };
        //    var requestBody = new AuthorizationRequest()
        //    {
        //        Name = name,
        //        Password = password
        //    };
        //    request.AddBody(requestBody);
        //    request.AddHeader("Authorization", AuthorizationService.GetAuthToken());
        //    var restClient = new RestClient(_configuration.SageApiHost);
        //    var response = restClient.Execute<AuthorizationResponse>(request);
        //    if (response.StatusCode != System.Net.HttpStatusCode.OK)
        //        return null;
        //    var results = new JavaScriptSerializer().Deserialize<AuthorizationResponse>(response.Content);
        //    if (results != null)
        //    {
        //        if(results.Type == AuthorizationType.Technician)
        //        {
        //            var tech = _repository.Get<SageEmployee>(results.Id);
        //            tech.IosDeviceToken = deviceToken;
        //        }
        //        return Json("success", JsonRequestBehavior.AllowGet);
        //    }
        //    return Json("failed", JsonRequestBehavior.AllowGet);
        //}
    }
}