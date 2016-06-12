namespace Sage.WebApi.Infratructure.Service.Implementation
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.MessageResponse;
    using BloomService.Domain.Exceptions;

    using Sage.Messaging;
    using Sage.WebApi.Infratructure.Constants;

    public class ServiceManagement : IServiceManagement
    {
        private readonly ClaimsAgent claimsAgent;

        private bool isCreated;

        private IMessageBoard messageBoard;

        private MessageTypeDescriptor messageTypeDescriptor;

        private string name;

        private string password;

        public ServiceManagement(ClaimsAgent claimsAgent, SageWebConfig configConstants)
        {
            this.claimsAgent = claimsAgent;
            CatalogPath = configConstants.CatalogPath;
        }

        public string CatalogPath { get; set; }

        public SageAssignment[] AddAssignments(Dictionary<string, string> properties)
        {
            var propertiesStr = string.Empty;
            foreach (var property in properties)
            {
                propertiesStr += string.Format(Messages.Property, property.Key, property.Value);
            }

            var message = string.Format(Messages.AddAssignment, propertiesStr);
            var result = SendMessage(message);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Assignments;
        }
       
        public object Agreements()
        {
            var result = SendMessage(Messages.Agreements);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam;
        }

        public SageAssignment[] Assignments()
        {
            var result = SendMessage(Messages.Assignments);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Assignments;
        }

        public SageAssignment[] Assignments(string number)
        {
            var messages = string.Format(Messages.AssignmentsValue, number);
            var result = SendMessage(messages);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Assignments;
        }

        public SageCallType[] Calltypes()
        {
            var result = SendMessage(Messages.Calltypes);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.CallTypes;
        }

        public void Create(string name, string password)
        {
            this.password = password;
            this.name = name;
            isCreated = true;
            messageTypeDescriptor = new MessageTypeDescriptor();
            var xmlMessage = string.Format(Messages.MessageTypeDescriptor, name, password);
            messageTypeDescriptor.Xml = xmlMessage;
            messageBoard = MessageBoardFactory.CreateMessageBoardFromDefaultCatalogFile();
            messageBoard.RoutingCatalogPersist.LoadFrom(CatalogPath);
        }

        public SageDepartment[] Departments()
        {
            var result = SendMessage(Messages.Departments);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Departments;
        }

        public SageAssignment[] EditAssignments(Dictionary<string, string> properties)
        {
            var propertiesStr = string.Empty;
            foreach (var property in properties)
            {
                propertiesStr += string.Format(Messages.Property, property.Key, property.Value);
            }

            var messages = string.Format(Messages.EditAssignment, propertiesStr);
            var result = SendMessage(messages);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Assignments;
        }

        public bool AddWorkOrderItem(Dictionary<string, string> properties)
        {
            var propertiesStr = string.Empty;
            foreach (var property in properties)
            {
                propertiesStr += string.Format(Messages.Property, property.Key, property.Value);
            }

            var messages = string.Format(Messages.AddWorkOrderItem, propertiesStr);
            var result = SendMessage(messages);
            return true;
        }

        public SageEmployee[] Employees()
        {
            var result = SendMessage(Messages.Employees);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Employees;
        }

        public SageEquipment[] Equipments()
        {
            var result = SendMessage(Messages.Equipments);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Equipments;
        }

        public SageLocation[] Locations()
        {
            var result = SendMessage(Messages.Locations);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Locations;
        }

        public SagePart[] Parts()
        {
            var result = SendMessage(Messages.Parts);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Parts;
        }

        public IEnumerable<string> PermissionCode()
        {
            var result = SendMessage(Messages.WorkOrders);
            var noDublicate = new HashSet<string>();
            var allRateSheet =
                (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrders.Select(
                    x => x.PermissionCode);
            foreach (var rateSheet in allRateSheet)
            {
                if (noDublicate.All(x => x != rateSheet) && rateSheet != string.Empty)
                {
                    noDublicate.Add(rateSheet);
                }
            }

            return noDublicate;
        }

        public SageProblem[] Problems()
        {
            var result = SendMessage(Messages.Problems);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Problems;
        }

        public IEnumerable<string> RateSheet()
        {
            var result = SendMessage(Messages.WorkOrders);
            var noDublicate = new HashSet<string>();
            var allRateSheet =
                (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrders.Select(
                    x => x.RateSheet);
            foreach (var rateSheet in allRateSheet)
            {
                if (noDublicate.All(x => x != rateSheet) && rateSheet != string.Empty)
                {
                    noDublicate.Add(rateSheet);
                }
            }

            return noDublicate;
        }

        public SageRepair[] Repairs()
        {
            var result = SendMessage(Messages.Repairs);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Repairs;
        }

        public object SendMessage(string message)
        {
            if (!isCreated)
            {
                Create("kris", "sageDEV!!");
            }

            var response = messageBoard.SendMessage(messageTypeDescriptor.Xml, message);

            var serializer = new XmlSerializer(typeof(MessageResponses));
            object result;
            using (TextReader reader = new StringReader(response))
            {
                result = serializer.Deserialize(reader);
            }

            var messageResponses = result as MessageResponses;
            if (messageResponses != null && messageResponses.MessageResponse.Error != null)
            {
                throw new ResponseException(messageResponses.MessageResponse.Error);
            }

            return result;
        }

        public object SendMessage2(string message)
        {
            if (!isCreated)
            {
                Create(claimsAgent.Name, claimsAgent.Password);
            }

            var response = messageBoard.SendMessage(messageTypeDescriptor.Xml, message);

            var serializer = new XmlSerializer(typeof(MessageResponses));
            object result;
            using (TextReader reader = new StringReader(response))
            {
                result = serializer.Deserialize(reader);
            }

            var messageResponses = result as MessageResponses;
            if (messageResponses != null && messageResponses.MessageResponse.Error != null)
            {
                throw new ResponseException(messageResponses.MessageResponse.Error);
            }

            return result;
        }

        public SageWorkOrder[] WorkOrders()
        {
            var result = SendMessage(Messages.WorkOrders);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrders;
        }

        public SageWorkOrder[] WorkOrders(string number)
        {
            var messages = string.Format(Messages.WorkOrdersValue, number);
            var result = SendMessage(messages);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrders;
        }

        public SageWorkOrder[] WorkOrders(Dictionary<string, string> properties)
        {
            var propertiesStr = string.Empty;
            foreach (var property in properties)
            {
                propertiesStr += string.Format(Messages.Property, property.Key, property.Value);
            }

            var messages = string.Format(Messages.CreateWorkOrder, propertiesStr);
            var result = SendMessage(messages);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrders;
        }
    }
}