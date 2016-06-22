namespace Sage.WebApi.Infratructure.Service.Implementation
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using BloomService.Domain.Entities.Concrete;

    using Sage.Messaging;
    using Sage.WebApi.Infratructure.Constants;
    using MessageResponse;
    using Common.Logging;

    public class ServiceManagement : IServiceManagement
    {
        private readonly ClaimsAgent claimsAgent;

        private bool isCreated;

        private IMessageBoard messageBoard;

        private MessageTypeDescriptor messageTypeDescriptor;

        private string name;

        private string password;

        private readonly ILog _log = LogManager.GetLogger(typeof(ServiceManagement));

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
            var response = SendMessage(message);
            var assignments = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Assignments;
            var result = AutoMapper.Mapper.Map<SageAssignment[]>(assignments);
            return result;
        }
       
        public object Agreements()
        {
            var result = SendMessage(Messages.Agreements);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam;
        }

        public SageAssignment[] Assignments()
        {
            var response = SendMessage(Messages.Assignments);
            var entities = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Assignments;
            var result = AutoMapper.Mapper.Map<SageAssignment[]>(entities);
            return result;
        }

        public SageAssignment[] Assignments(string number)
        {
            var messages = string.Format(Messages.AssignmentsValue, number);
            var response = SendMessage(messages);
            var entities = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Assignments;
            var result = AutoMapper.Mapper.Map<SageAssignment[]>(entities);
            return result;
        }

        public SageCallType[] Calltypes()
        {
            var result = SendMessage(Messages.Calltypes);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.CallTypes;
        }

        public SageEquipment[] GetEquipmentsByWorkOrderId(string id)
        {
            var message = string.Format(Messages.GetEquipmentsByWorkOrderId, id);
            var response = SendMessage(message);
            var entities = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrderItems;
            var result = AutoMapper.Mapper.Map<SageEquipment[]>(entities);
            return result;
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
            var response = SendMessage(messages);
            var assignments = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Assignments;
            var result = AutoMapper.Mapper.Map<SageAssignment[]>(assignments);
            return result;
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
            var response = SendMessage(Messages.Employees);
            var entities = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Employees;
            var result = AutoMapper.Mapper.Map<SageEmployee[]>(entities);
            return result;
        }

        public SageEquipment[] Equipments()
        {
            var response = SendMessage(Messages.Equipments);
            var entities = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Equipments;
            var result = AutoMapper.Mapper.Map<SageEquipment[]>(entities);
            return result;
        }

        public SageLocation[] Locations()
        {
            var response = SendMessage(Messages.Locations);
            var entities = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Locations;
            var result = AutoMapper.Mapper.Map<SageLocation[]>(entities);
            return result;
        }

        public SagePart[] Parts()
        {
            var response = SendMessage(Messages.Parts);
            var entities = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Parts;
            var result = AutoMapper.Mapper.Map<SagePart[]>(entities);
            return result;
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

        public string SendMessageXml(string message)
        {
            if (!isCreated)
            {
                Create("kris", "sageDEV!!");
            }

            var response = messageBoard.SendMessage(messageTypeDescriptor.Xml, message);
            return response;
        }

        public object SendMessage(string message)
        {
            _log.InfoFormat("Send message: {0}", message);
            if (!isCreated)
            {
                Create("kris", "sageDEV!!");
            }

            var response = messageBoard.SendMessage(messageTypeDescriptor.Xml, message);
            _log.InfoFormat("Response {0}", response);
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
            var response = SendMessage(Messages.WorkOrders);
            var entities = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrders;
            var result = AutoMapper.Mapper.Map<SageWorkOrder[]>(entities);
            return result;
        }

        public SageWorkOrder[] WorkOrders(string number)
        {
            var messages = string.Format(Messages.WorkOrdersValue, number);
            var response = SendMessage(messages);
            var entities = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrders;
            var result = AutoMapper.Mapper.Map<SageWorkOrder[]>(entities);
            return result;
        }

        public SageWorkOrder[] WorkOrders(Dictionary<string, string> properties)
        {
            var propertiesStr = string.Empty;
            foreach (var property in properties)
            {
                propertiesStr += string.Format(Messages.Property, property.Key, property.Value);
            }

            var messages = string.Format(Messages.CreateWorkOrder, propertiesStr);
            var response = SendMessage(messages);
            var entities = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrders;
            var result = AutoMapper.Mapper.Map<SageWorkOrder[]>(entities);
            return result;
        }


        public SageWorkOrder[] EditWorkOrder(Dictionary<string, string> properties)
        {
            var propertiesStr = string.Empty;
            foreach (var property in properties)
            {
                propertiesStr += string.Format(Messages.Property, property.Key, property.Value);
            }

            var messages = string.Format(Messages.EditWorkOrder, propertiesStr);
            var response = SendMessage(messages);
            var entities = (response as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrders;
            var result = AutoMapper.Mapper.Map<SageWorkOrder[]>(entities);
            return result;
        }

        public void UnassignWorkOrder(string id)
        {
            var messages = string.Format(Messages.UnAssignWorkOrder, id);
            var response = SendMessage(messages);
        }
    }
}