using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BloomService.Domain.Entities.Concrete;

namespace BloomService.Web.Models
{
    public class MapViewModel
    {
        public SageWorkOrder WorkOrder { get; set; }
        public DateTime? DateEntered { get; set; }

    }
}