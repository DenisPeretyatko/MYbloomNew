namespace BloomService.Domain.Entities.Concrete
{
    using System;
    using System.Xml.Serialization;

    using BloomService.Domain.Attributes;
    using BloomService.Domain.Entities.Concrete.Auxiliary;

    [XmlType(AnonymousType = true)]
    [CollectionName("CustomerCollection")]
    public class SageCustomer : SageEntity
    {
        [XmlAttribute]
        public string ABCompanyID { get; set; }

        [XmlAttribute]
        public string ABN { get; set; }

        [XmlAttribute]
        public string AddonTable { get; set; }

        [XmlAttribute]
        public string Address1 { get; set; }

        [XmlAttribute]
        public string Address2 { get; set; }

        [XmlAttribute]
        public string Address3 { get; set; }

        [XmlAttribute]
        public string Address4 { get; set; }

        [XmlAttribute]
        public string AutoText { get; set; }

        [XmlAttribute]
        public string Billing { get; set; }

        [XmlAttribute]
        public string BillingAddress1 { get; set; }

        [XmlAttribute]
        public string BillingAddress2 { get; set; }

        [XmlAttribute]
        public string BillingAddress3 { get; set; }

        [XmlAttribute]
        public string BillingAddress4 { get; set; }

        [XmlAttribute]
        public string BillingCity { get; set; }

        [XmlAttribute]
        public string BillingContact { get; set; }

        [XmlAttribute]
        public string BillingEmailAddress { get; set; }

        [XmlAttribute]
        public string BillingFreq { get; set; }

        [XmlAttribute]
        public string BillingState { get; set; }

        [XmlAttribute]
        public string BillingZIPCode { get; set; }

        [XmlAttribute]
        public object Checklist1Date { get; set; }

        [XmlAttribute]
        public bool Checklist10 { get; set; }

        [XmlAttribute]
        public object Checklist10Date { get; set; }

        [XmlAttribute]
        public bool Checklist2 { get; set; }

        [XmlAttribute]
        public object Checklist2Date { get; set; }

        [XmlAttribute]
        public bool Checklist3 { get; set; }

        [XmlAttribute]
        public object Checklist3Date { get; set; }

        [XmlAttribute]
        public bool Checklist4 { get; set; }

        [XmlAttribute]
        public object Checklist4Date { get; set; }

        [XmlAttribute]
        public bool Checklist5 { get; set; }

        [XmlAttribute]
        public object Checklist5Date { get; set; }

        [XmlAttribute]
        public bool Checklist6 { get; set; }

        [XmlAttribute]
        public object Checklist6Date { get; set; }

        [XmlAttribute]
        public bool Checklist7 { get; set; }

        [XmlAttribute]
        public object Checklist7Date { get; set; }

        [XmlAttribute]
        public bool Checklist8 { get; set; }

        [XmlAttribute]
        public object Checklist8Date { get; set; }

        [XmlAttribute]
        public bool Checklist9 { get; set; }

        [XmlAttribute]
        public object Checklist9Date { get; set; }

        [XmlAttribute]
        public string City { get; set; }

        [XmlAttribute]
        public string Contact10 { get; set; }

        [XmlAttribute]
        public string Contact2 { get; set; }

        [XmlAttribute]
        public string Contact3 { get; set; }

        [XmlAttribute]
        public string Contact4 { get; set; }

        [XmlAttribute]
        public string Contact5 { get; set; }

        [XmlAttribute]
        public string Contact6 { get; set; }

        [XmlAttribute]
        public string Contact7 { get; set; }

        [XmlAttribute]
        public string Contact8 { get; set; }

        [XmlAttribute]
        public string Contact9 { get; set; }

        [XmlAttribute]
        public double CreditLimit { get; set; }

        [XmlAttribute]
        public string CreditRating { get; set; }

        [XmlAttribute]
        public string Customer { get; set; }

        [XmlAttribute]
        public object CustomerFileLinks { get; set; }

        [XmlAttribute]
        public object CustomerNotes { get; set; }

        [XmlAttribute]
        public string CustomerType { get; set; }

        [XmlAttribute]
        public DateTime DateEstablished { get; set; }

        [XmlAttribute]
        public DateTime DateStamp { get; set; }

        [XmlAttribute]
        public int DaysBeforeDue { get; set; }

        [XmlAttribute]
        public double Discount { get; set; }

        [XmlAttribute]
        public string EmailAddress { get; set; }

        [XmlAttribute]
        public string EquipmentExemptStatus { get; set; }

        [XmlAttribute]
        public string EquipmentRateTable { get; set; }

        [XmlAttribute]
        public bool ExemptfromFinanceCharge { get; set; }

        [XmlAttribute]
        public string Fax { get; set; }

        [XmlAttribute]
        public double FinanceChargeFlatRate { get; set; }

        [XmlAttribute]
        public double FinanceChargePercentageRate { get; set; }

        [XmlAttribute]
        public string FinanceChargeRateType { get; set; }

        [XmlAttribute]
        public string GLPrefix { get; set; }

        [XmlAttribute]
        public string InvoiceFormat { get; set; }

        [XmlAttribute]
        public object InvoiceHeader { get; set; }

        [XmlAttribute]
        public string Key1 { get; set; }

        [XmlAttribute]
        public string Key2 { get; set; }

        [XmlAttribute]
        public string LaborExemptStatus { get; set; }

        [XmlAttribute]
        public string LaborRateTable { get; set; }

        [XmlAttribute]
        public long LastAutoNumber { get; set; }

        [XmlAttribute]
        public string MarkupTable { get; set; }

        [XmlAttribute]
        public string MaterialExemptStatus { get; set; }

        [XmlAttribute]
        public string MaterialRateTable { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string OperatorStamp { get; set; }

        [XmlAttribute]
        public string OtherExemptStatus { get; set; }

        [XmlAttribute]
        public string OtherRateTable { get; set; }

        [XmlAttribute]
        public string OverheadExemptStatus { get; set; }

        [XmlAttribute]
        public string OverheadRateTable { get; set; }

        [XmlAttribute]
        public bool PORequired { get; set; }

        [XmlAttribute]
        public string ResaleCert { get; set; }

        [XmlAttribute]
        public double Retainage { get; set; }

        [XmlAttribute]
        public bool SendStatement { get; set; }

        [XmlAttribute]
        public string SMInvoiceFormat { get; set; }

        [XmlAttribute]
        public string State { get; set; }

        [XmlAttribute]
        public string Status { get; set; }

        [XmlAttribute]
        public string SubcontractExemptStatus { get; set; }

        [XmlAttribute]
        public string SubcontractRateTable { get; set; }

        [XmlAttribute]
        public string TaxGroup { get; set; }

        [XmlAttribute]
        public string Telephone { get; set; }

        [XmlAttribute]
        public object Text2 { get; set; }

        [XmlAttribute]
        public object Text3 { get; set; }

        [XmlAttribute]
        public object Text4 { get; set; }

        [XmlAttribute]
        public object Text5 { get; set; }

        [XmlAttribute]
        public object Text6 { get; set; }

        [XmlAttribute]
        public TimeSpan TimeStamp { get; set; }

        [XmlAttribute]
        public string TotalBilledExemptStatus { get; set; }

        [XmlAttribute]
        public string Trade { get; set; }

        [XmlAttribute]
        public bool Warranty { get; set; }

        [XmlAttribute]
        public string ZIPCode { get; set; }
    }
}