using System;

namespace BloomService.Web.Models
{
    public class AssignmentViewModel
    {
        public string Id { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Employee { get; set; }
        public string WorkOrder { get; set; }
        public string EstimatedRepairHours { get; set; }
        public DateTime EndDate { get; set; }
    }
}