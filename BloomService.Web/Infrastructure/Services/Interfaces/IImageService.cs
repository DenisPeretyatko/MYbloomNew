namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Models;

    public interface IImageService
    {
        bool SavePhotoForWorkOrder(ImageModel model);
        bool BuildTechnicianIcons(TechnicianModel technician);
        List<ImageLocation> GetPhotoForWorkOrder(string idWorkOrder, string prefixUrl = null);
        bool SaveDescriptionsForPhoto(CommentImageModel model);
    }
}
