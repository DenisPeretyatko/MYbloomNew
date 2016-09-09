using System.ComponentModel.DataAnnotations;

namespace BloomService.Web.Models
{
    using System;
    using System.Collections.Generic;
    public class WorkOrderModel
    {
        public DateTime Calldate { get; set; }

        public string Calltype { get; set; }

        public string Customer { get; set; }

        public string Customerpo { get; set; }

        public long Emploee { get; set; }

        public List<LaborPartsModel> PartsAndLabors { get; set; }

        public string Estimatehours { get; set; }

        public string Id { get; set; }

        [Required]
        public string Location { get; set; }

        public string Locationcomments { get; set; }

        public string Nottoexceed { get; set; }

        public string Paymentmethods { get; set; }

        public string Permissiocode { get; set; }

        [Required]
        public string Problem { get; set; }

        public string Ratesheet { get; set; }

        public long WorkOrder { get; set; }

        public int Status { get; set; }

        public string JCJob { get; set; }

        public List<WorkOrderNoteModel> Notes { get; set; }

        public string Contact { get; set; }

        public ushort Equipment { get; set; }

        public DateTime AssignmentDate { get; set; }

        public DateTime AssignmentTime { get; set; }
    }
}