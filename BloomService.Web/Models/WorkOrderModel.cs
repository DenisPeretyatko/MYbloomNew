namespace BloomService.Web.Models
{
    using System;

    public class WorkOrderModel
    {
        public DateTime Calldate { get; set; }

        public string Calltype { get; set; }

        public string Customer { get; set; }

        public string Customerpo { get; set; }

        public string Emploee { get; set; }

        public string Equipment { get; set; }

        public string Estimatehours { get; set; }

        public string Id { get; set; }

        public string Location { get; set; }

        public string Locationcomments { get; set; }

        public string Nottoexceed { get; set; }

        public string Paymentmethods { get; set; }

        public string Permissiocode { get; set; }

        public string Problem { get; set; }

        public string Ratesheet { get; set; }
    }
}