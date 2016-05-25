using System.Web.Http;
using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Areas.Apimobile.Controllers
{
    public class LocationController : ApiController
    {
        IApiMobileService _apiService;
        public LocationController(IApiMobileService apiService)
        {
            _apiService = apiService;
        }

        public bool Post(string technicianId, decimal lat, decimal lng)
        {
            _apiService.SaveTechnicianLocation(technicianId, lat, lng);
            return true;
        }
    }
}
