namespace BloomService.Web.Models
{
    public class ImageLocationModel
    {
        public long WorkOrderId { get; set; }
        public long PictureId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}