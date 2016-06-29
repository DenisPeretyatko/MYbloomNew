using BloomService.Domain.Entities.Concrete;
using BloomService.Web.Models;
using BloomService.Web.Models.Request;
using System.Collections.Generic;

namespace BloomService.Web.Services.Abstract
{
    public interface IImageService
    {
        bool SavePhotoForWorkOrder(ImageModel model);
        bool BuildTechnicianIcons(TechnicianModel technician);
        List<ImageLocation> GetPhotoForWorkOrder(string idWorkOrder, string prefixUrl = null);
        bool SaveDescriptionsForPhoto(CommentImageModel model);
    }
}
