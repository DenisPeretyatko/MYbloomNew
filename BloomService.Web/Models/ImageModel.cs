namespace BloomService.Web.Models
{
    using System.Web;

    public class ImageModel
    {
        public HttpPostedFileBase Image { get; set; }
        public long IdWorkOrder { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Description { get; set; } 
    }
}