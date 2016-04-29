using BloomService.Web.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace BloomService.Web.Services.Concrete
{
    public class ImageService : IImageService
    {
        private static readonly Dictionary<Guid, string> _knownImageFormats =
            (from p in typeof(ImageFormat).GetProperties(BindingFlags.Static | BindingFlags.Public)
             where p.PropertyType == typeof(ImageFormat)
             let value = (ImageFormat)p.GetValue(null, null)
             select new { Guid = value.Guid, Name = value.ToString() })
            .ToDictionary(p => p.Guid, p => p.Name);

        //public string UpdateFile(string file, string pathToFolder, string path, string userId)
        //{
        //    if (file != null)
        //    {
        //        if (File.Exists(pathToFolder + path) && !string.IsNullOrEmpty(path))
        //            File.Delete(pathToFolder + path);
        //        return SaveFile(file, pathToFolder, userId);
        //    }
        //    return path;
        //}

        public string SaveFile(string file, string path, string userId)
        {
            if (file == null)
            {
                return string.Empty;
            }
            byte[] imgData = Convert.FromBase64String(file);

            MemoryStream ms = new MemoryStream(imgData, 0,
              imgData.Length);
            ms.Write(imgData, 0, imgData.Length);
            Image image = Image.FromStream(ms, true);
            string name;
            string ext = "jpg";
            if (_knownImageFormats.TryGetValue(image.RawFormat.Guid, out name))
                ext = name.ToLower();
            var di = new DirectoryInfo(path);
            if (!di.Exists)
                di.Create();


            var newPath = userId + "." + ext;
            File.WriteAllBytes(path + newPath, imgData);
            return newPath;
        }
    }
}