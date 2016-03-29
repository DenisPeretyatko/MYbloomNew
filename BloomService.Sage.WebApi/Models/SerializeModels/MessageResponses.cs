namespace Sage.WebApi.Models.SerializeModels
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class MessageResponses
    {

        private MessageResponsesMessageResponse messageResponseField;

        private string[] textField;

        /// <remarks/>
        public MessageResponsesMessageResponse MessageResponse
        {
            get
            {
                return this.messageResponseField;
            }
            set
            {
                this.messageResponseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
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
                return this.errorField;
            }
            set
            {
                this.errorField = value;
            }
        }

        /// <remarks/>
        public MessageResponsesMessageResponseReturnParams ReturnParams
        {
            get
            {
                return this.returnParamsField;
            }
            set
            {
                this.returnParamsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TargetId
        {
            get
            {
                return this.targetIdField;
            }
            set
            {
                this.targetIdField = value;
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
    }

    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
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
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }



    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageResponsesMessageResponseReturnParams
    {

        private MessageResponsesMessageResponseReturnParamsReturnParam returnParamField;

        private string[] textField;

        /// <remarks/>
        public MessageResponsesMessageResponseReturnParamsReturnParam ReturnParam
        {
            get
            {
                return this.returnParamField;
            }
            set
            {
                this.returnParamField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }


        /// <remarks/>
        
    }

    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
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
                return this.locationsField;
            }
            set
            {
                this.locationsField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Part", IsNullable = false)]
        public SagePart[] Parts
        {
            get
            {
                return this.partsField;
            }
            set
            {
                this.partsField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Problem", IsNullable = false)]
        public SageProblem[] Problems
        {
            get
            {
                return this.problemsField;
            }
            set
            {
                this.problemsField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Employee", IsNullable = false)]
        public SageEmployee[] Employees
        {
            get
            {
                return this.employeesField;
            }
            set
            {
                this.employeesField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("WorkOrder", IsNullable = false)]
        public SageWorkOrder[] WorkOrders
        {
            get
            {
                return this.workOrdersField;
            }
            set
            {
                this.workOrdersField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Repair", IsNullable = false)]
        public SageRepair[] Repairs
        {
            get
            {
                return this.repairsField;
            }
            set
            {
                this.repairsField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("CallType", IsNullable = false)]
        public SageCallType[] CallTypes
        {
            get
            {
                return this.callType;
            }
            set
            {
                this.callType = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Department", IsNullable = false)]
        public SageDepartment[] Departments
        {
            get
            {
                return this.departments;
            }
            set
            {
                this.departments = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Equipment", IsNullable = false)]
        public SageEquipment[] Equipments
        {
            get
            {
                return this.equipments;
            }
            set
            {
                this.equipments = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Assignment", IsNullable = false)]
        public SageAssignment[] Assignments
        {
            get
            {
                return this.assignmentsField;
            }
            set
            {
                this.assignmentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
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
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

    }
}