namespace BloomService.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Domain.Entities.Concrete;
    using Models;
    using Services.Abstract;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Web.Hosting;
    using Domain.Repositories.Abstract;

    public class TechnicianController : BaseController
    {
        private readonly IImageService imageService;

        private readonly IRepository _repository;

        public TechnicianController(IImageService imageService, IRepository repository)
        {
            this.imageService = imageService;
            _repository = repository;
        }

        [HttpGet]
        [Route("Technician/{id}")]
        public ActionResult GetTechnician(string id)
        {
            var technician = _repository.Get<SageEmployee>(id);
            return Json(technician, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Technician")]
        public ActionResult GetTechnicians()
        {
            var list = _repository.GetAll<SageEmployee>();
            return Json(list.OrderBy(x => x.Employee), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Technician/Save")]
        public ActionResult SaveTechnician(TechnicianModel model)
        {
            var employee = _repository.Get<SageEmployee>(model.Id);
            var technician = Mapper.Map<SageEmployee, EmployeeModel>(employee);
            technician.AvailableDays = model.AvailableDays;
            technician.IsAvailable = model.IsAvailable;
            technician.Picture = model.Picture;

            var pathToIcon = HostingEnvironment.MapPath("/Public/images/technician.png");
            var pathToResultFolder = HostingEnvironment.MapPath("/Public/technician/");
            var pathToResultIcon = pathToResultFolder + technician.Employee + "/" + technician.Employee + ".png";

            if (imageService.CreateIcon(pathToIcon, model.Color, pathToResultIcon, Color.FromArgb(0, 13, 255)))
            {
                technician.Color = model.Color;
            }
            var updatedTechnician = Mapper.Map<EmployeeModel, SageEmployee>(technician);
            _repository.Update(updatedTechnician);

            return Success();
        }
    }
}