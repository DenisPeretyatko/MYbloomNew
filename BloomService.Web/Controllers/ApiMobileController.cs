using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Jobs;
using BloomService.Web.Infrastructure.Services.Interfaces;
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
    using BloomService.Web.Models;

    using System.Security.Claims;
    using System.Threading;
    using Infrastructure.SignalR;
    using RestSharp;
    using Domain.Models.Requests;
    using RestSharp.Serializers;
    using System.Xml.Serialization;

    using BloomService.Web.Infrastructure;
    using BloomService.Web.Infrastructure.Mongo;

    public class ApiMobileController : BaseController
    {
        private readonly IImageService _imageService;

        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));

        private readonly IRepository repository;

        private readonly ISageApiProxy sageApiProxy;

        private readonly IAuthorizationService authorizationService;

        private readonly INotificationService notification;

        private readonly BloomServiceConfiguration settings;

        private readonly IBloomServiceHub _hub;

        public ApiMobileController(
            ISageApiProxy sageApiProxy,
            IImageService imageService,
            IRepository repository,
            IAuthorizationService authorizationService,
            INotificationService notification,
            BloomServiceConfiguration settings,
            IBloomServiceHub hub)
        {
            this.sageApiProxy = sageApiProxy;
            this._imageService = imageService;
            this.repository = repository;
            this.settings = settings;
            this.authorizationService = authorizationService;
            this.notification = notification;
            _hub = hub;
        }

        [HttpPost]
        [Route("Apimobile/Workorder/{id}/Equipment")]
        public ActionResult AddEquipmentToWorkOrder()
        {
            _log.Info("Method: AddEquipmentToWorkOrder");
            return Success();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Apimobile/Authorization")]
        public ActionResult Get(string name, string password, string deviceToken)
        {
            var token = authorizationService.Authorization(name, password);
            if (token == null)
            {
                return Error("Invalid login or password");
            }
            var employee = repository.SearchFor<SageEmployee>(x => x.Employee == token.Id).FirstOrDefault();
            if (employee != null && !string.IsNullOrEmpty(deviceToken))
            {
                employee.IosDeviceToken = deviceToken;
                repository.Update(employee);
            }
            return Json(new { access_token = token.Token }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Apimobile/Equipment/{part}")]
        public ActionResult GetEquipment(string part)
        {
            var result = repository.SearchFor<SageEquipment>(x => x.Part == part);
            return Json(result);
        }

        [HttpGet]
        [Route("Apimobile/Workorder/{id}")]
        public ActionResult GetWorkOrder(long id)
        {
            var workOrder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == id);
            return Json(workOrder, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Apimobile/Part")]
        public ActionResult GetPart()
        {
            var result = repository.GetAll<SagePart>().Where(x => x.PartNumber.StartsWith("R-") && x.Inactive == "No");
            return Json(result);
        }

        [HttpGet]
        [Route("Apimobile/Repair")]
        public ActionResult GetRepair()
        {
            var result = repository.GetAll<SageRepair>();
            return Json(result);
        }

        public class ModelItem
        {
            public string Model { get; set; }
        }

        [HttpGet]
        [Route("Apimobile/Workorder")]
        public ActionResult GetWorkOrders()
        {
            var userId = UserModel.Name;

            var assignments = repository.SearchFor<SageAssignment>(x => x.Employee == userId).ToList();
            var allWorkorders = repository.SearchFor<SageWorkOrder>(x => x.Status == "Open").ToList();

            var result = new List<SageWorkOrder>();

            foreach (var assignment in assignments)
            {
                var workorder = allWorkorders.Where(x => x.WorkOrder == assignment.WorkOrder).SingleOrDefault();
                if (workorder != null)
                {
                    workorder.Images = workorder.Images.OrderBy(x => x.Id).ToList();
                    workorder.ScheduleDate = assignment.Start.TryAsDateTime();
                    result.Add(workorder);
                }
            }

            var locations = repository.GetAll<SageLocation>();
            foreach (var order in result)
            {
                order.Equipments = new List<SageEquipment>();

                order.Images = _imageService.GetPhotoForWorkOrder(order.WorkOrder, settings.SiteUrl);

                var location = locations.FirstOrDefault(x => x.Name == order.Location);
                if (location == null)
                {
                    continue;
                }
                order.Latitude = location.Latitude;
                order.Longitude = location.Longitude;
                order.Address = location.Address;
                if (order.Equipment != 0)
                {
                    var equipments = repository.SearchFor<SageEquipment>(x => x.Equipment == order.Equipment);
                    order.Equipments.AddRange(equipments);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/AddItem")]
        public ActionResult AddWOItem(AddWOItemModel model)
        {
            var items = repository.GetAll<SageWorkOrderItem>().ToList().Where(x => x.WorkOrder == model.WorkOrder);
            var item = items.SingleOrDefault(x => x.WorkOrderItem == model.WorkOrderItem);
            if (item == null)
            {
                var newItem = new SageWorkOrderItem()
                {
                    WorkOrder = model.WorkOrder,
                    WorkOrderItem = items.Any()? items.Max(x => x.WorkOrderItem) + 1:1,
                    WorkDate = model.WorkDate,
                    Description = model.Description,
                    Quantity = model.Quantity,
                    ItemType = model.ItemType,
                    SalesTaxAmount = model.SalesTaxAmount,
                    FlatRateLaborTaxAmt = model.FlatRateLaborTaxAmt
                };
                repository.Add(newItem);
                return Json(newItem, JsonRequestBehavior.AllowGet);
            }
            else
            {
                item.WorkDate = model.WorkDate;
                item.Description = model.Description;
                item.Quantity = model.Quantity;
                item.ItemType = model.ItemType;
                item.SalesTaxAmount = model.SalesTaxAmount;
                item.FlatRateLaborTaxAmt = model.FlatRateLaborTaxAmt;
                repository.Update(item);
            }
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Apimobile/Image")]
        public ActionResult PostImage(ImageModel model)
        {
            _log.InfoFormat("Method: PostImage. Workorder Id: {0}", model.IdWorkOrder);
            var result = _imageService.SavePhotoForWorkOrder(model);
            if (result != null)
            {
                _log.InfoFormat("Add image for workorder success");
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
                return Error("Add image faild");
        }

        [HttpPost]
        [Route("Apimobile/CommentImage")]
        public ActionResult ComentImage(CommentImageModel model)
        {
            if (_imageService.SaveDescriptionsForPhoto(model))
            {
                _log.InfoFormat("Add image for workorder success");
                return Success();
            }
            else
                return Error("Add descriptions image faild");
        }

        [HttpPost]
        [Route("Apimobile/Location")]
        public ActionResult PostLocation(decimal lat, decimal lng)
        {
            var userId = UserModel.Id;
            _log.InfoFormat("Method: PostLocation. technicianId: {0}, lat: {1}, lng {2}", userId, lat, lng);
            
            var techLocation = new SageTechnicianLocation
            {
                Employee = Convert.ToInt64(userId),
                Latitude = lat,
                Longitude = lng,
                Date = DateTime.Now.GetLocalDate()
            };
            repository.Add(techLocation);
            var emploee = repository.SearchFor<SageEmployee>(x => x.Employee.ToString() == userId).Single();
            emploee.Longitude = lat;
            emploee.Latitude = lng;
            _hub.UpdateTechnicianLocation(emploee);
            _log.InfoFormat("TechLocation added. TechnicianId: {0}", userId);
            return Success();
        }

        [HttpPost]
        [Route("Apimobile/ChangeWorkorderStatus")]
        public ActionResult ChangeWorkorderStatus(long id, string status)
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
            notification.SendNotification(string.Format("Workorder {0} change status by {1}", workorder.Name, status));
            return Success();
        }

        //private LoginResponseModel GetToken(string mail, string password)
        //{
        //    _log.InfoFormat("Method: GetToken. Mail {0}, password {1}", mail, password);
        //   ASCIIEncoding encoding = new ASCIIEncoding();
        //    string postData = "username=" + mail;
        //    postData += "&password=" + password;
        //    postData += "&grant_type=" + "password";
        //    byte[] data = encoding.GetBytes(postData);

        //    var url = ConfigurationManager.AppSettings["SiteUrl"] + "/apimobile/Token";
        //    var request = WebRequest.Create(url);
        //    request.Headers.Add(HttpRequestHeader.Authorization, "usernamepassword");
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.Method = "POST";
        //    var newStream = request.GetRequestStream();
        //    newStream.Write(data, 0, data.Length);
        //    newStream.Close();
        //    try
        //    {
        //        var response = (HttpWebResponse)request.GetResponse();
        //        var dataStream = response.GetResponseStream();

        //        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(LoginResponseModel));
        //        var model = (LoginResponseModel)ser.ReadObject(dataStream);
        //        _log.InfoFormat("Success. Returned LoginResponseModel (ID: {0})", model.Id);
        //        return model;
        //    }
        //    catch
        //    {
        //        _log.ErrorFormat("Method: GetToken. Error. Returned null");
        //        return null;
        //    }
        //}
    }
}