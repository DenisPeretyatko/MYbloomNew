namespace BloomService.Web.Models
{
    using System;

    public class AssignmentViewModel
    {
        public string Employee { get; set; }

        public DateTime EndDate { get; set; }

        public string EstimatedRepairHours { get; set; }

        public string Id { get; set; }

        public DateTime ScheduleDate { get; set; }

        public string WorkOrder { get; set; }
    }
}