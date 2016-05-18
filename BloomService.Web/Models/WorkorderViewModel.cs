namespace BloomService.Web.Models
{
    public class WorkorderViewModel
    {
        public string WorkOrder { get; set; }
        public string DateEntered { get; set; }
        public string ARCustomer { get; set; }
        public string Location { get; set; }
        public string EstimatedRepairHours { get; set; }
        public string Status { get; set; }
    }
}