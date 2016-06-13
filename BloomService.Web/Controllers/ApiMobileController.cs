namespace BloomService.Web.Controllers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Models;
    using BloomService.Web.Models.Request;
    using BloomService.Web.Services.Abstract;

    public class ApiMobileController : BaseController
    {
        private readonly IApiMobileService apiMobileService;

        public ApiMobileController(IApiMobileService apiMobileService)
        {
            this.apiMobileService = apiMobileService;
        }

        [HttpPost]
        [Route("Apimobile/Workorder/{id}/Equipment")]
        public ActionResult AddEquipmentToWorkOrder()
        {
            return Success();
        }

        [HttpGet]
        [Route("Apimobile/Token")]
        public ActionResult Get(string name, string password)
        {
            var token = GetToken(name, password);
            if (token == null)
            {
                return HttpNotFound();
            }

            return Json(token);
        }

        [Route("Apimobile/Equipment")]
        public ActionResult GetEquipment()
        {
            return Json(apiMobileService.GetEquipments());
        }

        [HttpGet]
        [Route("Apimobile/Workorder/{id}")]
        public ActionResult GetWorkOrder(string id)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"public\mock\getWorkorder.json");
            var sr = new StreamReader(path);
            var json = sr.ReadToEnd();
            var workorder = new JavaScriptSerializer().Deserialize<SageWorkOrder>(json);
            return Json(workorder, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Apimobile/Workorder")]
        public ActionResult GetWorkOrders()
        {
            return Json(apiMobileService.GetWorkOreders(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/Image")]
        public ActionResult PostImage(ImageModel model)
        {
            apiMobileService.AddImage(model);
            return Success();
        }

        [HttpPost]
        [Route("Apimobile/Location")]
        public ActionResult PostLocation(string technicianId, decimal lat, decimal lng)
        {
            apiMobileService.SaveTechnicianLocation(technicianId, lat, lng);
            return Success();
        }

        [HttpPost]
        [Route("Apimobile/Workorder/{id}")]
        public ActionResult PostWorkOrder(string id)
        {
            return Success();
        }

        [HttpPost]
        [Route("Apimobile/Token")]
        private LoginResponseModel GetToken(string mail, string password)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "username=" + mail;
            postData += "&password=" + password;
            postData += "&grant_type=" + "password";
            byte[] data = encoding.GetBytes(postData);

            var url = ConfigurationManager.AppSettings["url"] + "apimobile/Token";
            var request = WebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.Authorization, "usernamepassword");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            var newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var dataStream = response.GetResponseStream();

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(LoginResponseModel));
                var model = (LoginResponseModel)ser.ReadObject(dataStream);
                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}