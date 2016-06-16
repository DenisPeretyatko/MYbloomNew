using BloomService.Web.Infrastructure.Jobs;
using Common.Logging;

namespace BloomService.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Domain.Entities.Concrete;
    using Models;
    using Services.Abstract;
    using Domain.Repositories.Abstract;
    public class TechnicianController : BaseController
    {
        private readonly IImageService imageService;

        private readonly IRepository repository;
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));


        public TechnicianController(IImageService imageService, IRepository repository)
        {
            this.imageService = imageService;
            this.repository = repository;
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
            model.Id = employee.Employee;
            var technician = Mapper.Map<SageEmployee, EmployeeModel>(employee);
            technician.AvailableDays = model.AvailableDays;
            technician.IsAvailable = model.IsAvailable;
            technician.Picture = model.Picture;
            
            if (imageService.BuildTechnicianIcons(model))
            {
                technician.Color = model.Color;
            }
            var updatedTechnician = Mapper.Map<EmployeeModel, SageEmployee>(technician);
            repository.Update(updatedTechnician);
            _log.InfoFormat("Repository update technician. Name {0}, ID {1}", updatedTechnician.Name, updatedTechnician.Id);

            return Success();
        }
    }
}