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
            var employee = repository.Get<SageEmployee>(model.Id);
            var technician = Mapper.Map<SageEmployee, EmployeeModel>(employee);
            technician.AvailableDays = model.AvailableDays;
            technician.IsAvailable = model.IsAvailable;
            technician.Picture = model.Picture;
            
            if (imageService.BuildTechnicianColor(model))
            {
                technician.Color = model.Color;
            }
            var updatedTechnician = Mapper.Map<EmployeeModel, SageEmployee>(technician);
            repository.Update(updatedTechnician);

            return Success();
        }
    }
}