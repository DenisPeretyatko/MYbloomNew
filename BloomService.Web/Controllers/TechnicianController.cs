using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Infrastructure.SignalR;
using Common.Logging;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BloomService.Domain.Entities.Concrete;
using BloomService.Web.Infrastructure.Mongo;
using BloomService.Web.Models;

namespace BloomService.Web.Controllers
{
    public class TechnicianController : BaseController
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(TechnicianController));
        private readonly IImageService _imageService;
        private readonly IBloomServiceHub _hub;
        private readonly IRepository _repository;
        private readonly INotificationService _notification;

        public TechnicianController(IImageService imageService,
            IRepository repository,
            IBloomServiceHub hub,
            INotificationService notification)
        {
            _imageService = imageService;
            _repository = repository;
            _hub = hub;
            _notification = notification;
        }

        [HttpGet]
        [Route("Technician/{id}")]
        public ActionResult GetTechnician(string id)
        {
            var technician = _repository.Get<SageEmployee>(id);
            return technician == null ?
                Error("Technician does not exist", $"There are no Technician with technicianID: {id}. technician == null") :
                Success(technician);
        }

        [HttpGet]
        [Route("Technician")]
        public ActionResult GetTechnicians()
        {
            var technicians = _repository.GetAll<SageEmployee>().OrderBy(x => x.Employee);
            return Success(technicians);
        }

        [HttpPost]
        [Route("Technician/Save")]
        public ActionResult SaveTechnician(TechnicianModel model)
        {
            _log.InfoFormat("Method: SaveTechnician. Model ID: {0}", model.Id);
            var employee = _repository.Get<SageEmployee>(model.Id);
            var assignment = _repository.SearchFor<SageAssignment>(e => e.EmployeeId == employee.Employee).FirstOrDefault();
            model.Id = employee.Employee.ToString();
            var technician = Mapper.Map<SageEmployee, EmployeeModel>(employee);
            technician.AvailableDays = model.AvailableDays;
            technician.IsAvailable = model.IsAvailable;
            technician.Picture = model.Picture;
            if (model.IsAvailable == false && employee.IsAvailable)
            {
                _notification.SendNotification($"Technician {employee.Name} is unavailable");
            }
            if (_imageService.BuildTechnicianIcons(model))
            {
                technician.Color = model.Color;
                if (assignment != null) assignment.Color = model.Color;
            }
            var updatedTechnician = Mapper.Map<EmployeeModel, SageEmployee>(technician);
            _repository.Update(updatedTechnician);
            _repository.Update(assignment);
            _hub.UpdateTechnician(model);
            _log.InfoFormat("Repository update technician. Name {0}, ID {1}", updatedTechnician.Name, updatedTechnician.Id);
            return Success();
        }
    }
}