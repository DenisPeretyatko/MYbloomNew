using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sage.WebApi.Models.SerializeModels
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SageCallType
    {
        private string callTypeField;

        private string descriptionField;

        private string serviceatCenterField;

        private string agreementRequiredField;

        private string callBackField;

        private string inactiveField;

        private string rateSheetField;

        private string jobSaleProductField;

        private string laborProductField;

        private string partsProductField;

        private string miscellaneousProductField;

        private string flatRateLaborProductField;

        private string flatRatePartsProductField;

        private string workOrderTypeField;

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
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ServiceatCenter
        {
            get
            {
                return this.serviceatCenterField;
            }
            set
            {
                this.serviceatCenterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AgreementRequired
        {
            get
            {
                return this.agreementRequiredField;
            }
            set
            {
                this.agreementRequiredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CallBack
        {
            get
            {
                return this.callBackField;
            }
            set
            {
                this.callBackField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Inactive
        {
            get
            {
                return this.inactiveField;
            }
            set
            {
                this.inactiveField = value;
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
        public string LaborProduct
        {
            get
            {
                return this.laborProductField;
            }
            set
            {
                this.laborProductField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PartsProduct
        {
            get
            {
                return this.partsProductField;
            }
            set
            {
                this.partsProductField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MiscellaneousProduct
        {
            get
            {
                return this.miscellaneousProductField;
            }
            set
            {
                this.miscellaneousProductField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FlatRateLaborProduct
        {
            get
            {
                return this.flatRateLaborProductField;
            }
            set
            {
                this.flatRateLaborProductField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FlatRatePartsProduct
        {
            get
            {
                return this.flatRatePartsProductField;
            }
            set
            {
                this.flatRatePartsProductField = value;
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
    }

}