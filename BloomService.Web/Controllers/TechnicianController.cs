using BloomService.Web.Infrastructure.Jobs;
using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Infrastructure.SignalR;
using Common.Logging;

namespace BloomService.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using BloomService.Web.Infrastructure.Mongo;

    using Domain.Entities.Concrete;
    using Models;

    public class TechnicianController : BaseController
    {
        private readonly IImageService imageService;
        private readonly IBloomServiceHub hub;
        private readonly IRepository repository;
        private readonly INotificationService notification;
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));


        public TechnicianController(IImageService imageService, IRepository repository, IBloomServiceHub hub, INotificationService notification)
        {
            this.imageService = imageService;
            this.repository = repository;
            this.hub = hub;
            this.notification = notification;
        }

        [HttpGet]
        [Route("Technician/{id}")]
        public ActionResult GetTechnician(string id)
        {
            var technician = repository.Get<SageEmployee>(id);
            return Json(technician, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Technician")]
        public ActionResult GetTechnicians()
        {
            var list = repository.GetAll<SageEmployee>();
            return Json(list.OrderBy(x => x.Employee), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Technician/Save")]
        public ActionResult SaveTechnician(TechnicianModel model)
        {
            _log.InfoFormat("Method: SaveTechnician. Model ID: {0}", model.Id);
            var employee = repository.Get<SageEmployee>(model.Id);
            var assignment = repository.SearchFor<SageAssignment>(e => e.EmployeeId == employee.Employee).FirstOrDefault();
            model.Id = employee.Employee;
            var technician = Mapper.Map<SageEmployee, EmployeeModel>(employee);
            technician.AvailableDays = model.AvailableDays;
            technician.IsAvailable = model.IsAvailable;
            technician.Picture = model.Picture;
            if (model.IsAvailable == false && employee.IsAvailable)
            {
                notification.SendNotification(string.Format("Technician {0} is unavailable", employee.Name));
            }
            if (imageService.BuildTechnicianIcons(model))
            {
                technician.Color = model.Color;
                if (assignment != null) assignment.Color = model.Color;
            }
            var updatedTechnician = Mapper.Map<EmployeeModel, SageEmployee>(technician);
            repository.Update(updatedTechnician);
            repository.Update(assignment);
            hub.UpdateTechnician(model);
            _log.InfoFormat("Repository update technician. Name {0}, ID {1}", updatedTechnician.Name, updatedTechnician.Id);

            return Success();
        }
    }
}