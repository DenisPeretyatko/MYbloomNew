using BloomService.Domain.Attributes;
using System;
using System.Xml.Serialization;

namespace BloomService.Domain.Entities.Concrete
{
    [XmlType(AnonymousType = true)]
    public class SageWorkOrderItem : SageEntity
    {
        [XmlAttribute(DataType = "date")]
        public DateTime AccountingDate { get; set; }
        [XmlAttribute]
        public decimal ActualLaborHours { get; set; }
        [XmlAttribute]
        public decimal CostQuantity { get; set; }
        [XmlAttribute]
        public string Covered { get; set; }
        [XmlAttribute(DataType = "date")]
        public DateTime DateEntered { get; set; }
        [XmlAttribute]
        public string Description { get; set; }
        [XmlAttribute]
        public string Employee { get; set; }
        [XmlAttribute]
        public string EnteredBy { get; set; }
        [XmlAttribute]
        public int Equipment { get; set; }
        [XmlAttribute]
        public string ExemptStatus { get; set; }
        [XmlAttribute]
        public string FlatRate { get; set; }
        [XmlAttribute]
        public decimal FlatRateLaborTaxAmt { get; set; }
        [XmlAttribute]
        public string FlatRateLbJCCategory { get; set; }
        [XmlAttribute]
        public string FlatRateLbJCCostCode { get; set; }
        [XmlAttribute]
        public string FlatRateLbrExemptStatus { get; set; }
        [XmlAttribute]
        public string FlatRateLbrTaxGroup { get; set; }
        [XmlAttribute]
        public string FlatRateLevel { get; set; }
        [XmlAttribute]
        public decimal FlatRatePartsTaxAmt { get; set; }
        [XmlAttribute]
        public string FlatRatePrtExemptStatus { get; set; }
        [XmlAttribute]
        public string FlatRatePrtTaxGroup { get; set; }
        [XmlAttribute]
        public string FlatRatePtJCCategory { get; set; }
        [XmlAttribute]
        public string FlatRatePtJCCostCode { get; set; }
        [XmlAttribute]
        public decimal IncludedTax { get; set; }
        [XmlAttribute]
        public int Invoice { get; set; }
        [XmlAttribute]
        public string ItemType { get; set; }
        [XmlAttribute]
        public string JCCategory { get; set; }
        [XmlAttribute]
        public string JCCostCode { get; set; }
        [XmlAttribute]
        public string JCExtra { get; set; }
        [XmlAttribute]
        public string JCJob { get; set; }
        [XmlAttribute]
        public decimal LaborCost { get; set; }
        [XmlAttribute]
        public string LaborProduct { get; set; }
        [XmlAttribute]
        public decimal LaborSale { get; set; }
        [XmlAttribute]
        public int Note { get; set; }
        [XmlAttribute]
        public decimal PartsCost { get; set; }
        [XmlAttribute]
        public int Part { get; set; }
        [XmlAttribute]
        public string PartsProduct { get; set; }
        [XmlAttribute]
        public decimal PartsSale { get; set; }
        [XmlAttribute]
        public decimal Quantity { get; set; }
        [XmlAttribute]
        public decimal SalesTaxAmount { get; set; }
        [XmlAttribute]
        public int Status { get; set; }
        [XmlAttribute]
        public int TaxGroup { get; set; }
        [XmlAttribute(DataType = "time")]
        public DateTime TimeEntered { get; set; }
        [XmlAttribute]
        public decimal TotalCost { get; set; }
        [XmlAttribute]
        public decimal TotalSale { get; set; }
        [XmlAttribute]
        public decimal UnitSale { get; set; }
        [XmlAttribute]
        public decimal UnitSaleUnrecognized { get; set; }
        [XmlAttribute]
        public decimal UnrecognizedRevenue { get; set; }
        [XmlAttribute(DataType = "date")]
        public DateTime WorkDate { get; set; }
        [XmlAttribute]
        public int WorkOrder { get; set; }
        [XmlAttribute("WorkOrderItem")]
        public int WorkOrderItem { get; set; }
    }
}
