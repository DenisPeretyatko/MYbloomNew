namespace BloomService.Domain.Entities.Concrete
{
    using System;
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;
    using System.Collections.Generic;
    [CollectionName("WorkOrderCollection")]
    [XmlType(AnonymousType = true)]
    public class SageWorkOrder : SageEntity
    {
        [XmlIgnore]
        public List<SageEquipment> Equipments { get; set; }

        [XmlIgnore]
        public IEnumerable<SageWorkOrderItem> WorkOrderItems { get; set; }

        public List<ImageLocation> Images { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        [XmlAttribute]
        public decimal ActualLaborCost { get; set; }

        [XmlIgnore]
        public bool ActualLaborCostSpecified { get; set; }

        [XmlAttribute]
        public decimal ActualLaborHours { get; set; }

        [XmlIgnore]
        public bool ActualLaborHoursSpecified { get; set; }

        [XmlAttribute]
        public decimal ActualMiscCost { get; set; }

        [XmlIgnore]
        public bool ActualMiscCostSpecified { get; set; }

        [XmlAttribute]
        public decimal ActualPartsCost { get; set; }

        [XmlIgnore]
        public bool ActualPartsCostSpecified { get; set; }

        [XmlAttribute]
        public byte Agreement { get; set; }

        [XmlAttribute("AgreemntPeri...Customer")]
        public string AgreemntPeriCustomer { get; set; }

        [XmlAttribute]
        public byte AgreemntPeriod { get; set; }

        [XmlIgnore]
        public bool AgreemntPeriodSpecified { get; set; }

        [XmlAttribute]
        public string AlternateWorkOrderNbr { get; set; }

        [XmlAttribute]
        public decimal Amount { get; set; }

        [XmlAttribute]
        public decimal AmountBilled { get; set; }

        [XmlIgnore]
        public bool AmountBilledSpecified { get; set; }

        [XmlIgnore]
        public bool AmountSpecified { get; set; }

        [XmlAttribute]
        public string ARCustomer { get; set; }

        [XmlAttribute]
        public string Area { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? CallDate { get; set; }

        [XmlIgnore]
        public bool CallDateSpecified { get; set; }

        [XmlAttribute(DataType = "time")]
        public DateTime? CallTime { get; set; }

        [XmlIgnore]
        public bool CallTimeSpecified { get; set; }

        [XmlAttribute]
        public string CallType { get; set; }

        [XmlAttribute]
        public string Center { get; set; }

        [XmlAttribute("ChargeBill-to")]
        public string ChargeBillto { get; set; }

        [XmlAttribute]
        public string Comments { get; set; }

        [XmlIgnore]
        public byte CompletedBy { get; set; }

        [XmlIgnore]
        public bool CompletedBySpecified { get; set; }

        [XmlAttribute]
        public string Contact { get; set; }

        [XmlAttribute]
        public string CustomerPO { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? DateClosed { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? DateComplete { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? DateEntered { get; set; }

        [XmlIgnore]
        public bool DateEnteredSpecified { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? DateRun { get; set; }

        [XmlAttribute]
        public string Department { get; set; }

        [XmlAttribute]
        public string Employee { get; set; }

        [XmlAttribute]
        public string EnteredBy { get; set; }

        [XmlAttribute]
        public ushort Equipment { get; set; }

        [XmlIgnore]
        public bool EquipmentSpecified { get; set; }

        [XmlAttribute]
        public decimal EstimatedLaborCost { get; set; }

        [XmlIgnore]
        public bool EstimatedLaborCostSpecified { get; set; }

        [XmlAttribute]
        public decimal EstimatedMiscCost { get; set; }

        [XmlIgnore]
        public bool EstimatedMiscCostSpecified { get; set; }

        [XmlAttribute]
        public decimal EstimatedPartsCost { get; set; }

        [XmlIgnore]
        public bool EstimatedPartsCostSpecified { get; set; }

        [XmlAttribute]
        public decimal EstimatedRepairHours { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? InvoiceDate { get; set; }

        [XmlAttribute]
        public string JCExtra { get; set; }

        [XmlAttribute]
        public string JCJob { get; set; }

        [XmlAttribute]
        public string JobSaleProduct { get; set; }

        [XmlAttribute]
        public byte Lead { get; set; }

        [XmlAttribute]
        public string LeadSource { get; set; }

        [XmlIgnore]
        public bool LeadSpecified { get; set; }

        [XmlAttribute]
        public string Location { get; set; }

        [XmlAttribute]
        public string Misc { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute("Non-Billable")]
        public string NonBillable { get; set; }

        [XmlAttribute]
        public string NottoExceed { get; set; }

        [XmlAttribute]
        public string PayMethod { get; set; }

        [XmlAttribute]
        public string PermissionCode { get; set; }

        [XmlAttribute]
        public string PreventiveMaintenance { get; set; }

        [XmlAttribute]
        public string Priority { get; set; }

        [XmlAttribute]
        public string Problem { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime? QuoteExpirationDate { get; set; }

        [XmlAttribute]
        public string RateSheet { get; set; }

        [XmlAttribute]
        public string SalesEmployee { get; set; }

        [XmlAttribute]
        public decimal SalesTaxAmount { get; set; }

        [XmlIgnore]
        public bool SalesTaxAmountSpecified { get; set; }

        [XmlAttribute]
        public decimal SalesTaxBilled { get; set; }

        [XmlAttribute]
        public string Status { get; set; }

        [XmlAttribute]
        public string TaxatCenter { get; set; }

        [XmlAttribute(DataType = "time")]
        public DateTime? TimeComplete { get; set; }

        [XmlIgnore]
        public bool TimeCompleteSpecified { get; set; }

        [XmlAttribute(DataType = "time")]
        public DateTime? TimeEntered { get; set; }

        [XmlIgnore]
        public bool TimeEnteredSpecified { get; set; }

        [XmlAttribute]
        public decimal TotalCost { get; set; }

        [XmlIgnore]
        public bool TotalCostSpecified { get; set; }

        [XmlAttribute]
        public long WorkOrder { get; set; }

        [XmlAttribute]
        public string WorkOrderType { get; set; }

        [XmlIgnore]
        public long AssignmentId { get; set; }
        [XmlIgnore]
        public string Address { get; set; }

        [XmlIgnore]
        public SageCustomer CustomerObj { get; set; }
        [XmlIgnore]
        public SageLocation LocationObj { get; set; }
        [XmlIgnore]
        public SageCallType CalltypeObj { get; set; }
        [XmlIgnore]
        public SageProblem ProblemObj { get; set; }
        [XmlIgnore]
        public SageRateSheet RateSheetObj { get; set; }
        [XmlIgnore]
        public SageEmployee EmployeeObj { get; set; }
        [XmlIgnore]
        public SageRepair HourObj { get; set; }
        [XmlIgnore]
        public SagePermissionCode PermissionCodeObj { get; set; }
        [XmlIgnore]
        public DateTime? ScheduleDate { get; set; }

        public SagePaymentMethod PaymentMethodObj { get; set; }
        [XmlIgnore]
        public SageWorkOrderStatus StatusObj { get; set; }
    }
}