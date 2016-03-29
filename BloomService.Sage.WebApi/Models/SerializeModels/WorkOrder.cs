namespace Sage.WebApi.Models.SerializeModels
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SageWorkOrder
    {
        private ushort workOrderField;

        private string callTypeField;

        private string problemField;

        private decimal estimatedRepairHoursField;

        private string priorityField;

        private byte agreementField;

        private byte agreemntPeriodField;

        private bool agreemntPeriodFieldSpecified;

        private string centerField;

        private string areaField;

        private string nameField;

        private string contactField;

        private System.DateTime dateEnteredField;

        private bool dateEnteredFieldSpecified;

        private System.DateTime timeEnteredField;

        private bool timeEnteredFieldSpecified;

        private string enteredByField;

        private string employeeField;

        private string dateRunField;

        private string dateCompleteField;

        private System.DateTime timeCompleteField;

        private bool timeCompleteFieldSpecified;

        private byte completedByField;

        private bool completedByFieldSpecified;

        private string statusField;

        private string customerPOField;

        private string quoteExpirationDateField;

        private string taxatCenterField;

        private decimal amountField;

        private bool amountFieldSpecified;

        private decimal salesTaxAmountField;

        private bool salesTaxAmountFieldSpecified;

        private decimal amountBilledField;

        private bool amountBilledFieldSpecified;

        private decimal totalCostField;

        private bool totalCostFieldSpecified;

        private string leadSourceField;

        private string commentsField;

        private ushort equipmentField;

        private bool equipmentFieldSpecified;

        private string workOrderTypeField;

        private string payMethodField;

        private string preventiveMaintenanceField;

        private string rateSheetField;

        private string salesEmployeeField;

        private string jobSaleProductField;

        private decimal estimatedPartsCostField;

        private bool estimatedPartsCostFieldSpecified;

        private decimal estimatedLaborCostField;

        private bool estimatedLaborCostFieldSpecified;

        private decimal estimatedMiscCostField;

        private bool estimatedMiscCostFieldSpecified;

        private decimal actualPartsCostField;

        private bool actualPartsCostFieldSpecified;

        private decimal actualLaborCostField;

        private bool actualLaborCostFieldSpecified;

        private decimal actualMiscCostField;

        private bool actualMiscCostFieldSpecified;

        private decimal actualLaborHoursField;

        private bool actualLaborHoursFieldSpecified;

        private string alternateWorkOrderNbrField;

        private byte leadField;

        private bool leadFieldSpecified;

        private string miscField;

        private System.DateTime callDateField;

        private bool callDateFieldSpecified;

        private System.DateTime callTimeField;

        private bool callTimeFieldSpecified;

        private string jCJobField;

        private string jCExtraField;

        private string locationField;

        private string aRCustomerField;

        private string departmentField;

        private string nonBillableField;

        private string chargeBilltoField;

        private string permissionCodeField;

        private decimal salesTaxBilledField;

        private string invoiceDateField;

        private string dateClosedField;

        private string nottoExceedField;

        private string agreemntPeriCustomerField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort WorkOrder
        {
            get
            {
                return this.workOrderField;
            }
            set
            {
                this.workOrderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CallType
        {
            get
            {
                return this.callTypeField;
            }
            set
            {
                this.callTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Problem
        {
            get
            {
                return this.problemField;
            }
            set
            {
                this.problemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal EstimatedRepairHours
        {
            get
            {
                return this.estimatedRepairHoursField;
            }
            set
            {
                this.estimatedRepairHoursField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Priority
        {
            get
            {
                return this.priorityField;
            }
            set
            {
                this.priorityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Agreement
        {
            get
            {
                return this.agreementField;
            }
            set
            {
                this.agreementField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte AgreemntPeriod
        {
            get
            {
                return this.agreemntPeriodField;
            }
            set
            {
                this.agreemntPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AgreemntPeriodSpecified
        {
            get
            {
                return this.agreemntPeriodFieldSpecified;
            }
            set
            {
                this.agreemntPeriodFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Center
        {
            get
            {
                return this.centerField;
            }
            set
            {
                this.centerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Area
        {
            get
            {
                return this.areaField;
            }
            set
            {
                this.areaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Contact
        {
            get
            {
                return this.contactField;
            }
            set
            {
                this.contactField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime DateEntered
        {
            get
            {
                return this.dateEnteredField;
            }
            set
            {
                this.dateEnteredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DateEnteredSpecified
        {
            get
            {
                return this.dateEnteredFieldSpecified;
            }
            set
            {
                this.dateEnteredFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
        public System.DateTime TimeEntered
        {
            get
            {
                return this.timeEnteredField;
            }
            set
            {
                this.timeEnteredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TimeEnteredSpecified
        {
            get
            {
                return this.timeEnteredFieldSpecified;
            }
            set
            {
                this.timeEnteredFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string EnteredBy
        {
            get
            {
                return this.enteredByField;
            }
            set
            {
                this.enteredByField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Employee
        {
            get
            {
                return this.employeeField;
            }
            set
            {
                this.employeeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateRun
        {
            get
            {
                return this.dateRunField;
            }
            set
            {
                this.dateRunField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateComplete
        {
            get
            {
                return this.dateCompleteField;
            }
            set
            {
                this.dateCompleteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
        public System.DateTime TimeComplete
        {
            get
            {
                return this.timeCompleteField;
            }
            set
            {
                this.timeCompleteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TimeCompleteSpecified
        {
            get
            {
                return this.timeCompleteFieldSpecified;
            }
            set
            {
                this.timeCompleteFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte CompletedBy
        {
            get
            {
                return this.completedByField;
            }
            set
            {
                this.completedByField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CompletedBySpecified
        {
            get
            {
                return this.completedByFieldSpecified;
            }
            set
            {
                this.completedByFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CustomerPO
        {
            get
            {
                return this.customerPOField;
            }
            set
            {
                this.customerPOField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string QuoteExpirationDate
        {
            get
            {
                return this.quoteExpirationDateField;
            }
            set
            {
                this.quoteExpirationDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TaxatCenter
        {
            get
            {
                return this.taxatCenterField;
            }
            set
            {
                this.taxatCenterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Amount
        {
            get
            {
                return this.amountField;
            }
            set
            {
                this.amountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AmountSpecified
        {
            get
            {
                return this.amountFieldSpecified;
            }
            set
            {
                this.amountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal SalesTaxAmount
        {
            get
            {
                return this.salesTaxAmountField;
            }
            set
            {
                this.salesTaxAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SalesTaxAmountSpecified
        {
            get
            {
                return this.salesTaxAmountFieldSpecified;
            }
            set
            {
                this.salesTaxAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal AmountBilled
        {
            get
            {
                return this.amountBilledField;
            }
            set
            {
                this.amountBilledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AmountBilledSpecified
        {
            get
            {
                return this.amountBilledFieldSpecified;
            }
            set
            {
                this.amountBilledFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TotalCost
        {
            get
            {
                return this.totalCostField;
            }
            set
            {
                this.totalCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalCostSpecified
        {
            get
            {
                return this.totalCostFieldSpecified;
            }
            set
            {
                this.totalCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LeadSource
        {
            get
            {
                return this.leadSourceField;
            }
            set
            {
                this.leadSourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Comments
        {
            get
            {
                return this.commentsField;
            }
            set
            {
                this.commentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort Equipment
        {
            get
            {
                return this.equipmentField;
            }
            set
            {
                this.equipmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EquipmentSpecified
        {
            get
            {
                return this.equipmentFieldSpecified;
            }
            set
            {
                this.equipmentFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WorkOrderType
        {
            get
            {
                return this.workOrderTypeField;
            }
            set
            {
                this.workOrderTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PayMethod
        {
            get
            {
                return this.payMethodField;
            }
            set
            {
                this.payMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PreventiveMaintenance
        {
            get
            {
                return this.preventiveMaintenanceField;
            }
            set
            {
                this.preventiveMaintenanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RateSheet
        {
            get
            {
                return this.rateSheetField;
            }
            set
            {
                this.rateSheetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SalesEmployee
        {
            get
            {
                return this.salesEmployeeField;
            }
            set
            {
                this.salesEmployeeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string JobSaleProduct
        {
            get
            {
                return this.jobSaleProductField;
            }
            set
            {
                this.jobSaleProductField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal EstimatedPartsCost
        {
            get
            {
                return this.estimatedPartsCostField;
            }
            set
            {
                this.estimatedPartsCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EstimatedPartsCostSpecified
        {
            get
            {
                return this.estimatedPartsCostFieldSpecified;
            }
            set
            {
                this.estimatedPartsCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal EstimatedLaborCost
        {
            get
            {
                return this.estimatedLaborCostField;
            }
            set
            {
                this.estimatedLaborCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EstimatedLaborCostSpecified
        {
            get
            {
                return this.estimatedLaborCostFieldSpecified;
            }
            set
            {
                this.estimatedLaborCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal EstimatedMiscCost
        {
            get
            {
                return this.estimatedMiscCostField;
            }
            set
            {
                this.estimatedMiscCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EstimatedMiscCostSpecified
        {
            get
            {
                return this.estimatedMiscCostFieldSpecified;
            }
            set
            {
                this.estimatedMiscCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ActualPartsCost
        {
            get
            {
                return this.actualPartsCostField;
            }
            set
            {
                this.actualPartsCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualPartsCostSpecified
        {
            get
            {
                return this.actualPartsCostFieldSpecified;
            }
            set
            {
                this.actualPartsCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ActualLaborCost
        {
            get
            {
                return this.actualLaborCostField;
            }
            set
            {
                this.actualLaborCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualLaborCostSpecified
        {
            get
            {
                return this.actualLaborCostFieldSpecified;
            }
            set
            {
                this.actualLaborCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ActualMiscCost
        {
            get
            {
                return this.actualMiscCostField;
            }
            set
            {
                this.actualMiscCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualMiscCostSpecified
        {
            get
            {
                return this.actualMiscCostFieldSpecified;
            }
            set
            {
                this.actualMiscCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ActualLaborHours
        {
            get
            {
                return this.actualLaborHoursField;
            }
            set
            {
                this.actualLaborHoursField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualLaborHoursSpecified
        {
            get
            {
                return this.actualLaborHoursFieldSpecified;
            }
            set
            {
                this.actualLaborHoursFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AlternateWorkOrderNbr
        {
            get
            {
                return this.alternateWorkOrderNbrField;
            }
            set
            {
                this.alternateWorkOrderNbrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Lead
        {
            get
            {
                return this.leadField;
            }
            set
            {
                this.leadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LeadSpecified
        {
            get
            {
                return this.leadFieldSpecified;
            }
            set
            {
                this.leadFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Misc
        {
            get
            {
                return this.miscField;
            }
            set
            {
                this.miscField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime CallDate
        {
            get
            {
                return this.callDateField;
            }
            set
            {
                this.callDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CallDateSpecified
        {
            get
            {
                return this.callDateFieldSpecified;
            }
            set
            {
                this.callDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
        public System.DateTime CallTime
        {
            get
            {
                return this.callTimeField;
            }
            set
            {
                this.callTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CallTimeSpecified
        {
            get
            {
                return this.callTimeFieldSpecified;
            }
            set
            {
                this.callTimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string JCJob
        {
            get
            {
                return this.jCJobField;
            }
            set
            {
                this.jCJobField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string JCExtra
        {
            get
            {
                return this.jCExtraField;
            }
            set
            {
                this.jCExtraField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ARCustomer
        {
            get
            {
                return this.aRCustomerField;
            }
            set
            {
                this.aRCustomerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Department
        {
            get
            {
                return this.departmentField;
            }
            set
            {
                this.departmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("Non-Billable")]
        public string NonBillable
        {
            get
            {
                return this.nonBillableField;
            }
            set
            {
                this.nonBillableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("ChargeBill-to")]
        public string ChargeBillto
        {
            get
            {
                return this.chargeBilltoField;
            }
            set
            {
                this.chargeBilltoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PermissionCode
        {
            get
            {
                return this.permissionCodeField;
            }
            set
            {
                this.permissionCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal SalesTaxBilled
        {
            get
            {
                return this.salesTaxBilledField;
            }
            set
            {
                this.salesTaxBilledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InvoiceDate
        {
            get
            {
                return this.invoiceDateField;
            }
            set
            {
                this.invoiceDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateClosed
        {
            get
            {
                return this.dateClosedField;
            }
            set
            {
                this.dateClosedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NottoExceed
        {
            get
            {
                return this.nottoExceedField;
            }
            set
            {
                this.nottoExceedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("AgreemntPeri...Customer")]
        public string AgreemntPeriCustomer
        {
            get
            {
                return this.agreemntPeriCustomerField;
            }
            set
            {
                this.agreemntPeriCustomerField = value;
            }
        }
    }


}