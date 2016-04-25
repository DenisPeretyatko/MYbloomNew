using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloomService.Web.Models.Request
{
    public class ImageRequest
    {
        public IEnumerable<string> Images { get; set; }
        public string IdWorkOrder { get; set; }
    }
}