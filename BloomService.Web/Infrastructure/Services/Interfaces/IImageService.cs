using BloomService.Web.Models;
using System.Drawing;

namespace BloomService.Web.Services.Abstract
{
    public interface IImageService
    {
        string SavePhotoForWorkOrder(string file, string path, string userId);
        bool BuildTechnicianIcons(TechnicianModel technician);
    }
}
