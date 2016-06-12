using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

using BloomService.Web.Services.Abstract;
using System.Drawing.Drawing2D;

namespace BloomService.Web.Services.Concrete
{
    public class ImageService : IImageService
    {
        private static readonly Dictionary<Guid, string> _knownImageFormats = new Dictionary<Guid, string>()
        {
            { ImageFormat.Bmp.Guid, "bmp"},
            { ImageFormat.Jpeg.Guid, "jpeg"},
            { ImageFormat.Png.Guid, "png"}
        };
    
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

        public bool CreateIcon(string pathToIcon, string color, string resultIconPath, Color oldColor)
        {
            try
            {
                if (color != null)
                {
                    Image image = Image.FromFile(pathToIcon);
                    Graphics imageGraphics = Graphics.FromImage(image);

                    ColorMap[] colorSwapper = new ColorMap[1];
                    colorSwapper[0] = new ColorMap();
                    colorSwapper[0].OldColor = oldColor;
                    colorSwapper[0].NewColor = ColorTranslator.FromHtml(color);
                    ImageAttributes imageAttr = new ImageAttributes();
                    imageAttr.SetRemapTable(colorSwapper);
                    imageGraphics.DrawImage(image, new Rectangle(0, 0,
                                                  image.Width, image.Height), 0, 0, image.Width,
                                                  image.Height, GraphicsUnit.Pixel, imageAttr);
                    image.Save(resultIconPath, ImageFormat.Png);

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = size.Width / (float)originalWidth;
                float percentHeight = size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }

            Image newImage = new Bitmap(newWidth, newHeight);
            using (var graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }



        private Image ValidateImage(HttpPostedFileBase file)
        {
            try
            {
                var img = Image.FromStream(file.InputStream, true, true);
                if (IsOneOfValidFormats(img.RawFormat))
                {
                    return img;
                }
            }
            catch
            {
            }
            return null;
        }

        private bool IsOneOfValidFormats(ImageFormat rawFormat)
        {
            return new[] { ImageFormat.Png, ImageFormat.Jpeg, ImageFormat.Gif }.Contains(rawFormat);
        }
    }
}