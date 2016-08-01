using System;
using BloomService.Domain.Entities.Concrete;

namespace BloomService.Web.Models
{
    public class MapViewModel
    {
        public SageWorkOrder WorkOrder { get; set; }
        public DateTime? DateEntered { get; set; }
        public string Color { get; set; }
        public long Employee { get; set; }
    }
}