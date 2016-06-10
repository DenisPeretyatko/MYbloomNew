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
    using System.Drawing;
    using System.Drawing.Imaging;
    using System;
    using System.Globalization;
    using System.Web.Hosting;
    [Authorize]
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
            try
            {
                if (model.Color != null)
                {
                    var pathToImage = HostingEnvironment.MapPath("/Images/Technicians/");
                    Image image = Image.FromFile(pathToImage + "technician4.png");
                    Graphics imageGraphics = Graphics.FromImage(image);

                    ColorMap[] colorSwapper = new ColorMap[1];
                    colorSwapper[0] = new ColorMap();
                    colorSwapper[0].OldColor = Color.FromArgb(0, 13, 255);
                    colorSwapper[0].NewColor = System.Drawing.ColorTranslator.FromHtml(model.Color);
                    ImageAttributes imageAttr = new ImageAttributes();
                    imageAttr.SetRemapTable(colorSwapper);
                    imageGraphics.DrawImage(image, new Rectangle(0, 0,
                                                  image.Width, image.Height), 0, 0, image.Width,
                                                  image.Height, GraphicsUnit.Pixel, imageAttr);
                    var fileName = model.Color.Remove(0,1);
                    image.Save(pathToImage + fileName + ".png", ImageFormat.Png);

                }
                technician.Color = model.Color;
            }
            catch
            {

            }
            var updatedTechnician = Mapper.Map<EmployeeModel, SageEmployee>(technician);

            unitOfWork.GetEntities<SageEmployee>().Add(updatedTechnician);

            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}