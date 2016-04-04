namespace BloomService.Domain.Entities
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SagePart
    {
        private string partField;

        private string partNumberField;

        private string aKAField;

        private string descriptionField;

        private string manufacturerNumberField;

        private string manufacturerField;

        private string modelField;

        private string equipmentTypeField;

        private string serializedField;

        private string lottedField;

        private string allowFractionsField;

        private string autoGenerateEquipmentField;

        private string binLocationField;

        private string productField;

        private string standardCostField;

        private string averageCostField;

        private string exchangeCostField;

        private string usedCostField;

        private string level1PriceField;

        private string level2PriceField;

        private string level3PriceField;

        private string exchangePriceField;

        private string usedPriceField;

        private string agreementPriceField;

        private string reimbursementAmountField;

        private string badInventoryValueField;

        private string quantityonHandField;

        private string quantityonOrderField;

        private string quantityAllocatedField;

        private string warningLevelField;

        private string minimumStockQuantityField;

        private string reorderQuantityField;

        private string totalQuantityField;

        private string vendorField;

        private string lastPriceField;

        private string warrantyMonthsField;

        private string lastOrderDateField;

        private string lastReceivedDateField;

        private string lastActivityDateField;

        private string orderLeadTimeField;

        private string inactiveField;

        private string unitField;

        private string inStockField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Part
        {
            get
            {
                return this.partField;
            }
            set
            {
                this.partField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PartNumber
        {
            get
            {
                return this.partNumberField;
            }
            set
            {
                this.partNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AKA
        {
            get
            {
                return this.aKAField;
            }
            set
            {
                this.aKAField = value;
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
        public string ManufacturerNumber
        {
            get
            {
                return this.manufacturerNumberField;
            }
            set
            {
                this.manufacturerNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Manufacturer
        {
            get
            {
                return this.manufacturerField;
            }
            set
            {
                this.manufacturerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Model
        {
            get
            {
                return this.modelField;
            }
            set
            {
                this.modelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string EquipmentType
        {
            get
            {
                return this.equipmentTypeField;
            }
            set
            {
                this.equipmentTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Serialized
        {
            get
            {
                return this.serializedField;
            }
            set
            {
                this.serializedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Lotted
        {
            get
            {
                return this.lottedField;
            }
            set
            {
                this.lottedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AllowFractions
        {
            get
            {
                return this.allowFractionsField;
            }
            set
            {
                this.allowFractionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("Auto-GenerateEquipment")]
        public string AutoGenerateEquipment
        {
            get
            {
                return this.autoGenerateEquipmentField;
            }
            set
            {
                this.autoGenerateEquipmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BinLocation
        {
            get
            {
                return this.binLocationField;
            }
            set
            {
                this.binLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Product
        {
            get
            {
                return this.productField;
            }
            set
            {
                this.productField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StandardCost
        {
            get
            {
                return this.standardCostField;
            }
            set
            {
                this.standardCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AverageCost
        {
            get
            {
                return this.averageCostField;
            }
            set
            {
                this.averageCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ExchangeCost
        {
            get
            {
                return this.exchangeCostField;
            }
            set
            {
                this.exchangeCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UsedCost
        {
            get
            {
                return this.usedCostField;
            }
            set
            {
                this.usedCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Level1Price
        {
            get
            {
                return this.level1PriceField;
            }
            set
            {
                this.level1PriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Level2Price
        {
            get
            {
                return this.level2PriceField;
            }
            set
            {
                this.level2PriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Level3Price
        {
            get
            {
                return this.level3PriceField;
            }
            set
            {
                this.level3PriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ExchangePrice
        {
            get
            {
                return this.exchangePriceField;
            }
            set
            {
                this.exchangePriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UsedPrice
        {
            get
            {
                return this.usedPriceField;
            }
            set
            {
                this.usedPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AgreementPrice
        {
            get
            {
                return this.agreementPriceField;
            }
            set
            {
                this.agreementPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ReimbursementAmount
        {
            get
            {
                return this.reimbursementAmountField;
            }
            set
            {
                this.reimbursementAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BadInventoryValue
        {
            get
            {
                return this.badInventoryValueField;
            }
            set
            {
                this.badInventoryValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string QuantityonHand
        {
            get
            {
                return this.quantityonHandField;
            }
            set
            {
                this.quantityonHandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string QuantityonOrder
        {
            get
            {
                return this.quantityonOrderField;
            }
            set
            {
                this.quantityonOrderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string QuantityAllocated
        {
            get
            {
                return this.quantityAllocatedField;
            }
            set
            {
                this.quantityAllocatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WarningLevel
        {
            get
            {
                return this.warningLevelField;
            }
            set
            {
                this.warningLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MinimumStockQuantity
        {
            get
            {
                return this.minimumStockQuantityField;
            }
            set
            {
                this.minimumStockQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ReorderQuantity
        {
            get
            {
                return this.reorderQuantityField;
            }
            set
            {
                this.reorderQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalQuantity
        {
            get
            {
                return this.totalQuantityField;
            }
            set
            {
                this.totalQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Vendor
        {
            get
            {
                return this.vendorField;
            }
            set
            {
                this.vendorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastPrice
        {
            get
            {
                return this.lastPriceField;
            }
            set
            {
                this.lastPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WarrantyMonths
        {
            get
            {
                return this.warrantyMonthsField;
            }
            set
            {
                this.warrantyMonthsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastOrderDate
        {
            get
            {
                return this.lastOrderDateField;
            }
            set
            {
                this.lastOrderDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastReceivedDate
        {
            get
            {
                return this.lastReceivedDateField;
            }
            set
            {
                this.lastReceivedDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastActivityDate
        {
            get
            {
                return this.lastActivityDateField;
            }
            set
            {
                this.lastActivityDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string OrderLeadTime
        {
            get
            {
                return this.orderLeadTimeField;
            }
            set
            {
                this.orderLeadTimeField = value;
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
        public string Unit
        {
            get
            {
                return this.unitField;
            }
            set
            {
                this.unitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InStock
        {
            get
            {
                return this.inStockField;
            }
            set
            {
                this.inStockField = value;
            }
        }
    }


}