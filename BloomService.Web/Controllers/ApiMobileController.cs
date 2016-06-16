using BloomService.Web.Infrastructure.Jobs;
using Common.Logging;

namespace BloomService.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Extensions;
    using BloomService.Domain.Repositories.Abstract;
    using BloomService.Web.Infrastructure.Services.Abstract;
    using BloomService.Web.Models;
    using BloomService.Web.Models.Request;
    using BloomService.Web.Services.Abstract;
    using System.Security.Claims;
    using System.Threading;
    public class ApiMobileController : BaseController
    {
        private readonly IImageService imageService;
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));

        private readonly IRepository repository;

        private readonly ISageApiProxy sageApiProxy;

        private readonly IAuthorizationService authorizationService;

        private readonly BloomServiceConfiguration settings;

        public ApiMobileController(
            ISageApiProxy sageApiProxy,
            IImageService imageService,
            IRepository repository,
            IAuthorizationService authorizationService,
            BloomServiceConfiguration settings)
        {
            this.sageApiProxy = sageApiProxy;
            this.imageService = imageService;
            this.repository = repository;
            this.settings = settings;
            this.authorizationService = authorizationService;
        }

        [HttpPost]
        [Route("Apimobile/Workorder/{id}/Equipment")]
        public ActionResult AddEquipmentToWorkOrder()
        {
            _log.Info("Method: AddEquipmentToWorkOrder");
            return Success();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Apimobile/Authorization")]
        public ActionResult Get(string name, string password, string deviceToken)
        {
            var token = GetToken(name, password);
            if (token == null)
            {
                return HttpNotFound();
            }
            var employee = repository.SearchFor<SageEmployee>(x => x.Employee == token.Id).FirstOrDefault();
            if (employee != null && !string.IsNullOrEmpty(deviceToken))
                employee.IosDeviceToken = deviceToken;
            repository.Update(employee);
            return Json(token);
        }

        [Route("Apimobile/Equipment/{part}")]
        public ActionResult GetEquipment(string part)
        {
            var result = repository.SearchFor<SageEquipment>(x => x.Part == part);
            return Json(result);
        }

        [Route("Apimobile/Part")]
        public ActionResult GetPart()
        {
            var result = repository.GetAll<SagePart>();
            return Json(result);
        }

        [HttpGet]
        [Route("Apimobile/Workorder")]
        public ActionResult GetWorkOrders()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var userId = authorizationService.GetUser(identity).Name;

            var workOrders = repository.SearchFor<SageWorkOrder>(x => x.Status == "Open").ToList();
            var result = workOrders.Where(x => x.Employee == userId);
            var locations = repository.GetAll<SageLocation>();
            foreach (var order in result)
            {
                order.Equipments = new List<SageEquipment>();

                order.Images = imageService.GetPhotoForWorkOrder(order.WorkOrder, false, settings.BSUrl);
                var location = locations.FirstOrDefault(x => x.Name == order.Location);
                if (location == null)
                {
                    continue;
                }
                order.Latitude = location.Latitude;
                order.Longitude = location.Longitude;
                if (order.Equipment != 0)
                {
                    var equipments = repository.SearchFor<SageEquipment>(x => x.Equipment == order.Equipment.ToString());
                    order.Equipments.AddRange(equipments);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/Image")]
        public ActionResult PostImage(ImageModel model)
        {
            _log.InfoFormat("Method: PostImage. Workorder Id: {0}", model.IdWorkOrder);
            if (imageService.SavePhotoForWorkOrder(model))
            {
                _log.InfoFormat("Add image for workorder success");
                return Success();
            }
            else
                return Error("Add image faild");
        }

        [HttpPost]
        [Route("Apimobile/Location")]
        public ActionResult PostLocation(string technicianId, decimal lat, decimal lng)
        {
            _log.InfoFormat("Method: PostLocation. technicianId: {0}, lat: {1}, lng {2}", technicianId, lat, lng);
            var techLocation = new SageTechnicianLocation
            {
                Employee = technicianId,
                Latitude = lat,
                Longitude = lng,
                Date = DateTime.Now
            };
            repository.Add(techLocation);
            _log.InfoFormat("TechLocation added. TechnicianId: {0}", technicianId);
            return Success();
        }

        [HttpPost]
        [Route("Apimobile/ChangeWorkorderStatus")]
        public ActionResult ChangeWorkorderStatus(string id, string status)
        {
            _log.InfoFormat("Method: ChangeWorkorderStatus. Id: {0}, Status {1}", id, status);
           var workorder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == id).FirstOrDefault();
            if (workorder == null)
                return Error("Workorder not found");
            workorder.Status = status;
            
            //var result = sageApiProxy.EditWorkOrder(workorder);
            //if (!result.IsSucceed)
            //    return Error("Was not able to save workorder to sage");

            repository.Update(workorder);
            _log.InfoFormat("Workorder ({0}) status changed. Status: {1}. Repository updated", workorder.Name, status);
            var workorder2 = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == id).FirstOrDefault();
            return Success();
        }

        private LoginResponseModel GetToken(string mail, string password)
        {
            _log.InfoFormat("Method: GetToken. Mail {0}, password {1}", mail, password);
           ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "username=" + mail;
            postData += "&password=" + password;
            postData += "&grant_type=" + "password";
            byte[] data = encoding.GetBytes(postData);

            var url = ConfigurationManager.AppSettings["BSurl"] + "/apimobile/Token";
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
                _log.InfoFormat("Success. Returned LoginResponseModel (ID: {0})", model.Id);
                return model;
            }
            catch
            {
                _log.ErrorFormat("Method: GetToken. Error. Returned null");
                return null;
            }
        }
    }
}