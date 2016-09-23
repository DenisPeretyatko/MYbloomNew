namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Models;

    public interface IImageService
    {
        ImageLocation SavePhotoForWorkOrder(ImageModel model);
        bool BuildTechnicianIcons(TechnicianModel technician);
        List<ImageLocation> GetPhotoForWorkOrder(long idWorkOrder, string prefixUrl = null);
        bool SaveDescriptionsForPhoto(CommentImageModel model);
        byte[] CreateArchive(SageImageWorkOrder pictures);
        bool ChangeImageLocation(ImageLocationModel model);
    }
}
