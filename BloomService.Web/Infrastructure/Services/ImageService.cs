using System.Drawing.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using BloomService.Web.Infrastructure.StorageProviders;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using RestSharp.Extensions;

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
    using BloomService.Web.Infrastructure.Mongo;
    using BloomService.Web.Infrastructure.Services.Interfaces;
    using BloomService.Web.Models;
    using SignalR;
    using Domain.Extensions;

    public class ImageService : IImageService
    {
        private static readonly Dictionary<ImageFormat, string> _knownImageFormats = new Dictionary<ImageFormat, string>()
        {
            { ImageFormat.Bmp, "bmp"},
            { ImageFormat.Jpeg, "jpg"},
            { ImageFormat.Png, "png"}
        };

        private readonly IRepository repository;
        private readonly IBloomServiceHub _hub;
        private readonly IStorageProvider _storageProvider;


        private readonly string _urlToTechnicianIcon;
        private readonly string _urlToWorkOrderIcon;
        private readonly string _urlToFolderTecnician;
        private readonly string _urlToFolderWorkOrder;
        private readonly string _urlToFolderPhotoWorkOrders;
        private readonly string _urlToFolderFonts;
        private readonly string small = "small";
        private readonly Color colorTechnicianIcon = Color.FromArgb(0, 13, 255);
        private readonly Color colorWorkOrderIcon = Color.FromArgb(255, 0, 4);
        private readonly BloomServiceConfiguration settings;

        public ImageService(IHttpContextProvider httpContextProvider, IRepository repository, IBloomServiceHub hub, IStorageProvider storageProvider)
        {
            this.repository = repository;
            this.settings = BloomServiceConfiguration.FromWebConfig(ConfigurationManager.AppSettings);
            _hub = hub;
            _storageProvider = storageProvider;
            _urlToTechnicianIcon = Path.Combine(settings.BasePath, "images/technician.png");
            _urlToWorkOrderIcon = Path.Combine(settings.BasePath, "images/workorder.png");
            _urlToFolderTecnician = Path.Combine(settings.BasePath, "technician");
            _urlToFolderWorkOrder = Path.Combine(settings.BasePath, "workorder");
            _urlToFolderPhotoWorkOrders = Path.Combine(settings.BasePath, "images");
            _urlToFolderFonts = Path.Combine(settings.BasePath.Replace("/userFiles", ""), "fonts");;

        }


        //Upload images to Azure
        private static List<string> DirSearch(string sDir)
        {
            var files = new List<string>();
            files.AddRange(Directory.GetFiles(sDir));
            foreach (var d in Directory.GetDirectories(sDir))
            {
                files.AddRange(DirSearch(d));
            }
            return files;
        }

        private void SaveFilesHelper()
        {
            string basePath = @"C:\Users\User\Desktop\beta";
            string finalPath = settings.BasePath.Replace("dev", "beta");
            if (!_storageProvider.IsFolderExits(finalPath))
                _storageProvider.TryCreateFolder(finalPath);

            List<string> files = DirSearch(basePath);
            foreach (var file in files)
            {
                Image image = Image.FromFile(Path.Combine(file), true);
                var format = image.RawFormat;
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, format);
                    var newPath = file.Replace(@"C:\Users\User\Desktop\beta\userFiles\", "");
                    if (_storageProvider.IsFileExists(Path.Combine(finalPath, newPath)))
                        _storageProvider.DeleteFile(Path.Combine(finalPath, newPath));
                    _storageProvider.CreateFile(Path.Combine(finalPath, newPath), ms.ToArray());
                }
            }
        }

        public ImageLocation SavePhotoForWorkOrder(ImageModel model)
        {
            var workOrder = this.repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == model.IdWorkOrder).SingleOrDefault();
            if (workOrder == null)
            {
                return null;
            }

            var imagesDb = repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.IdWorkOrder).SingleOrDefault();
            var countImage = 1;
            if (imagesDb != null && imagesDb.Images != null)
            {
                countImage = imagesDb.Images.Last().Id + 1;
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

            //var pathToImage = Path.Combine(this.httpContextProvider.MapPath(this._urlToFolderPhotoWorkOrders), model.IdWorkOrder.ToString());
            var pathToImage = _storageProvider.GetPublicUrl(Path.Combine(this._urlToFolderPhotoWorkOrders, model.IdWorkOrder.ToString()));
            var nameBig = countImage.ToString();
            var nameSmall = small + countImage;
            var fileName = SavePhotoForWorkOrder(model.Image, pathToImage, nameBig, settings.SizeBigPhoto, workOrder, countImage);
            var fileNameSmall = SavePhotoForWorkOrder(model.Image, pathToImage, nameSmall, this.settings.SizeSmallPhoto, workOrder, countImage);
            var maxId = imagesDb.Images.Any() ? imagesDb.Images.Max(x => x.Id) : 0;
            var image = new ImageLocation { Image = fileNameSmall, BigImage = fileName, Latitude = model.Latitude, Longitude = model.Longitude, Id = maxId + 1, Description = model.Description };
            imagesDb.Images.Add(image);
            repository.Add(imagesDb);
            var milliseconds = DateTime.Now.TimeOfDay.TotalMilliseconds;
            image.Image += $"?{milliseconds}";
            image.BigImage += $"?{milliseconds}";
            _hub.UpdateWorkOrderPicture(imagesDb);
            return image;
        }

        public Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();
            return bmp;
        }

        public FontFamily LoadFontFamily(byte[] buffer, out PrivateFontCollection fontCollection)
        {
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                fontCollection = new PrivateFontCollection();
                fontCollection.AddMemoryFont(ptr, buffer.Length);
                return fontCollection.Families[0];
            }
            finally
            {
                handle.Free();
            }
        }

        public FontFamily LoadFontFamily(Stream stream, out PrivateFontCollection fontCollection)
        {
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return LoadFontFamily(buffer, out fontCollection);
        }

        private Image AddWaterMark(Image image, SageWorkOrder workOrder, long number)
        {
            var watermarkImage = Image.FromStream(_storageProvider.GetFile(Path.Combine(_urlToFolderPhotoWorkOrders, "watermark.png")).OpenRead());
            var newWidth = image.Width / 7;
            var newHeight = newWidth * watermarkImage.Height / watermarkImage.Width;
            var margin = newWidth / 10;
            float opacity = (float)0.5;
            int fontSize = Convert.ToInt32(newHeight / 7);
            Stream fontStream = _storageProvider.GetFile(Path.Combine(_urlToFolderFonts, "OpenSans-Bold.ttf")).OpenRead();
            PrivateFontCollection fonts;
            FontFamily family = LoadFontFamily(fontStream, out fonts);
            Font theFont = new Font(family, fontSize);

            watermarkImage = ResizeImage(watermarkImage, new Size(newWidth, newHeight));
           // watermarkImage = ChangeOpacity(watermarkImage, opacity);

            using (var graphicsHandle = Graphics.FromImage(image))
            using (TextureBrush watermarkBrush = new TextureBrush(watermarkImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                int x = (margin);
                int y = (image.Height - newHeight);
                watermarkBrush.TranslateTransform(x, y);
                graphicsHandle.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(newWidth, newHeight)));

                using (var arialFont = theFont)
                {
                    x = (image.Width - newWidth - margin);
                    y = (image.Height - newHeight - 4);
                    RectangleF rectF1 = new RectangleF(x, y, newWidth, newHeight);
                    SolidBrush brush = new SolidBrush(Color.FromArgb(255, Color.White));
                    StringFormat stringFormat = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    
                    graphicsHandle.FillRectangle(brush, Rectangle.Round(rectF1));
                    var pen = new Pen(Color.Black, 0.1f);
                    graphicsHandle.DrawRectangle(pen, Rectangle.Round(rectF1));
                    pen.Color = Color.Red;
                    RectangleF rectInside = new RectangleF(x+2, y+2, newWidth-4, newHeight-4);
                    graphicsHandle.DrawRectangle(pen, Rectangle.Round(rectInside));
                    var woDate=""; 
                    if (workOrder.DateEntered != null)
                         woDate = workOrder.DateEntered.Value.Date.ToShortDateString();
                    graphicsHandle.DrawString($"WORK ORDER #{workOrder.WorkOrder}\r\n{woDate}\r\nIMAGE #{number}", arialFont, Brushes.Black, x+ newWidth/2, y+ newHeight/2, stringFormat);
                }

            }
            return image;
        }

        public bool SaveDescriptionsForPhoto(CommentImageModel model)
        {
            var imagesDb = this.repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.IdWorkorder).SingleOrDefault();
            if (imagesDb != null && imagesDb.Images != null)
            {
                var image = imagesDb.Images.FirstOrDefault(x => x.Id == model.IdImage);
                if (image != null)
                {
                    imagesDb.Images.Remove(image);
                    image.Description = model.Description;
                    imagesDb.Images.Add(image);
                    this.repository.Update(imagesDb);
                    _hub.UpdateWorkOrderPicture(imagesDb);
                    return true;
                }
            }
            return false;
        }

        public List<ImageLocation> GetPhotoForWorkOrder(long idWorkOrder, string prefixUrl = null)
        {
            var images = this.repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == idWorkOrder).SingleOrDefault();

            if (images == null)
                return new List<ImageLocation>();

            var milliseconds = DateTime.Now.TimeOfDay.TotalMilliseconds;
            var path = Path.Combine(this._urlToFolderPhotoWorkOrders, idWorkOrder.ToString());

            foreach (var image in images.Images.Where(x=>!x.IsDeleted))
            {
                image.Image = _storageProvider.GetFullUrl(Path.Combine(path, $"{image.Image}?{milliseconds}"));
                image.BigImage = _storageProvider.GetFullUrl(Path.Combine(path, $"{image.BigImage}?{milliseconds}"));
            }
            return images.Images.OrderBy(x => x.Id).ToList();

        }

        private string SavePhotoForWorkOrder(HttpPostedFileBase file, string path, string userId, int MaxSize, SageWorkOrder workOrder, long number)
        {
            if (file == null)
                return string.Empty;
            Image image = Image.FromStream(file.InputStream, true);
            if (!this.ValidateImage(image))
                return string.Empty;
            //string name;
            string ext = _knownImageFormats[ImageFormat.Jpeg];
            //if (_knownImageFormats.TryGetValue(image.RawFormat, out name))
            //    ext = name.ToLower();
            image = AddWaterMark(image, workOrder, number);
            image = ResizeImage(image, new Size(MaxSize, MaxSize));
            if (!_storageProvider.IsFolderExits(path))
                _storageProvider.TryCreateFolder(path);
            var newPath = userId + "." + ext;
            using (var ms = new MemoryStream())
            {
                var resultPath = Path.Combine(path, newPath);
                image.Save(ms, ImageFormat.Jpeg);
                if (_storageProvider.IsFileExists(resultPath))
                    _storageProvider.DeleteFile(resultPath);
                _storageProvider.CreateFile(resultPath, ms.ToArray());
            }

            return newPath;
        }

        private bool CreateIcon(string pathToIcon, string color, string resultIconPath, Color oldColor)
        {
            try
            {
                if (color != null)
                {
                    Image image = Image.FromStream(_storageProvider.GetFile(pathToIcon).OpenRead());
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
                    using (var ms = new MemoryStream())
                    {
                        image.Save(ms, ImageFormat.Png);
                        if (_storageProvider.IsFileExists(resultIconPath))
                            _storageProvider.DeleteFile(resultIconPath);
                        _storageProvider.CreateFile(resultIconPath, ms.ToArray());

                    }
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
            //var pathToTechnicianIcon = this.httpContextProvider.MapPath(this._urlToTechnicianIcon);
            var pathToTechnicianIcon = _storageProvider.GetPublicUrl(this._urlToTechnicianIcon);
            //var pathToResultIconTechnician = string.Format("{0}{1}.{2}",
            //    this.httpContextProvider.MapPath(this._urlToFolderTecnician), "\\" + technician.Id, _knownImageFormats[ImageFormat.Png]);
            var pathToResultIconTechnician = string.Format("{0}{1}.{2}",
              _storageProvider.GetPublicUrl(this._urlToFolderTecnician), "\\" + technician.Id, _knownImageFormats[ImageFormat.Png]);

            //var pathToWorkOrderIcon = this.httpContextProvider.MapPath(this._urlToWorkOrderIcon);
            var pathToWorkOrderIcon = _storageProvider.GetPublicUrl(this._urlToWorkOrderIcon);
            //var pathToResultIconWorkOrder = string.Format("{0}{1}.{2}",
            //    this.httpContextProvider.MapPath(this._urlToFolderWorkOrder),
            //    "\\" + technician.Id, _knownImageFormats[ImageFormat.Png]
            //    );
            var pathToResultIconWorkOrder = string.Format("{0}{1}.{2}",
               _storageProvider.GetPublicUrl(this._urlToFolderWorkOrder),
               "\\" + technician.Id, _knownImageFormats[ImageFormat.Png]
               );

            return this.CreateIcon(pathToTechnicianIcon, technician.Color, pathToResultIconTechnician, this.colorTechnicianIcon)
                   && this.CreateIcon(pathToWorkOrderIcon, technician.Color, pathToResultIconWorkOrder, this.colorWorkOrderIcon);
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
            //try
            //{
            //    if (this.IsOneOfValidFormats(file.RawFormat))
            //    {
            //        return true;
            //    }
            //}
            //catch
            //{
            //}
            //return false;
            return true;
        }

        public byte[] CreateArchive(SageImageWorkOrder pictures)
        {
            var pathToImg = Path.Combine(_urlToFolderPhotoWorkOrders, pictures.WorkOrder.ToString());
            var path = _storageProvider.GetPublicUrl(pathToImg);
            var outputMemStream = new MemoryStream();
            var zipStream = new ZipOutputStream(outputMemStream);
            zipStream.SetLevel(3);

            foreach (var record in pictures.Images.Where(x=>!x.IsDeleted))
            {
                byte[] bytes;
                try
                {
                    bytes = _storageProvider.GetFile(Path.Combine(path, record.BigImage)).OpenRead().ReadAsBytes();
                }
                catch (Exception)
                {
                    continue;
                }
                var newEntry = new ZipEntry(record.BigImage) { DateTime = DateTime.Now };
                zipStream.PutNextEntry(newEntry);
                var inStream = new MemoryStream(bytes);
                StreamUtils.Copy(inStream, zipStream, new byte[4096]);
                inStream.Close();
                zipStream.CloseEntry();
            }
            zipStream.IsStreamOwner = false;
            zipStream.Close();
            outputMemStream.Position = 0;
            return outputMemStream.ToArray();
        }

        public bool ChangeImageLocation(ImageLocationModel model)
        {
            var images = repository.SearchFor<SageImageWorkOrder>(x => x.WorkOrder == model.WorkOrderId).SingleOrDefault();
            var changedImage = images?.Images.SingleOrDefault(x => x.Id == model.PictureId && !x.IsDeleted);
            if (changedImage == null)
                return false;

            changedImage.Latitude = model.Latitude;
            changedImage.Longitude = model.Longitude;
            repository.Update(images);
            return true;
        }

        private bool IsOneOfValidFormats(ImageFormat rawFormat)
        {
            return new[] { ImageFormat.Png, ImageFormat.Jpeg, ImageFormat.Gif }.Contains(rawFormat);
        }
    }
}