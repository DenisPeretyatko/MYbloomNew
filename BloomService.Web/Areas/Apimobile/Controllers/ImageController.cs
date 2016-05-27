using System.Collections.Generic;
using System.Web.Http;

using BloomService.Web.Models.Request;
using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Areas.Apimobile.Controllers
{
    [Authorize]
    public class ImageController : ApiController
    {
        IApiMobileService _apiService;
        public ImageController(IApiMobileService apiService)
        {
            _apiService = apiService;
        }

        public string Post(ImageRequest model)
        {
            _apiService.AddImage(model);
            return "saved";
        }

    }
}
