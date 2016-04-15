using System;
using Newtonsoft.Json;

namespace BloomService.Web.Models
{
    public class CustomerModel
    {
        [JsonProperty("Customer")]
        public string Customer { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Customer_Type")]
        public string CustomerType { get; set; }
        [JsonProperty("Trade")]
        public string Trade { get; set; }
        [JsonProperty("Key_1")]
        public string Key1 { get; set; }
        [JsonProperty("Key_2")]
        public string Key2 { get; set; }
        [JsonProperty("Address_1")]
        public string Address1 { get; set; }
        [JsonProperty("Address_2")]
        public string Address2 { get; set; }
        [JsonProperty("Address_3")]
        public string Address3 { get; set; }
        [JsonProperty("Address_4")]
        public string Address4 { get; set; }
        [JsonProperty("City")]
        public string City { get; set; }
        [JsonProperty("State")]
        public string State { get; set; }
        [JsonProperty("ZIP_Code")]
        public string ZipCode { get; set; }
        [JsonProperty("Telephone")]
        public string Telephone { get; set; }
        [JsonProperty("Fax")]
        public string Fax { get; set; }
        [JsonProperty("Email_Address")]
        public string EmailAddress { get; set; }
        [JsonProperty("Date_Established")]
        public DateTime DateEstablished { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }
        [JsonProperty("GL_Prefix")]
        public string GlPrefix { get; set; }
        [JsonProperty("Days_Before_Due")]
        public int DaysBeforeDue { get; set; }
        [JsonProperty("Send_Statement")]
        public bool SendStatement { get; set; }
        [JsonProperty("PO_Required")]
        public bool PoRequired { get; set; }
        [JsonProperty("Credit_Limit")]
        public int CreditLimit { get; set; }
        [JsonProperty("Credit_Rating")]
        public string CreditRating { get; set; }
        [JsonProperty("Billing")]
        public string Billing { get; set; }
        [JsonProperty("Contact_2")]
        public string Contact2 { get; set; }
        [JsonProperty("Contact_3")]
        public string Contact3 { get; set; }
        [JsonProperty("Contact_4")]
        public string Contact4 { get; set; }
        [JsonProperty("Contact_5")]
        public string Contact5 { get; set; }
        [JsonProperty("Contact_6")]
        public string Contact6 { get; set; }
        [JsonProperty("Contact_7")]
        public string Contact7 { get; set; }
        [JsonProperty("Contact_8")]
        public string Contact8 { get; set; }
        [JsonProperty("Contact_9")]
        public string Contact9 { get; set; }
        [JsonProperty("Contact_10")]
        public string Contact10 { get; set; }
        [JsonProperty("Billing_Address_1")]
        public string BillingAddress1 { get; set; }
        [JsonProperty("Billing_Address_2")]
        public string BillingAddress2 { get; set; }
        [JsonProperty("Billing_Address_3")]
        public string BillingAddress3 { get; set; }
        [JsonProperty("Billing_Address_4")]
        public string BillingAddress4 { get; set; }
        [JsonProperty("Billing_City")]
        public string BillingCity { get; set; }
        [JsonProperty("Billing_State")]
        public string BillingState { get; set; }
        [JsonProperty("Billing_ZIP_Code")]
        public string BillingZipCode { get; set; }
        [JsonProperty("Billing_Email_Address")]
        public string BillingEmailAddress { get; set; }
        [JsonProperty("Billing_Contact")]
        public string BillingContact { get; set; }
        [JsonProperty("Invoice_Format")]
        public string InvoiceFormat { get; set; }
        [JsonProperty("SM_Invoice_Format")]
        public string SmInvoiceFormat { get; set; }
        [JsonProperty("Billing_Freq")]
        public string BillingFreq { get; set; }
        [JsonProperty("Retainage")]
        public int Retainage { get; set; }
        [JsonProperty("Auto_Text")]
        public string AutoText { get; set; }
        [JsonProperty("Last_Auto_Number")]
        public int LastAutoNumber { get; set; }
        [JsonProperty("Exempt_from_Finance_Charge")]
        public bool ExemptFromFinanceCharge { get; set; }
        [JsonProperty("Finance_Charge_Rate_Type")]
        public string FinanceChargeRateType { get; set; }
        [JsonProperty("Finance_Charge_Percentage_Rate")]
        public int FinanceChargePercentageRate { get; set; }
        [JsonProperty("Finance_Charge_Flat_Rate")]
        public int FinanceChargeFlatRate { get; set; }
        [JsonProperty("Labor_Rate_Table")]
        public string LaborRateTable { get; set; }
        [JsonProperty("Material_Rate_Table")]
        public string MaterialRateTable { get; set; }
        [JsonProperty("Subcontract_Rate_Table")]
        public string SubcontractRateTable { get; set; }
        [JsonProperty("Equipment_Rate_Table")]
        public string EquipmentRateTable { get; set; }
        [JsonProperty("Overhead_Rate_Table")]
        public string OverheadRateTable { get; set; }
        [JsonProperty("Other_Rate_Table")]
        public string OtherRateTable { get; set; }
        [JsonProperty("Markup_Table")]
        public string MarkupTable { get; set; }
        [JsonProperty("Addon_Table")]
        public string AddonTable { get; set; }
        [JsonProperty("Discount")]
        public int Discount { get; set; }
        [JsonProperty("Total_Billed_Exempt_Status")]
        public string TotalBilledExemptStatus { get; set; }
        [JsonProperty("Tax_Group")]
        public string TaxGroup { get; set; }
        [JsonProperty("Labor_Exempt_Status")]
        public string LaborExemptStatus { get; set; }
        [JsonProperty("Material_Exempt_Status")]
        public string MaterialExemptStatus { get; set; }
        [JsonProperty("Subcontract_Exempt_Status")]
        public string SubcontractExemptStatus { get; set; }
        [JsonProperty("Equipment_Exempt_Status")]
        public string EquipmentExemptStatus { get; set; }
        [JsonProperty("Overhead_Exempt_Status")]
        public string OverheadExemptStatus { get; set; }
        [JsonProperty("Other_Exempt_Status")]
        public string OtherExemptStatus { get; set; }
        [JsonProperty("Resale_Cert")]
        public string ResaleCert { get; set; }
        [JsonProperty("ABN")]
        public string Abn { get; set; }
        [JsonProperty("Warranty")]
        public bool Warranty { get; set; }
        [JsonProperty("Checklist_1_Date")]
        public object Checklist1Date { get; set; }
        [JsonProperty("Checklist_2")]
        public bool Checklist2 { get; set; }
        [JsonProperty("Checklist_2_Date")]
        public object Checklist2Date { get; set; }
        [JsonProperty("Checklist_3")]
        public bool Checklist3 { get; set; }
        [JsonProperty("Checklist_3_Date")]
        public object Checklist3Date { get; set; }
        [JsonProperty("Checklist_4")]
        public bool Checklist4 { get; set; }
        [JsonProperty("Checklist_4_Date")]
        public object Checklist4Date { get; set; }
        [JsonProperty("Checklist_5")]
        public bool Checklist5 { get; set; }
        [JsonProperty("Checklist_5_Date")]
        public object Checklist5Date { get; set; }
        [JsonProperty("Checklist_6")]
        public bool Checklist6 { get; set; }
        [JsonProperty("Checklist_6_Date")]
        public object Checklist6Date { get; set; }
        [JsonProperty("Checklist_7")]
        public bool Checklist7 { get; set; }
        [JsonProperty("Checklist_7_Date")]
        public object Checklist7Date { get; set; }
        [JsonProperty("Checklist_8")]
        public bool Checklist8 { get; set; }
        [JsonProperty("Checklist_8_Date")]
        public object Checklist8Date { get; set; }
        [JsonProperty("Checklist_9")]
        public bool Checklist9 { get; set; }
        [JsonProperty("Checklist_9_Date")]
        public object Checklist9Date { get; set; }
        [JsonProperty("Checklist_10")]
        public bool Checklist10 { get; set; }
        [JsonProperty("Checklist_10_Date")]
        public object Checklist10Date { get; set; }
        [JsonProperty("Invoice_Header")]
        public object InvoiceHeader { get; set; }
        [JsonProperty("Text_2")]
        public object Text2 { get; set; }
        [JsonProperty("Text_3")]
        public object Text3 { get; set; }
        [JsonProperty("Text_4")]
        public object Text4 { get; set; }
        [JsonProperty("Text_5")]
        public object Text5 { get; set; }
        [JsonProperty("Text_6")]
        public object Text6 { get; set; }
        [JsonProperty("AB_Company_ID")]
        public string AbCompanyId { get; set; }
        [JsonProperty("Operator_Stamp")]
        public string OperatorStamp { get; set; }
        [JsonProperty("Date_Stamp")]
        public DateTime DateStamp { get; set; }
        [JsonProperty("Time_Stamp")]
        public TimeStamp TimeStamp { get; set; }
        [JsonProperty("Customer_Notes")]
        public object CustomerNotes { get; set; }
        [JsonProperty("Customer_File_Links")]
        public object CustomerFileLinks { get; set; }
    }
    public class TimeStamp
    {
        [JsonProperty("Ticks")]
        public long Ticks { get; set; }
        [JsonProperty("Days")]
        public int Days { get; set; }
        [JsonProperty("Hours")]
        public int Hours { get; set; }
        [JsonProperty("Milliseconds")]
        public int Milliseconds { get; set; }
        [JsonProperty("Minutes")]
        public int Minutes { get; set; }
        [JsonProperty("Seconds")]
        public int Seconds { get; set; }
        [JsonProperty("TotalDays")]
        public double TotalDays { get; set; }
        [JsonProperty("TotalHours")]
        public double TotalHours { get; set; }
        [JsonProperty("TotalMilliseconds")]
        public int TotalMilliseconds { get; set; }
        [JsonProperty("TotalMinutes")]
        public double TotalMinutes { get; set; }
        [JsonProperty("TotalSeconds")]
        public int TotalSeconds { get; set; }
    }
}