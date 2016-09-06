namespace BloomService.Web.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Web;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Extensions;
    using BloomService.Web.Infrastructure.Mongo;
    using BloomService.Web.Infrastructure.Services.Interfaces;
    using BloomService.Web.Infrastructure.SignalR;
    using BloomService.Web.Infrastructure.StorageProviders;
    using BloomService.Web.Models;

    public class ImageService : IImageService
    {
        private const string Small = "small";

        private static readonly Dictionary<ImageFormat, string> KnownImageFormats =
            new Dictionary<ImageFormat, string>()
                {
                    { ImageFormat.Bmp, "bmp" }, 
                    { ImageFormat.Jpeg, "jpg" }, 
                    { ImageFormat.Png, "png" }
                };

        private readonly Color colorTechnicianIcon = Color.FromArgb(0, 13, 255);

        private readonly Color colorWorkOrderIcon = Color.FromArgb(255, 0, 4);

        private readonly IHttpContextProvider httpContextProvider;

        private readonly IBloomServiceHub hub;

        private readonly IRepository repository;

        private readonly BloomServiceConfiguration settings;

        private readonly IStorageFile storageFile;

        private readonly IStorageFolder storageFolder;

        private readonly IStorageProvider storageProvider;

        public ImageService(
            IHttpContextProvider httpContextProvider, 
            IRepository repository, 
            IBloomServiceHub hub, 
            IStorageProvider storageProvider, 
            IStorageFolder storageFolder, 
            IStorageFile storageFile)
        {
            this.httpContextProvider = httpContextProvider;
            this.repository = repository;
            settings = BloomServiceConfiguration.FromWebConfig(ConfigurationManager.AppSettings);
            this.hub = hub;
            this.storageProvider = storageProvider;
            this.storageFolder = storageFolder;
            this.storageFile = storageFile;
        }

        public bool BuildTechnicianIcons(TechnicianModel technician)
        {
            var ff = storageProvider.GetPublicUrl(settings.UrlToTechnicianIcon);
            var pathToTechnicianIcon = httpContextProvider.MapPath(settings.UrlToTechnicianIcon);
            string pathToResultIconTechnician =
                $"{httpContextProvider.MapPath(settings.UrlToFolderTecnician)}{technician.Id}.{KnownImageFormats[ImageFormat.Png]}";

            var pathToWorkOrderIcon = httpContextProvider.MapPath(settings.UrlToWorkOrderIcon);
            string pathToResultIconWorkOrder =
                $"{httpContextProvider.MapPath(settings.UrlToFolderWorkOrder)}{technician.Id}.{KnownImageFormats[ImageFormat.Png]}";

            return CreateIcon(pathToTechnicianIcon, technician.Color, pathToResultIconTechnician, colorTechnicianIcon)
                   && CreateIcon(pathToWorkOrderIcon, technician.Color, pathToResultIconWorkOrder, colorWorkOrderIcon);
        }

        public List<ImageLocation> GetPhotoForWorkOrder(long idWorkOrder, string prefixUrl = null)
        {
            string pathToImage = $"{settings.UrlToFolderPhotoWorkOrders}{idWorkOrder}/";
            var baseUri = prefixUrl == null
                              ? settings.UrlToFolderPhotoWorkOrders.UriCombine(idWorkOrder.ToString())
                              : prefixUrl.UriCombine(pathToImage);
            var images = repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == idWorkOrder).SingleOrDefault();

            if (images == null)
            {
                return new List<ImageLocation>();
            }

            var milliseconds = DateTime.Now.TimeOfDay.TotalMilliseconds;
            foreach (var image in images.Images)
            {
                image.Image = baseUri.UriCombine($"{image.Image}?{milliseconds}");
                image.BigImage = baseUri.UriCombine($"{image.BigImage}?{milliseconds}");
            }

            return images.Images.OrderBy(x => x.Id).ToList();
        }

        public bool SaveDescriptionsForPhoto(CommentImageModel model)
        {
            var imagesDb =
                repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.IdWorkorder).SingleOrDefault();
            var image = imagesDb?.Images?.FirstOrDefault(x => x.Id == model.IdImage);
            if (image != null)
            {
                imagesDb.Images.Remove(image);
                image.Description = model.Description;
                imagesDb.Images.Add(image);
                repository.Update(imagesDb);
                hub.UpdateWorkOrderPicture(imagesDb);
                return true;
            }

            return false;
        }

        public ImageLocation SavePhotoForWorkOrder(ImageModel model)
        {
            var workOrder = repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.IdWorkOrder).SingleOrDefault();
            if (workOrder == null)
            {
                return null;
            }

            var imagesDb =
                repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.IdWorkOrder).SingleOrDefault();
            var countImage = 1;
            if (imagesDb?.Images != null)
            {
                countImage = imagesDb.Images.Count + 1;
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

            var pathToImage = Path.Combine(
                httpContextProvider.MapPath(settings.UrlToFolderPhotoWorkOrders), 
                model.IdWorkOrder.ToString());
            var nameBig = countImage.ToString();
            var nameSmall = Small + countImage;
            var fileName = SavePhotoForWorkOrder(model.Image, pathToImage, nameBig, settings.SizeBigPhoto);
            var fileNameSmall = SavePhotoForWorkOrder(model.Image, pathToImage, nameSmall, settings.SizeSmallPhoto);
            var maxId = imagesDb.Images.Any() ? imagesDb.Images.Max(x => x.Id) : 0;
            var image = new ImageLocation
                            {
                                Image = fileNameSmall, 
                                BigImage = fileName, 
                                Latitude = model.Latitude, 
                                Longitude = model.Longitude, 
                                Id = maxId + 1, 
                                Description = model.Description
                            };
            imagesDb.Images.Add(image);
            repository.Add(imagesDb);
            var milliseconds = DateTime.Now.TimeOfDay.TotalMilliseconds;
            image.Image += $"?{milliseconds}";
            image.BigImage += $"?{milliseconds}";
            hub.UpdateWorkOrderPicture(imagesDb);
            return image;
        }

        private static bool CreateIcon(string pathToIcon, string color, string resultIconPath, Color oldColor)
        {
            try
            {
                if (color == null)
                {
                    return true;
                }
                var image = Image.FromFile(pathToIcon);
                var imageGraphics = Graphics.FromImage(image);

                var colorSwapper = new ColorMap[1];
                colorSwapper[0] = new ColorMap { OldColor = oldColor, NewColor = ColorTranslator.FromHtml(color) };
                var imageAttr = new ImageAttributes();
                imageAttr.SetRemapTable(colorSwapper);
                imageGraphics.DrawImage(
                    image, 
                    new Rectangle(0, 0, image.Width, image.Height), 
                    0, 
                    0, 
                    image.Width, 
                    image.Height, 
                    GraphicsUnit.Pixel, 
                    imageAttr);
                image.Save(resultIconPath, ImageFormat.Png);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                var originalWidth = image.Width;
                var originalHeight = image.Height;
                var percentWidth = size.Width / (float)originalWidth;
                var percentHeight = size.Height / (float)originalHeight;
                var percent = percentHeight < percentWidth ? percentHeight : percentWidth;
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

        private bool IsOneOfValidFormats(ImageFormat rawFormat)
        {
            return new[] { ImageFormat.Png, ImageFormat.Jpeg, ImageFormat.Gif }.Contains(rawFormat);
        }

        private string SavePhotoForWorkOrder(HttpPostedFileBase file, string path, string userId, int maxSize)
        {
            if (file == null)
            {
                return string.Empty;
            }

            var image = Image.FromStream(file.InputStream, true);
            if (!ValidateImage(image))
            {
                return string.Empty;
            }

            string name;
            var ext = KnownImageFormats[ImageFormat.Jpeg];
            if (KnownImageFormats.TryGetValue(image.RawFormat, out name))
            {
                ext = name.ToLower();
            }

            image = ResizeImage(image, new Size(maxSize, maxSize));
            var di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                di.Create();
            }

            var newPath = userId + "." + ext;
            image.Save(Path.Combine(path, newPath));
            return newPath;
        }

        private bool ValidateImage(Image file)
        {
            // try
            // {
            // if (this.IsOneOfValidFormats(file.RawFormat))
            // {
            // return true;
            // }
            // }
            // catch
            // {
            // }
            // return false;
            return true;
        }
    }
}