namespace BloomService.Web.Models
{
    using System;

    public class AssignmentViewModel
    {
        public long Employee { get; set; }

        public DateTime EndDate { get; set; }

        public string EstimatedRepairHours { get; set; }

        public string Id { get; set; }

        public DateTime ScheduleDate { get; set; }

        public long WorkOrder { get; set; }
    }
}