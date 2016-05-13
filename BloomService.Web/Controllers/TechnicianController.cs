using System.Web.Mvc;

using AttributeRouting.Web.Mvc;

using BloomService.Domain.UnitOfWork;
using BloomService.Web.Models;
using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Controllers
{
    using System.Linq;

    using BloomService.Domain.Entities.Concrete;

    public class TechnicianController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public TechnicianController(IEmployeeService employeeService, IImageService imageService, IUnitOfWork unitOfWork)
        {
            this._employeeService = employeeService;
            this._imageService = imageService;
            this._unitOfWork = unitOfWork;
        }

        [GET("Technician")]
        public ActionResult GetTechnicians()
        {
            var list = this._employeeService.Get();
            return Json(list.OrderBy(x=> x.Employee), JsonRequestBehavior.AllowGet);
        }

        [GET("Technician/{id}")]
        public ActionResult GetTechnician(string id)
        {
            var technician = this._employeeService.Get(id);
            return Json(technician, JsonRequestBehavior.AllowGet);
        }

        [POST("Technician/Save")]
        public ActionResult SaveTechniciance(TechnicianModel model)
        {

            var employee = this._employeeService.Get(model.Id);
            var technician = AutoMapper.Mapper.Map<SageEmployee, EmployeeModel>(employee);
            technician.AvailableDays = model.AvailableDays;
            technician.IsAvailable = model.IsAvailable;
            technician.Picture = model.Picture;
            technician.Color = model.Color;

            var updatedTechnician = AutoMapper.Mapper.Map<EmployeeModel, SageEmployee>(technician);
            
            this._unitOfWork.GetEntities<SageEmployee>().Update(updatedTechnician);
            
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}
