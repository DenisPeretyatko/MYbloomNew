using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloomService.Web.Models.Request
{
    public class ImageRequest
    {
        public string Image { get; set; }
        public string IdWorkOrder { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}