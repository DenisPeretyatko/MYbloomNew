using System.Web;

namespace BloomService.Web.Models.Request
{
    public class ImageModel
    {
        public HttpPostedFileBase Image { get; set; }
        public string IdWorkOrder { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Description { get; set; } 
    }
}