using System.Xml.Serialization;

namespace BloomService.Domain.Entities
{
    /// <remarks/>
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class MessageResponses : SageEntity
    {

        private MessageResponsesMessageResponse messageResponseField;

        private string[] textField;

        /// <remarks/>
        public MessageResponsesMessageResponse MessageResponse
        {
            get
            {
                return messageResponseField;
            }
            set
            {
                messageResponseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return textField;
            }
            set
            {
                textField = value;
            }
        }
    }

    /// <remarks/>
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageResponsesMessageResponse
    {

        private MessageResponsesMessageResponseReturnParams returnParamsField;

        private MessageResponsesMessageResponseError errorField;

        private string[] textField;

        private string targetIdField;

        private string statusField;

        public MessageResponsesMessageResponseError Error
        {
            get
            {
                return errorField;
            }
            set
            {
                errorField = value;
            }
        }

        /// <remarks/>
        public MessageResponsesMessageResponseReturnParams ReturnParams
        {
            get
            {
                return returnParamsField;
            }
            set
            {
                returnParamsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return textField;
            }
            set
            {
                textField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TargetId
        {
            get
            {
                return targetIdField;
            }
            set
            {
                targetIdField = value;
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
    }

    [XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageResponsesMessageResponseError
    {

        private byte codeField;

        private string messageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Code
        {
            get
            {
                return codeField;
            }
            set
            {
                codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Message
        {
            get
            {
                return messageField;
            }
            set
            {
                messageField = value;
            }
        }
    }



    /// <remarks/>
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageResponsesMessageResponseReturnParams
    {

        private MessageResponsesMessageResponseReturnParamsReturnParam returnParamField;

        private string[] textField;

        /// <remarks/>
        public MessageResponsesMessageResponseReturnParamsReturnParam ReturnParam
        {
            get
            {
                return returnParamField;
            }
            set
            {
                returnParamField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return textField;
            }
            set
            {
                textField = value;
            }
        }


        /// <remarks/>
        
    }

    [XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageResponsesMessageResponseReturnParamsReturnParam
    {
        private SageLocation[] locationsField;

        private SagePart[] partsField;

        private SageProblem[] problemsField;

        private SageEmployee[] employeesField;

        private SageWorkOrder[] workOrdersField;

        private SageRepair[] repairsField;

        private SageCallType[] callType;

        private SageEquipment[] equipments;

        private SageDepartment[] departments;

        private SageAssignment[] assignmentsField;

        private string[] textField;

        private string nameField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Location", IsNullable = false)]
        public SageLocation[] Locations
        {
            get
            {
                return locationsField;
            }
            set
            {
                locationsField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Part", IsNullable = false)]
        public SagePart[] Parts
        {
            get
            {
                return partsField;
            }
            set
            {
                partsField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Problem", IsNullable = false)]
        public SageProblem[] Problems
        {
            get
            {
                return problemsField;
            }
            set
            {
                problemsField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Employee", IsNullable = false)]
        public SageEmployee[] Employees
        {
            get
            {
                return employeesField;
            }
            set
            {
                employeesField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("WorkOrder", IsNullable = false)]
        public SageWorkOrder[] WorkOrders
        {
            get
            {
                return workOrdersField;
            }
            set
            {
                workOrdersField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Repair", IsNullable = false)]
        public SageRepair[] Repairs
        {
            get
            {
                return repairsField;
            }
            set
            {
                repairsField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("CallType", IsNullable = false)]
        public SageCallType[] CallTypes
        {
            get
            {
                return callType;
            }
            set
            {
                callType = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Department", IsNullable = false)]
        public SageDepartment[] Departments
        {
            get
            {
                return departments;
            }
            set
            {
                departments = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Equipment", IsNullable = false)]
        public SageEquipment[] Equipments
        {
            get
            {
                return equipments;
            }
            set
            {
                equipments = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Assignment", IsNullable = false)]
        public SageAssignment[] Assignments
        {
            get
            {
                return assignmentsField;
            }
            set
            {
                assignmentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return textField;
            }
            set
            {
                textField = value;
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
        public string Type
        {
            get
            {
                return typeField;
            }
            set
            {
                typeField = value;
            }
        }

    }
}