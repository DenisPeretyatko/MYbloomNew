﻿using System;

namespace BloomService.Web.Models
{
    public class WorkOrderModel
    {
        public string Customer { get; set; }
        public string Location { get; set; }
        public string Calltype { get; set; }
        public DateTime Calldate { get; set; }
        public string Problem { get; set; }
        public string Ratesheet { get; set; }
        public string Emploee { get; set; }
        public string Equipment { get; set; }
        public decimal Estimatehours { get; set; }
        public string Nottoexceed { get; set; }
        public string Locationcomments { get; set; }
        public string Customerpo { get; set; }
        public string Permissiocode { get; set; }
        public string Paymentmethods { get; set; }
    }
}