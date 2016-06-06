using AutoMapper.Attributes;
using BloomService.Domain.Entities.Concrete;
using System;

namespace Sage.WebApi.Models.DbModels
{
    [MapsTo(typeof(SageWorkOrder))]
    public class WorkOrderDbModel
    {
        [MapsFromProperty(typeof(SageWorkOrder), "ActualLaborCost")]
        public decimal ACTLABORCOST { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "ActualLaborHours")]
        public decimal ACTLABORHOURS { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "ActualMiscCost")]
        public decimal ACTMISCCOST { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "ActualPartsCost")]
        public decimal ACTPARTSCOST { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Agreement")]
        public int AGREEMENTNBR { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "AlternateWorkOrderNbr")]
        public string ALTWONBR { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "AmountBilled")]
        public decimal AMTBILLED { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "ARCustomer")]
        public string ARCUST { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Area")]
        public int AREA { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "CallDate")]
        public DateTime CALLDATE { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "CallTime")]
        public DateTime CALLTIME { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "CallType")]
        public int CALLTYPECODE { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Center")]
        public int CENTERNBR { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "ChargeBillto")]
        public short CHARGEBILLTO { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Comments")]
        public string COMMENTS { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "CompletedBy")]
        public int COMPLETEBY { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Contact")]
        public string CONTACT { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "CustomerPO")]
        public string CUSTOMERPO { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "DateClosed")]
        public DateTime DATECLOSED { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "DateComplete")]
        public DateTime DATECOMPLETE { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "DateEntered")]
        public DateTime DATEENTER { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "DateRun")]
        public DateTime DATERUN { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Department")]
        public int DEPT { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "EnteredBy")]
        public string ENTERBY { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "EstimatedLaborCost")]
        public decimal ESTLABORCOST { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "EstimatedMiscCost")]
        public decimal ESTMISCCOST { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "EstimatedPartsCost")]
        public decimal ESTPARTSCOST { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "EstimatedRepairHours")]
        public decimal ESTREPAIRHRS { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "InvoiceDate")]
        public DateTime INVOICEDATE { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "JCExtra")]
        public string JCEXTRA { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "JCJob")]
        public string JCJOB { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "JobSaleProduct")]
        public int JOBSALEPRODUCT { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Lead")]
        public int LEADNBR { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "LeadSource")]
        public int LEADSRCNBR { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Misc")]
        public string MISC { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Name")]
        public string NAME { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "NottoExceed")]
        public string NOTTOEXCEED { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "PayMethod")]
        public int PAYMETHOD { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "PermissionCode")]
        public string PERMISSIONCODE { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Priority")]
        public int PRIORITY { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Problem")]
        public int PROBLEMCODE { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "PreventiveMaintenance")]
        public string QPM { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "QuoteExpirationDate")]
        public DateTime QUOTEEXPDATE { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "RateSheet")]
        public int RATESHEETNBR { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "SalesEmployee")]
        public int SALESMANNBR { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "SalesTaxAmount")]
        public decimal SALESTAXAMT { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "SalesTaxBilled")]
        public decimal SALESTAXBILLED { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Status")]
        public int STATUS { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "Employee")]
        public int TECHNICIAN { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "TimeComplete")]
        public DateTime TIMECOMPLETE { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "TimeEntered")]
        public DateTime TIMEENTER { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "TotalCost")]
        public decimal TOTALCOST { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "WorkOrderType")]
        public int WOTYPE { get; set; }
        [MapsFromProperty(typeof(SageWorkOrder), "WorkOrder")]
        public int WRKORDNBR { get; set; }
    }
}