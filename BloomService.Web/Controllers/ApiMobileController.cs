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

    public class ApiMobileController : BaseController
    {
        private readonly IImageService imageService;

        private readonly IRepository repository;

        private readonly ISageApiProxy sageApiProxy;

        private readonly BloomServiceConfiguration settings;

        public ApiMobileController(
            ISageApiProxy sageApiProxy, 
            IImageService imageService, 
            IRepository repository, 
            BloomServiceConfiguration settings)
        {
            this.sageApiProxy = sageApiProxy;
            this.imageService = imageService;
            this.repository = repository;
            this.settings = settings;
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
            var userId = "Rinta, Chriss";
            var result = repository.SearchFor<SageEquipment>(x => x.Employee == userId);
            return Json(result);
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
            var userId = "Rinta, Chriss";

            var workOrders = repository.SearchFor<SageWorkOrder>(x => x.Status == "Open").ToList();
            var result = workOrders.Where(x => x.Employee == userId);
            var locations = repository.GetAll<SageLocation>();
            foreach (var order in result)
            {
                order.Equipments = new List<SageEquipment>();
                var images =
                    repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == order.WorkOrder).SingleOrDefault();
                if (images != null)
                {
                    foreach (var image in images.Images)
                    {
                        image.Image = settings.BSUrl + "/Images/" + image.Image;
                    }

                    order.Images = images.Images;
                }

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
            var pathToImage = HostingEnvironment.MapPath("/Images/");
            var workOrder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.IdWorkOrder).SingleOrDefault();
            if (workOrder == null)
            {
                return Error("Add image faild");
            }

            var imagesDb =
                repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.IdWorkOrder).SingleOrDefault();
            var countImage = 0;
            if (imagesDb != null && imagesDb.Images != null)
            {
                countImage = imagesDb.Images.Count();
            }
            else
            {
                imagesDb = new SageImageWorkOrder
                               {
                                   Images = new List<ImageLocation>(), 
                                   WorkOrder = model.IdWorkOrder, 
                                   WorkOrderBsonId = workOrder.Id
                               };
            }

            var name = model.IdWorkOrder + "id" + countImage;
            var fileName = imageService.SaveFile(model.Image, pathToImage, name);
            var image = new ImageLocation { Image = fileName, Latitude = model.Latitude, Longitude = model.Longitude };
            imagesDb.Images.Add(image);
            repository.Add(imagesDb);
            return Success();
        }

        [HttpPost]
        [Route("Apimobile/Location")]
        public ActionResult PostLocation(string technicianId, decimal lat, decimal lng)
        {
            var techLocation = new SageTechnicianLocation
                                   {
                                       Employee = technicianId, 
                                       Latitude = lat, 
                                       Longitude = lng, 
                                       Date = DateTime.Now
                                   };
            repository.Add(techLocation);
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