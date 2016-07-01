using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloomService.Web.Models
{
    public class AddWOItemModel
    {
        public ushort WorkOrder { get; set; }
        public int WorkOrderItem1 { get; set; }
        public string ItemType { get; set; }
        public decimal Quantity { get; set; }
        public DateTime WorkDate { get; set; }
        public string Description { get; set; }
        public decimal SalesTaxAmount { get; set; }
        public decimal FlatRateLaborTaxAmt { get; set; }
    }
}