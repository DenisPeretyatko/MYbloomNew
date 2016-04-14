using BloomService.Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace BloomService.Domain.Entities
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [CollectionNameAttribute("PartCollection")]
    public partial class SagePart : SageEntity
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
        [BsonId]
        public string Part
        {
            get
            {
                return partField;
            }
            set
            {
                partField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PartNumber
        {
            get
            {
                return partNumberField;
            }
            set
            {
                partNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AKA
        {
            get
            {
                return aKAField;
            }
            set
            {
                aKAField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Description
        {
            get
            {
                return descriptionField;
            }
            set
            {
                descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ManufacturerNumber
        {
            get
            {
                return manufacturerNumberField;
            }
            set
            {
                manufacturerNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Manufacturer
        {
            get
            {
                return manufacturerField;
            }
            set
            {
                manufacturerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Model
        {
            get
            {
                return modelField;
            }
            set
            {
                modelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string EquipmentType
        {
            get
            {
                return equipmentTypeField;
            }
            set
            {
                equipmentTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Serialized
        {
            get
            {
                return serializedField;
            }
            set
            {
                serializedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Lotted
        {
            get
            {
                return lottedField;
            }
            set
            {
                lottedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AllowFractions
        {
            get
            {
                return allowFractionsField;
            }
            set
            {
                allowFractionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("Auto-GenerateEquipment")]
        public string AutoGenerateEquipment
        {
            get
            {
                return autoGenerateEquipmentField;
            }
            set
            {
                autoGenerateEquipmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BinLocation
        {
            get
            {
                return binLocationField;
            }
            set
            {
                binLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Product
        {
            get
            {
                return productField;
            }
            set
            {
                productField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StandardCost
        {
            get
            {
                return standardCostField;
            }
            set
            {
                standardCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AverageCost
        {
            get
            {
                return averageCostField;
            }
            set
            {
                averageCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ExchangeCost
        {
            get
            {
                return exchangeCostField;
            }
            set
            {
                exchangeCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UsedCost
        {
            get
            {
                return usedCostField;
            }
            set
            {
                usedCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Level1Price
        {
            get
            {
                return level1PriceField;
            }
            set
            {
                level1PriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Level2Price
        {
            get
            {
                return level2PriceField;
            }
            set
            {
                level2PriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Level3Price
        {
            get
            {
                return level3PriceField;
            }
            set
            {
                level3PriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ExchangePrice
        {
            get
            {
                return exchangePriceField;
            }
            set
            {
                exchangePriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UsedPrice
        {
            get
            {
                return usedPriceField;
            }
            set
            {
                usedPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AgreementPrice
        {
            get
            {
                return agreementPriceField;
            }
            set
            {
                agreementPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ReimbursementAmount
        {
            get
            {
                return reimbursementAmountField;
            }
            set
            {
                reimbursementAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BadInventoryValue
        {
            get
            {
                return badInventoryValueField;
            }
            set
            {
                badInventoryValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string QuantityonHand
        {
            get
            {
                return quantityonHandField;
            }
            set
            {
                quantityonHandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string QuantityonOrder
        {
            get
            {
                return quantityonOrderField;
            }
            set
            {
                quantityonOrderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string QuantityAllocated
        {
            get
            {
                return quantityAllocatedField;
            }
            set
            {
                quantityAllocatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WarningLevel
        {
            get
            {
                return warningLevelField;
            }
            set
            {
                warningLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MinimumStockQuantity
        {
            get
            {
                return minimumStockQuantityField;
            }
            set
            {
                minimumStockQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ReorderQuantity
        {
            get
            {
                return reorderQuantityField;
            }
            set
            {
                reorderQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalQuantity
        {
            get
            {
                return totalQuantityField;
            }
            set
            {
                totalQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Vendor
        {
            get
            {
                return vendorField;
            }
            set
            {
                vendorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastPrice
        {
            get
            {
                return lastPriceField;
            }
            set
            {
                lastPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WarrantyMonths
        {
            get
            {
                return warrantyMonthsField;
            }
            set
            {
                warrantyMonthsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastOrderDate
        {
            get
            {
                return lastOrderDateField;
            }
            set
            {
                lastOrderDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastReceivedDate
        {
            get
            {
                return lastReceivedDateField;
            }
            set
            {
                lastReceivedDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastActivityDate
        {
            get
            {
                return lastActivityDateField;
            }
            set
            {
                lastActivityDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string OrderLeadTime
        {
            get
            {
                return orderLeadTimeField;
            }
            set
            {
                orderLeadTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Inactive
        {
            get
            {
                return inactiveField;
            }
            set
            {
                inactiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Unit
        {
            get
            {
                return unitField;
            }
            set
            {
                unitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InStock
        {
            get
            {
                return inStockField;
            }
            set
            {
                inStockField = value;
            }
        }
    }


}