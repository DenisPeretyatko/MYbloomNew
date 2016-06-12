using AutoMapper.Attributes;
using BloomService.Domain.Entities.Concrete;
using System;

namespace Sage.WebApi.Models.DbModels
{
    [MapsTo(typeof(SageWorkOrder))]
    public class WorkOrderDbModel
    {
        [MapsToProperty(typeof(SageWorkOrder), "ActualLaborCost")]
        public double ACTLABORCOST { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "ActualLaborHours")]
        public double ACTLABORHOURS { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "ActualMiscCost")]
        public double ACTMISCCOST { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "ActualPartsCost")]
        public double ACTPARTSCOST { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Agreement")]
        public int AGREEMENTNBR { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "AlternateWorkOrderNbr")]
        public string ALTWONBR { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "AmountBilled")]
        public double AMTBILLED { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "ARCustomer")]
        public string ARCUST { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Area")]
        public int AREA { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "CallDate")]
        public DateTime CALLDATE { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "CallTime")]
        public TimeSpan CALLTIME { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "CallType")]
        public int CALLTYPECODE { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Center")]
        public int CENTERNBR { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "ChargeBillto")]
        public short CHARGEBILLTO { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Comments")]
        public string COMMENTS { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "CompletedBy")]
        public int COMPLETEBY { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Contact")]
        public string CONTACT { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "CustomerPO")]
        public string CUSTOMERPO { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "DateClosed")]
        public DateTime DATECLOSED { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "DateComplete")]
        public DateTime DATECOMPLETE { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "DateEntered")]
        public DateTime DATEENTER { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "DateRun")]
        public DateTime DATERUN { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Department")]
        public int DEPT { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "EnteredBy")]
        public string ENTERBY { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "EstimatedLaborCost")]
        public double ESTLABORCOST { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "EstimatedMiscCost")]
        public double ESTMISCCOST { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "EstimatedPartsCost")]
        public double ESTPARTSCOST { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "EstimatedRepairHours")]
        public double ESTREPAIRHRS { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "InvoiceDate")]
        public DateTime INVOICEDATE { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "JCExtra")]
        public string JCEXTRA { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "JCJob")]
        public string JCJOB { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "JobSaleProduct")]
        public int JOBSALEPRODUCT { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Lead")]
        public int LEADNBR { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "LeadSource")]
        public int LEADSRCNBR { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Misc")]
        public string MISC { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Name")]
        public string NAME { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "NottoExceed")]
        public string NOTTOEXCEED { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "PayMethod")]
        public int PAYMETHOD { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "PermissionCode")]
        public string PERMISSIONCODE { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Priority")]
        public int PRIORITY { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Problem")]
        public int PROBLEMCODE { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "PreventiveMaintenance")]
        public string QPM { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "QuoteExpirationDate")]
        public DateTime QUOTEEXPDATE { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "RateSheet")]
        public int RATESHEETNBR { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "SalesEmployee")]
        public int SALESMANNBR { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "SalesTaxAmount")]
        public double SALESTAXAMT { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "SalesTaxBilled")]
        public double SALESTAXBILLED { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Status")]
        public int STATUS { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "Employee")]
        public int TECHNICIAN { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "TimeComplete")]
        public TimeSpan TIMECOMPLETE { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "TimeEntered")]
        public TimeSpan TIMEENTER { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "TotalCost")]
        public double TOTALCOST { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "WorkOrderType")]
        public int WOTYPE { get; set; }
        [MapsToProperty(typeof(SageWorkOrder), "WorkOrder")]
        public int WRKORDNBR { get; set; }
        public int AGREEMENTSEQ { get; set; }
        public short BILLINGLEVEL { get; set; }
        public short CREDCARDEXP { get; set; }
        public string CREDCARDNBR { get; set; }
        public int CREDCARDTYPE { get; set; }
        public short JCCOSTING { get; set; }
        public string PMLEASE { get; set; }
        public short PMLEASEREVISION { get; set; }
        public string PMPROPERTY { get; set; }
        public short PMRELATIONSHIP { get; set; }
        public string PMTENANT { get; set; }
        public string PMUNIT { get; set; }
        public string QNONBILLABLE { get; set; }
        public string QPRINTED { get; set; }
        public string QTAXATCENTER { get; set; }
        public DateTime SCHEDDATE { get; set; }
        public string SELECTEDTOBILL { get; set; }
        public string SELECTEDTOCLOSE { get; set; }
        public int SERVSITENBR { get; set; }
        public string SPARE { get; set; }
        public string SPARE3 { get; set; }
        public int SYSEQPNBR { get; set; }
        public double WRKORDAMT { get; set; }
    }
}