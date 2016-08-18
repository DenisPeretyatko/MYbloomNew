using Newtonsoft.Json;

namespace BloomService.Web.Models
{
    public class AssignmentModel : EntityModel
    {
        [JsonProperty("Alarm")]
        public string Alarm { get; set; }
        [JsonProperty("AlarmDate")]
        public string AlarmDate { get; set; }
        [JsonProperty("AlarmStatus")]
        public string AlarmStatus { get; set; }
        [JsonProperty("Area")]
        public string Area { get; set; }
        [JsonProperty("Assignment")]
        public string Assignment { get; set; }
        [JsonProperty("Assignmenttype")]
        public string Assignmenttype { get; set; }
        [JsonProperty("CallType")]
        public string CallType { get; set; }
        [JsonProperty("Center")]
        public string Center { get; set; }
        [JsonProperty("Comments")]
        public string Comments { get; set; }
        [JsonProperty("CreateTimeEntry")]
        public string CreateTimeEntry { get; set; }
        [JsonProperty("DateEntered")]
        public string DateEntered { get; set; }
        [JsonProperty("Department")]
        public string Department { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("ElapsedTime")]
        public string ElapsedTime { get; set; }
        [JsonProperty("Employee")]
        public string Employee { get; set; }
        [JsonProperty("EmployeeId")]
        public string EmployeeId { get; set; }
        [JsonProperty("Enddate")]
        public string Enddate { get; set; }
        [JsonProperty("Endtime")]
        public string Endtime { get; set; }
        [JsonProperty("EnteredBy")]
        public string EnteredBy { get; set; }
        [JsonProperty("EstimatedRepairHours")]
        public string EstimatedRepairHours { get; set; }
        [JsonProperty("ETAdate")]
        public string EtAdate { get; set; }
        [JsonProperty("ETAtime")]
        public string EtAtime { get; set; }
        [JsonProperty("Inactive")]
        public string Inactive { get; set; }
        [JsonProperty("LastDate")]
        public string LastDate { get; set; }
        [JsonProperty("LastStatus")]
        public string LastStatus { get; set; }
        [JsonProperty("LastTime")]
        public string LastTime { get; set; }
        [JsonProperty("NextECardNumber")]
        public string NextECardNumber { get; set; }
        [JsonProperty("PaidLunchBreak")]
        public string PaidLunchBreak { get; set; }
        [JsonProperty("PostedTime")]
        public string PostedTime { get; set; }
        [JsonProperty("Priority")]
        public string Priority { get; set; }
        [JsonProperty("Problem")]
        public string Problem { get; set; }
        [JsonProperty("ScheduleDate")]
        public string ScheduleDate { get; set; }
        [JsonProperty("StartTime")]
        public string StartTime { get; set; }
        [JsonProperty("TimeEntered")]
        public string TimeEntered { get; set; }
        [JsonProperty("WorkOrder")]
        public string WorkOrder { get; set; }

        [JsonProperty("Start")]
        public string Start { get; set; }
        [JsonProperty("End")]
        public string End { get; set; }
        [JsonProperty("Location")]
        public string Location { get; set; }
        [JsonProperty("Customer")]
        public string Customer { get; set; }
        [JsonProperty("Color")]
        public string Color { get; set; }
    }
}