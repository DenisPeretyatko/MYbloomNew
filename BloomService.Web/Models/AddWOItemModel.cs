using System;

namespace BloomService.Web.Models
{
    public class AddWOItemModel
    {
        public int WorkOrder { get; set; }
        public int WorkOrderItem { get; set; }
        public string ItemType { get; set; }
        public decimal Quantity { get; set; }
        public DateTime WorkDate { get; set; }
        public string Description { get; set; }
        public decimal SalesTaxAmount { get; set; }
        public decimal FlatRateLaborTaxAmt { get; set; }
    }
}