using System.Collections.Generic;

namespace BloomService.Web.Models
{
    public class ScheduleViewModel
    {
        public IEnumerable<WorkorderViewModel> UnassignedWorkorders { get; set; }
        public IEnumerable<AssignmentModel> Assigments { get; set; }
    }

}