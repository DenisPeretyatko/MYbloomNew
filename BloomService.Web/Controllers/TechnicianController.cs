namespace BloomService.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AttributeRouting.Web.Mvc;

    using AutoMapper;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Models;
    using BloomService.Web.Services.Abstract;

    public class TechnicianController : BaseController
    {
        private readonly IEmployeeService employeeService;

        private readonly IImageService imageService;

        private readonly IUnitOfWork unitOfWork;

        public TechnicianController(
            IEmployeeService employeeService, 
            IImageService imageService, 
            IUnitOfWork unitOfWork)
        {
            this.employeeService = employeeService;
            this.imageService = imageService;
            this.unitOfWork = unitOfWork;
        }

        [GET("Technician/{id}")]
        public ActionResult GetTechnician(string id)
        {
            var technician = employeeService.Get(id);
            return Json(technician, JsonRequestBehavior.AllowGet);
        }

        [GET("Technician")]
        public ActionResult GetTechnicians()
        {
            var list = employeeService.Get();
            return Json(list.OrderBy(x => x.Employee), JsonRequestBehavior.AllowGet);
        }

        [POST("Technician/Save")]
        public ActionResult SaveTechniciance(TechnicianModel model)
        {
            var employee = employeeService.Get(model.Id);
            var technician = Mapper.Map<SageEmployee, EmployeeModel>(employee);
            technician.AvailableDays = model.AvailableDays;
            technician.IsAvailable = model.IsAvailable;
            technician.Picture = model.Picture;
            technician.Color = model.Color;

            var updatedTechnician = Mapper.Map<EmployeeModel, SageEmployee>(technician);

            unitOfWork.GetEntities<SageEmployee>().Add(updatedTechnician);

            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}