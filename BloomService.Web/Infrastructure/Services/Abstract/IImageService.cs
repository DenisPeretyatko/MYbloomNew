using System.Drawing;

namespace BloomService.Web.Services.Abstract
{
    public interface IImageService
    {
        string SaveFile(string file, string path, string userId);
        bool CreateIcon(string nameIcon, string color, string id, Color oldColor);
    }
}
