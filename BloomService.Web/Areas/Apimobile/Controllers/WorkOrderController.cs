namespace BloomService.Web.Areas.Apimobile.Controllers
{
    using System;
    using System.IO;
    using System.Web.Http;
    using System.Web.Script.Serialization;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    [Authorize]
    public class WorkOrderController : ApiController
    {
        readonly IApiMobileService _apiService;

        public WorkOrderController(IApiMobileService apiService)
        {
            _apiService = apiService;
        }

        public IHttpActionResult Get()
        {
            return Json(_apiService.GetWorkOreders());
        }

        public SageWorkOrder Get(string id)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"public\mock\getWorkorder.json");
            var sr = new StreamReader(path);
            var json = sr.ReadToEnd();
            var workorder = new JavaScriptSerializer().Deserialize<SageWorkOrder>(json);
            return workorder;
        }

        public bool Post(string id)
        {
            return true;
        }
    }
}