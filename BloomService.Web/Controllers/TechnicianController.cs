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
            _repository.Update(updatedTechnician);

            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}