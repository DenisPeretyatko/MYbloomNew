using AutoMapper.Attributes;
using BloomService.Domain.Entities.Concrete;
using System;

namespace Sage.WebApi.Models
{
    [MapsTo(typeof(SageWorkOrderItem))]
    public class WorkOrderItemDbModel
    {
        [MapsToProperty(typeof(SageWorkOrderItem), "WorkOrder")]
        public long WRKORDNBR { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "WorkOrderItem")]
        public int WRKORDITEM { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "WorkDate")]
        public DateTime WORKDATE { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "AccountingDate")]
        public DateTime ACCTDATE { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "Equipment")]
        public int SYSEQPNBR { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "Employee")]
        public int TECHNICIAN { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "Quantity")]
        public double QUANTITY { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "UnitSale")]
        public double UNITSALE { get; set; }

        public double TOTALSALE { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "TotalCost")]
        public double TOTALCOST { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "Description")]
        public string DESCRIPTION { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "ItemType")]
        public int ITEMTYPE { get; set; }

        public string QINACTIVE { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "LaborProduct")]
        public int LABORPRODUCT { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "Invoice")]
        public int INVOICENBR { get; set; }

        public int INVITEMNBR { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "PartsProduct")]
        public int PARTSPRODUCT { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "Note")]
        public int NOTENBR { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "FlatRate")]
        public int FLATRATENBR { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "FlatRateLevel")]
        public int FLATRATELEVEL { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "LaborSale")]
        public double LABORSALE { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "PartsSale")]
        public double PARTSSALE { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "LaborCost")]
        public double LABORCOST { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "PartsCost")]
        public double PARTSCOST { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "IncludedTax")]
        public double INCLUDEDTAX { get; set; }

        public string QCOVERED { get; set; }

        public string SPARE2 { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "DateEntered")]
        public DateTime DATEENTER { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "TimeEntered")]
        public TimeSpan TIMEENTER { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "EnteredBy")]
        public string ENTERBY { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "ActualLaborHours")]
        public double ACTLABORHOURS { get; set; }

        public string SPARE { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "TaxGroup")]
        public string TAXGROUP { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "ExemptStatus")]
        public string EXEMPTSTATUS { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "SalesTaxAmount")]
        public double SALESTAXAMT { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "CostQuantity")]
        public double COSTQUANTITY { get; set; }

        public double PREFILLEDHOURS { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "JCJob")]
        public string JCJOB { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "JCExtra")]
        public string JCEXTRA { get; set; }

        public string JCCAT { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "JCCostCode")]
        public string JCCOSTCODE { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "Status")]
        public int STATUS { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "UnrecognizedRevenue")]
        public double UNRECOGNIZEDREV { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "FlatRateLbrExemptStatus")]
        public string FRLABOREXEMPTSTATUS { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "FlatRatePrtExemptStatus")]
        public string FRPARTSEXEMPTSTATUS { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "FlatRateLbrTaxGroup")]
        public string FRLABORTAXGROUP { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "FlatRatePrtTaxGroup")]
        public string FRPARTSTAXGROUP { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "FlatRateLbJCCostCode")]
        public string FRLABORJCCOSTCODE { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "FlatRatePtJCCostCode")]
        public string FRPARTSJCCOSTCODE { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "FlatRateLbJCCategory")]
        public string FRLABORJCCAT { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "FlatRatePtJCCategory")]
        public string FRPARTSJCCAT { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "FlatRateLaborTaxAmt")]
        public double FRLABORSALESTAXAMT { get; set; }

        [MapsToProperty(typeof(SageWorkOrderItem), "FlatRatePartsTaxAmt")]
        public double FRPARTSSALESTAXAMT { get; set; }

        public string PMCHARGETYPE { get; set; }

        public string FRLABORPMCHARGETYPE { get; set; }

        public string FRPARTSPMCHARGETYPE { get; set; }

        public double TOTALPRCOST { get; set; }

        public int ORIGIN { get; set; }

        public double UNITSALEUNRECOGNIZED { get; set; }
    }
}