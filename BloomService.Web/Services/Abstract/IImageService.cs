using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomService.Web.Services.Abstract
{
    public interface IImageService
    {
        string SaveFile(string file, string path, string userId);
    }
}
