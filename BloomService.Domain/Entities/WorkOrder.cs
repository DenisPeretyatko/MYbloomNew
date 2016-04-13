using BloomService.Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace BloomService.Domain.Entities
{
    [CollectionNameAttribute("WorkOrderCollection")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SageWorkOrder : SageEntity
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
        [BsonId]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ushort WorkOrder
        {
            get
            {
                return workOrderField;
            }
            set
            {
                workOrderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CallType
        {
            get
            {
                return callTypeField;
            }
            set
            {
                callTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Problem
        {
            get
            {
                return problemField;
            }
            set
            {
                problemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal EstimatedRepairHours
        {
            get
            {
                return estimatedRepairHoursField;
            }
            set
            {
                estimatedRepairHoursField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Priority
        {
            get
            {
                return priorityField;
            }
            set
            {
                priorityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Agreement
        {
            get
            {
                return agreementField;
            }
            set
            {
                agreementField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte AgreemntPeriod
        {
            get
            {
                return agreemntPeriodField;
            }
            set
            {
                agreemntPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AgreemntPeriodSpecified
        {
            get
            {
                return agreemntPeriodFieldSpecified;
            }
            set
            {
                agreemntPeriodFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Center
        {
            get
            {
                return centerField;
            }
            set
            {
                centerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Area
        {
            get
            {
                return areaField;
            }
            set
            {
                areaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Contact
        {
            get
            {
                return contactField;
            }
            set
            {
                contactField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime DateEntered
        {
            get
            {
                return dateEnteredField;
            }
            set
            {
                dateEnteredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DateEnteredSpecified
        {
            get
            {
                return dateEnteredFieldSpecified;
            }
            set
            {
                dateEnteredFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
        public System.DateTime TimeEntered
        {
            get
            {
                return timeEnteredField;
            }
            set
            {
                timeEnteredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TimeEnteredSpecified
        {
            get
            {
                return timeEnteredFieldSpecified;
            }
            set
            {
                timeEnteredFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string EnteredBy
        {
            get
            {
                return enteredByField;
            }
            set
            {
                enteredByField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Employee
        {
            get
            {
                return employeeField;
            }
            set
            {
                employeeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateRun
        {
            get
            {
                return dateRunField;
            }
            set
            {
                dateRunField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateComplete
        {
            get
            {
                return dateCompleteField;
            }
            set
            {
                dateCompleteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
        public System.DateTime TimeComplete
        {
            get
            {
                return timeCompleteField;
            }
            set
            {
                timeCompleteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TimeCompleteSpecified
        {
            get
            {
                return timeCompleteFieldSpecified;
            }
            set
            {
                timeCompleteFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte CompletedBy
        {
            get
            {
                return completedByField;
            }
            set
            {
                completedByField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CompletedBySpecified
        {
            get
            {
                return completedByFieldSpecified;
            }
            set
            {
                completedByFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Status
        {
            get
            {
                return statusField;
            }
            set
            {
                statusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CustomerPO
        {
            get
            {
                return customerPOField;
            }
            set
            {
                customerPOField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string QuoteExpirationDate
        {
            get
            {
                return quoteExpirationDateField;
            }
            set
            {
                quoteExpirationDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TaxatCenter
        {
            get
            {
                return taxatCenterField;
            }
            set
            {
                taxatCenterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Amount
        {
            get
            {
                return amountField;
            }
            set
            {
                amountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AmountSpecified
        {
            get
            {
                return amountFieldSpecified;
            }
            set
            {
                amountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal SalesTaxAmount
        {
            get
            {
                return salesTaxAmountField;
            }
            set
            {
                salesTaxAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SalesTaxAmountSpecified
        {
            get
            {
                return salesTaxAmountFieldSpecified;
            }
            set
            {
                salesTaxAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal AmountBilled
        {
            get
            {
                return amountBilledField;
            }
            set
            {
                amountBilledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AmountBilledSpecified
        {
            get
            {
                return amountBilledFieldSpecified;
            }
            set
            {
                amountBilledFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TotalCost
        {
            get
            {
                return totalCostField;
            }
            set
            {
                totalCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalCostSpecified
        {
            get
            {
                return totalCostFieldSpecified;
            }
            set
            {
                totalCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LeadSource
        {
            get
            {
                return leadSourceField;
            }
            set
            {
                leadSourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Comments
        {
            get
            {
                return commentsField;
            }
            set
            {
                commentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort Equipment
        {
            get
            {
                return equipmentField;
            }
            set
            {
                equipmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EquipmentSpecified
        {
            get
            {
                return equipmentFieldSpecified;
            }
            set
            {
                equipmentFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WorkOrderType
        {
            get
            {
                return workOrderTypeField;
            }
            set
            {
                workOrderTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PayMethod
        {
            get
            {
                return payMethodField;
            }
            set
            {
                payMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PreventiveMaintenance
        {
            get
            {
                return preventiveMaintenanceField;
            }
            set
            {
                preventiveMaintenanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RateSheet
        {
            get
            {
                return rateSheetField;
            }
            set
            {
                rateSheetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SalesEmployee
        {
            get
            {
                return salesEmployeeField;
            }
            set
            {
                salesEmployeeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string JobSaleProduct
        {
            get
            {
                return jobSaleProductField;
            }
            set
            {
                jobSaleProductField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal EstimatedPartsCost
        {
            get
            {
                return estimatedPartsCostField;
            }
            set
            {
                estimatedPartsCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EstimatedPartsCostSpecified
        {
            get
            {
                return estimatedPartsCostFieldSpecified;
            }
            set
            {
                estimatedPartsCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal EstimatedLaborCost
        {
            get
            {
                return estimatedLaborCostField;
            }
            set
            {
                estimatedLaborCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EstimatedLaborCostSpecified
        {
            get
            {
                return estimatedLaborCostFieldSpecified;
            }
            set
            {
                estimatedLaborCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal EstimatedMiscCost
        {
            get
            {
                return estimatedMiscCostField;
            }
            set
            {
                estimatedMiscCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EstimatedMiscCostSpecified
        {
            get
            {
                return estimatedMiscCostFieldSpecified;
            }
            set
            {
                estimatedMiscCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ActualPartsCost
        {
            get
            {
                return actualPartsCostField;
            }
            set
            {
                actualPartsCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualPartsCostSpecified
        {
            get
            {
                return actualPartsCostFieldSpecified;
            }
            set
            {
                actualPartsCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ActualLaborCost
        {
            get
            {
                return actualLaborCostField;
            }
            set
            {
                actualLaborCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualLaborCostSpecified
        {
            get
            {
                return actualLaborCostFieldSpecified;
            }
            set
            {
                actualLaborCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ActualMiscCost
        {
            get
            {
                return actualMiscCostField;
            }
            set
            {
                actualMiscCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualMiscCostSpecified
        {
            get
            {
                return actualMiscCostFieldSpecified;
            }
            set
            {
                actualMiscCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ActualLaborHours
        {
            get
            {
                return actualLaborHoursField;
            }
            set
            {
                actualLaborHoursField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualLaborHoursSpecified
        {
            get
            {
                return actualLaborHoursFieldSpecified;
            }
            set
            {
                actualLaborHoursFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AlternateWorkOrderNbr
        {
            get
            {
                return alternateWorkOrderNbrField;
            }
            set
            {
                alternateWorkOrderNbrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Lead
        {
            get
            {
                return leadField;
            }
            set
            {
                leadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LeadSpecified
        {
            get
            {
                return leadFieldSpecified;
            }
            set
            {
                leadFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Misc
        {
            get
            {
                return miscField;
            }
            set
            {
                miscField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime CallDate
        {
            get
            {
                return callDateField;
            }
            set
            {
                callDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CallDateSpecified
        {
            get
            {
                return callDateFieldSpecified;
            }
            set
            {
                callDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
        public System.DateTime CallTime
        {
            get
            {
                return callTimeField;
            }
            set
            {
                callTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CallTimeSpecified
        {
            get
            {
                return callTimeFieldSpecified;
            }
            set
            {
                callTimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string JCJob
        {
            get
            {
                return jCJobField;
            }
            set
            {
                jCJobField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string JCExtra
        {
            get
            {
                return jCExtraField;
            }
            set
            {
                jCExtraField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Location
        {
            get
            {
                return locationField;
            }
            set
            {
                locationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ARCustomer
        {
            get
            {
                return aRCustomerField;
            }
            set
            {
                aRCustomerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Department
        {
            get
            {
                return departmentField;
            }
            set
            {
                departmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("Non-Billable")]
        public string NonBillable
        {
            get
            {
                return nonBillableField;
            }
            set
            {
                nonBillableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("ChargeBill-to")]
        public string ChargeBillto
        {
            get
            {
                return chargeBilltoField;
            }
            set
            {
                chargeBilltoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PermissionCode
        {
            get
            {
                return permissionCodeField;
            }
            set
            {
                permissionCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal SalesTaxBilled
        {
            get
            {
                return salesTaxBilledField;
            }
            set
            {
                salesTaxBilledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InvoiceDate
        {
            get
            {
                return invoiceDateField;
            }
            set
            {
                invoiceDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateClosed
        {
            get
            {
                return dateClosedField;
            }
            set
            {
                dateClosedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NottoExceed
        {
            get
            {
                return nottoExceedField;
            }
            set
            {
                nottoExceedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("AgreemntPeri...Customer")]
        public string AgreemntPeriCustomer
        {
            get
            {
                return agreemntPeriCustomerField;
            }
            set
            {
                agreemntPeriCustomerField = value;
            }
        }
    }


}