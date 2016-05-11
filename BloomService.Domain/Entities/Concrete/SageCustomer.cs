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
        public string AB_Company_ID { get; set; }

        [XmlAttribute]
        public string ABN { get; set; }

        [XmlAttribute]
        public string Addon_Table { get; set; }

        [XmlAttribute]
        public string Address_1 { get; set; }

        [XmlAttribute]
        public string Address_2 { get; set; }

        [XmlAttribute]
        public string Address_3 { get; set; }

        [XmlAttribute]
        public string Address_4 { get; set; }

        [XmlAttribute]
        public string Auto_Text { get; set; }

        [XmlAttribute]
        public string Billing { get; set; }

        [XmlAttribute]
        public string Billing_Address_1 { get; set; }

        [XmlAttribute]
        public string Billing_Address_3 { get; set; }

        [XmlAttribute]
        public string Billing_Address_4 { get; set; }

        [XmlAttribute]
        public string Billing_City { get; set; }

        [XmlAttribute]
        public string Billing_Contact { get; set; }

        [XmlAttribute]
        public string Billing_Email_Address { get; set; }

        [XmlAttribute]
        public string Billing_Freq { get; set; }

        [XmlAttribute]
        public string Billing_State { get; set; }

        [XmlAttribute]
        public string Billing_ZIP_Code { get; set; }

        [XmlAttribute]
        public object Checklist_1_Date { get; set; }

        [XmlAttribute]
        public bool Checklist_10 { get; set; }

        [XmlAttribute]
        public object Checklist_10_Date { get; set; }

        [XmlAttribute]
        public bool Checklist_2 { get; set; }

        [XmlAttribute]
        public object Checklist_2_Date { get; set; }

        [XmlAttribute]
        public bool Checklist_3 { get; set; }

        [XmlAttribute]
        public object Checklist_3_Date { get; set; }

        [XmlAttribute]
        public bool Checklist_4 { get; set; }

        [XmlAttribute]
        public object Checklist_4_Date { get; set; }

        [XmlAttribute]
        public bool Checklist_5 { get; set; }

        [XmlAttribute]
        public object Checklist_5_Date { get; set; }

        [XmlAttribute]
        public bool Checklist_6 { get; set; }

        [XmlAttribute]
        public object Checklist_6_Date { get; set; }

        [XmlAttribute]
        public bool Checklist_7 { get; set; }

        [XmlAttribute]
        public object Checklist_7_Date { get; set; }

        [XmlAttribute]
        public bool Checklist_8 { get; set; }

        [XmlAttribute]
        public object Checklist_8_Date { get; set; }

        [XmlAttribute]
        public bool Checklist_9 { get; set; }

        [XmlAttribute]
        public object Checklist_9_Date { get; set; }

        [XmlAttribute]
        public string City { get; set; }

        [XmlAttribute]
        public string Contact_10 { get; set; }

        [XmlAttribute]
        public string Contact_2 { get; set; }

        [XmlAttribute]
        public string Contact_3 { get; set; }

        [XmlAttribute]
        public string Contact_4 { get; set; }

        [XmlAttribute]
        public string Contact_5 { get; set; }

        [XmlAttribute]
        public string Contact_6 { get; set; }

        [XmlAttribute]
        public string Contact_7 { get; set; }

        [XmlAttribute]
        public string Contact_8 { get; set; }

        [XmlAttribute]
        public string Contact_9 { get; set; }

        [XmlAttribute]
        public int Credit_Limit { get; set; }

        [XmlAttribute]
        public string Credit_Rating { get; set; }

        [XmlAttribute]
        public string Customer { get; set; }

        [XmlAttribute]
        public object Customer_File_Links { get; set; }

        [XmlAttribute]
        public object Customer_Notes { get; set; }

        [XmlAttribute]
        public string Customer_Type { get; set; }

        [XmlAttribute]
        public DateTime Date_Established { get; set; }

        [XmlAttribute]
        public DateTime Date_Stamp { get; set; }

        [XmlAttribute]
        public int Days_Before_Due { get; set; }

        [XmlAttribute]
        public int Discount { get; set; }

        [XmlAttribute]
        public string Email_Address { get; set; }

        [XmlAttribute]
        public string Equipment_Exempt_Status { get; set; }

        [XmlAttribute]
        public string Equipment_Rate_Table { get; set; }

        [XmlAttribute]
        public bool Exempt_from_Finance_Charge { get; set; }

        [XmlAttribute]
        public string Fax { get; set; }

        [XmlAttribute]
        public int Finance_Charge_Flat_Rate { get; set; }

        [XmlAttribute]
        public int Finance_Charge_Percentage_Rate { get; set; }

        [XmlAttribute]
        public string Finance_Charge_Rate_Type { get; set; }

        [XmlAttribute]
        public string GL_Prefix { get; set; }

        [XmlAttribute]
        public string Invoice_Format { get; set; }

        [XmlAttribute]
        public object Invoice_Header { get; set; }

        [XmlAttribute]
        public string Key_1 { get; set; }

        [XmlAttribute]
        public string Key_2 { get; set; }

        [XmlAttribute]
        public string Labor_Exempt_Status { get; set; }

        [XmlAttribute]
        public string Labor_Rate_Table { get; set; }

        [XmlAttribute]
        public int LastAutoNumber { get; set; }

        [XmlAttribute]
        public string Markup_Table { get; set; }

        [XmlAttribute]
        public string Material_Exempt_Status { get; set; }

        [XmlAttribute]
        public string Material_Rate_Table { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Operator_Stamp { get; set; }

        [XmlAttribute]
        public string Other_Exempt_Status { get; set; }

        [XmlAttribute]
        public string Other_Rate_Table { get; set; }

        [XmlAttribute]
        public string Overhead_Exempt_Status { get; set; }

        [XmlAttribute]
        public string Overhead_Rate_Table { get; set; }

        [XmlAttribute]
        public bool PO_Required { get; set; }

        [XmlAttribute]
        public string Resale_Cert { get; set; }

        [XmlAttribute]
        public int RetainRetainageage { get; set; }

        [XmlAttribute]
        public bool Send_Statement { get; set; }

        [XmlAttribute]
        public string SM_Invoice_Format { get; set; }

        [XmlAttribute]
        public string State { get; set; }

        [XmlAttribute]
        public string Status { get; set; }

        [XmlAttribute]
        public string Subcontract_Exempt_Status { get; set; }

        [XmlAttribute]
        public string Subcontract_Rate_Table { get; set; }

        [XmlAttribute]
        public string Tax_Group { get; set; }

        [XmlAttribute]
        public string Telephone { get; set; }

        [XmlAttribute]
        public object Text_2 { get; set; }

        [XmlAttribute]
        public object Text_3 { get; set; }

        [XmlAttribute]
        public object Text_4 { get; set; }

        [XmlAttribute]
        public object Text_5 { get; set; }

        [XmlAttribute]
        public object Text_6 { get; set; }

        [XmlAttribute]
        public TimeStamp Time_Stamp { get; set; }

        [XmlAttribute]
        public string Total_Billed_Exempt_Status { get; set; }

        [XmlAttribute]
        public string Trade { get; set; }

        [XmlAttribute]
        public bool Warranty { get; set; }

        [XmlAttribute]
        public string ZIP_Code { get; set; }
    }
}