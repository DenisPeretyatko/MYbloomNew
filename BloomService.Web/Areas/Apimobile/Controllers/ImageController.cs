using System.Collections.Generic;
using System.Web.Http;
using BloomService.Web.Services.Abstract;
using BloomService.Web.Models.Request;

namespace BloomService.Web.Areas.Apimobile.Controllers
{
    public class ImageController : ApiController
    {
        IAPIMobileService _apiService;
        public ImageController(IAPIMobileService apiService)
        {
            _apiService = apiService;
        }
        public string Post(ImageRequest model)
        {
            _apiService.AddImage(model.Images, model.IdWorkOrder);
            return "saved";
        }

    }
}
