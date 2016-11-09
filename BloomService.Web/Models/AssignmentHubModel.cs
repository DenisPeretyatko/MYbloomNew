using System;
using BloomService.Domain.Entities.Concrete;

namespace BloomService.Web.Models
{
    public class AssignmentHubModel
    {
        public SageWorkOrder WorkOrder { get; set; }
        public DateTime? DateEntered { get; set; }
        public string Color { get; set; }
        public long Employee { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Customer { get; set; }
        public string Location { get; set; }
        public long Assignment { get; set; }
        public string EstimatedRepairHours { get; set; }
    }
}