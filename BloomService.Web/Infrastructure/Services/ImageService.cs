using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

using BloomService.Web.Services.Abstract;
using System.Drawing.Drawing2D;
using BloomService.Web.Models;
using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Models.Request;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Repositories.Abstract;
using BloomService.Domain.Extensions;
using System.Configuration;

namespace BloomService.Web.Services.Concrete
{
    public class ImageService : IImageService
    {
        private static readonly Dictionary<ImageFormat, string> _knownImageFormats = new Dictionary<ImageFormat, string>()
        {
            { ImageFormat.Bmp, "bmp"},
            { ImageFormat.Jpeg, "jpg"},
            { ImageFormat.Png, "png"}
        };

        private readonly IHttpContextProvider httpContextProvider;
        private readonly IRepository repository;

        private readonly string urlToTechnicianIcon = "/Public/images/technician.png";
        private readonly string urlToWorkOrderIcon = "/Public/images/workorder.png";
        private readonly string urlToFolderTecnician = "/Public/technician/";
        private readonly string urlToFolderWorkOrder = "/Public/workorder/";
        private readonly string urlToFolderPhotoWorkOrders = "/Public/images/";
        private readonly Color colorTechnicianIcon = Color.FromArgb(0, 13, 255);
        private readonly Color colorWorkOrderIcon = Color.FromArgb(255, 0, 4);
        private readonly BloomServiceConfiguration settings;

        public ImageService(IHttpContextProvider httpContextProvider, IRepository repository)
        {
            this.httpContextProvider = httpContextProvider;
            this.repository = repository;
            settings = BloomServiceConfiguration.FromWebConfig(ConfigurationManager.AppSettings);
        }

        public bool SavePhotoForWorkOrder(ImageModel model)
        {
            var workOrder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.IdWorkOrder).SingleOrDefault();
            if (workOrder == null)
            {
                return false;
            }

            var imagesDb = repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.IdWorkOrder).SingleOrDefault();
            var countImage = 0;
            if (imagesDb != null && imagesDb.Images != null)
            {
                countImage = imagesDb.Images.Count();
            }
            else
            {
                imagesDb = new SageImageWorkOrder
                {
                    Images = new List<ImageLocation>(),
                    WorkOrder = model.IdWorkOrder,
                    WorkOrderBsonId = workOrder.Id,
                };
            }

            var pathToImage = string.Format("{0}/{1}/", httpContextProvider.MapPath(urlToFolderPhotoWorkOrders), model.IdWorkOrder);
            var nameBig = countImage.ToString();
            var nameSmall = "small" + countImage;
            var fileName = SavePhotoForWorkOrder(model.Image, pathToImage, nameBig, settings.SizeBigPhoto);
            var fileNameSmall = SavePhotoForWorkOrder(model.Image, pathToImage, nameSmall, settings.SizeSmallPhoto);
            var maxId = imagesDb.Images.Any() ? imagesDb.Images.Max(x => x.Id) : 0;
            var image = new ImageLocation { Image = fileNameSmall, BigImage = fileName, Latitude = model.Latitude, Longitude = model.Longitude, Id = maxId + 1 };
            imagesDb.Images.Add(image);
            repository.Add(imagesDb);
            return true;
        }

        public bool SaveDescriptionsForPhoto(CommentImageModel model)
        {
            var imagesDb = repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.IdWorkorder).SingleOrDefault();
            if (imagesDb != null && imagesDb.Images != null)
            {
                var image = imagesDb.Images.FirstOrDefault(x => x.Id == model.IdImage);
                if (image != null)
                {
                    imagesDb.Images.Remove(image);
                    image.Description = model.Description;
                    imagesDb.Images.Add(image);
                    repository.Update(imagesDb);
                    return true;
                }
            }
            return false;
        }

        public List<ImageLocation> GetPhotoForWorkOrder(string idWorkOrder, string prefixUrl = null)
        {
            var pathToImage = string.Format("{0}{1}/", urlToFolderPhotoWorkOrders, idWorkOrder);
            if (prefixUrl != null)
                pathToImage = prefixUrl + pathToImage;

            var images = repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == idWorkOrder).SingleOrDefault();
            if (images != null)
            {
                foreach (var image in images.Images)
                {
                    image.Image = pathToImage + image.Image;
                    image.BigImage = pathToImage + image.BigImage;
                }
                return images.Images;
            }
            return null;
        }

        private string SavePhotoForWorkOrder(string file, string path, string userId, int MaxSize)
        {
            if (file == null)
                return string.Empty;

            byte[] imgData = Convert.FromBase64String(file);
            MemoryStream ms = new MemoryStream(imgData, 0, imgData.Length);
            ms.Write(imgData, 0, imgData.Length);
            Image image = Image.FromStream(ms, true);
            if (!ValidateImage(image))
                return string.Empty;
            string name;
            string ext = "jpg";
            if (_knownImageFormats.TryGetValue(image.RawFormat, out name))
                ext = name.ToLower();
            image = ResizeImage(image, new Size(MaxSize, MaxSize));
            var di = new DirectoryInfo(path);
            if (!di.Exists)
                di.Create();
            var newPath = userId + "." + ext;
            image.Save(path + newPath);
            return newPath;
        }

        private bool CreateIcon(string pathToIcon, string color, string resultIconPath, Color oldColor)
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
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool BuildTechnicianIcons(TechnicianModel technician)
        {
            var pathToTechnicianIcon = httpContextProvider.MapPath(urlToTechnicianIcon);
            var pathToResultIconTechnician = string.Format("{0}{1}.png",
                httpContextProvider.MapPath(urlToFolderTecnician), technician.Id);

            var pathToWorkOrderIcon = httpContextProvider.MapPath(urlToWorkOrderIcon);
            var pathToResultIconWorkOrder = string.Format("{0}{1}.png",
                httpContextProvider.MapPath(urlToFolderWorkOrder),
                technician.Id
                );

            return CreateIcon(pathToTechnicianIcon, technician.Color, pathToResultIconTechnician, colorTechnicianIcon)
                   && CreateIcon(pathToWorkOrderIcon, technician.Color, pathToResultIconWorkOrder, colorWorkOrderIcon);
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

        private bool ValidateImage(Image file)
        {
            try
            {
                if (IsOneOfValidFormats(file.RawFormat))
                {
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        private bool IsOneOfValidFormats(ImageFormat rawFormat)
        {
            return new[] { ImageFormat.Png, ImageFormat.Jpeg, ImageFormat.Gif }.Contains(rawFormat);
        }
    }
}