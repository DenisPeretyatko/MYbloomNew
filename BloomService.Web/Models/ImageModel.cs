namespace BloomService.Web.Models.Request
{
    public class ImageModel
    {
        public string Image { get; set; }
        public string IdWorkOrder { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Description { get; set; } 
    }
}