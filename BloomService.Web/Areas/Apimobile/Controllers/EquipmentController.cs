using BloomService.Web.Services.Abstract;
using System.Web.Http;

namespace BloomService.Web.Areas.Apimobile.Controllers
{
    [Authorize]
    public class EquipmentsController : ApiController
    {
        readonly IApiMobileService _apiService;

        public EquipmentsController(IApiMobileService apiService)
        {
            _apiService = apiService;
        }

        // GET: Apimobile/Equipment
        public IHttpActionResult Get()
        {
            return Json(_apiService.GetEquipments());
        }
    }
}