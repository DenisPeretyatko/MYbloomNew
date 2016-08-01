using BloomService.Domain.Entities.Concrete;
using System;

namespace BloomService.Web.Models
{
    public class LaborPartsModel
    {
        public DateTime WorkDate { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Employee { get; set; }
        public decimal CostQty { get; set; }
        public decimal BiledQty { get; set; }
        public decimal Rate { get; set; }
        public long WorkOrder { get; set; }
        public long WorkOrderItem { get; set; }
        public decimal TotalSale { get; set; }
        public decimal LaborSale { get; set; }
        public decimal PartsSale { get; set; }
        public int Part { get; set; }
        public SageRepair LaborItem { get; set; }
    }
}