using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using BloomService.Domain.Entities;
using BloomService.Domain.Repositories.Abstract;
using BloomService.Domain.UnitOfWork;
using BloomService.Web.Infrastructure;
using BloomService.Web.Models;
using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Controllers
{
    public class TechnicianController : BaseController
    {
        private readonly IEmployeeSageApiService _employeeSageApiService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public TechnicianController(IEmployeeSageApiService employeeSageApiService, IImageService imageService, IUnitOfWork unitOfWork)
        {
            _employeeSageApiService = employeeSageApiService;
            _imageService = imageService;
            _unitOfWork = unitOfWork;
        }

        [GET("Technician")]
        public ActionResult GetTechnicians()
        {
            var list = _employeeSageApiService.Get();
            return Json(list.OrderBy(x=> x.Employee), JsonRequestBehavior.AllowGet);
        }

        [GET("Technician/{id}")]
        public ActionResult GetTechnician(string id)
        {
            var technician = _employeeSageApiService.Get(id);
            return Json(technician, JsonRequestBehavior.AllowGet);
        }

        [POST("Technician/Save")]
        public ActionResult SaveTechniciance(TechnicianModel model)
        {
            var employee = _employeeSageApiService.Get(model.Id);
            var technician = AutoMapper.Mapper.Map<SageEmployee, EmployeeModel>(employee);
            technician.AvailableDays = model.AvailableDays;
            technician.IsAvailable = model.IsAvailable;
            technician.Picture = model.Picture;

            var updatedTechnician = AutoMapper.Mapper.Map<EmployeeModel, SageEmployee>(technician);
            
            _unitOfWork.GetEntities<SageEmployee>().Update(updatedTechnician);
            
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}