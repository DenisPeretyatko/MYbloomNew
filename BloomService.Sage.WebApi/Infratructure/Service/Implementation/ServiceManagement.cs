using Sage.Messaging;
using Sage.WebApi.Infratructure.Constants;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Sage.WebApi.Infratructure.Service.Implementation
{
    using BloomService.Domain.Entities;
    using BloomService.Domain.Exceptions;

    public class ServiceManagement: IServiceManagement
    {
        public MessageTypeDescriptor mtd;
        public IMessageBoard mb;
        public ClaimsAgent ClaimsAgent;
        public bool Created;
        public string Password;
        public string Name;
        public string CatalogPath { get; set; }
        public ServiceManagement(ClaimsAgent claimsAgent, SageWebConfig configConstants)
        {
            ClaimsAgent = claimsAgent;
            CatalogPath = configConstants.CatalogPath;
        }  
        public void Create(string name, string password)
        {
            Password = password;
            Name = name;
            Created = true;
            mtd = new MessageTypeDescriptor();
            var xmlMessage = string.Format(Messages.MessageTypeDescriptor, name, password);
            mtd.Xml = xmlMessage;
            mb = MessageBoardFactory.CreateMessageBoardFromDefaultCatalogFile();
            mb.RoutingCatalogPersist.LoadFrom(CatalogPath);
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

        public SageProblem[] Problems()
        {
            var result = SendMessage(Messages.Problems);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Problems;
        }

        public SageEmployee[] Employees()
        {
            var result = SendMessage(Messages.Employees);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Employees;
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

        public SageWorkOrder[] WorkOrders(Properties properties)
        {
            var propertiesStr = string.Empty;
            foreach(var property in properties)
            {
                propertiesStr += string.Format(Messages.Property, property.Key, property.Value);
            }
            var messages = string.Format(Messages.CreateWorkOrder, propertiesStr);
            var result = SendMessage(messages);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrders;
        }

        public SageRepair[] Repairs()
        {
            var result = SendMessage(Messages.Repairs);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Repairs;
        }

        public SageCallType[] Calltypes()
        {
            var result = SendMessage(Messages.Calltypes);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.CallTypes;
        }

        public SageDepartment[] Departments()
        {
            var result = SendMessage(Messages.Departments);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Departments;
        }

        public SageEquipment[] Equipments()
        {
            var result = SendMessage(Messages.Equipments);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Equipments;
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

        public SageAssignment[] AddAssignments(Properties properties)
        {
            var propertiesStr = string.Empty;
            foreach (var property in properties)
            {
                propertiesStr += string.Format(Messages.Property, property.Key, property.Value);
            }
            var messages = string.Format(Messages.AddAssignment, propertiesStr);
            var result = SendMessage(messages);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Assignments;
        }

        public SageAssignment[] EditAssignments(Properties properties)
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

        public object Agreements()
        {
            var result = SendMessage(Messages.Agreements);
            return (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam;
        }        

        public IEnumerable<string> RateSheet()
        {
            var result = SendMessage(Messages.WorkOrders);
            var noDublicate = new HashSet<string>();
            var allRateSheet = (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrders.Select(x => x.RateSheet);
            foreach(var rateSheet in allRateSheet)
            {
                if (noDublicate.All(x => x != rateSheet) && rateSheet != string.Empty)
                    noDublicate.Add(rateSheet);
            }
            return noDublicate;
        }       

        public IEnumerable<string> PermissionCode()
        {
            var result = SendMessage(Messages.WorkOrders);
            var noDublicate = new HashSet<string>();
            var allRateSheet = (result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.WorkOrders.Select(x => x.PermissionCode);
            foreach (var rateSheet in allRateSheet)
            {
                if (noDublicate.All(x => x != rateSheet) && rateSheet !=string.Empty)
                    noDublicate.Add(rateSheet);
            }
            return noDublicate;
        }

        public object SendMessage(string message)
        {
            if (!Created)
                Create(ClaimsAgent.Name, ClaimsAgent.Password);
            
            var sResponse1 = mb.SendMessage(mtd.Xml, message);
            var serializer = new XmlSerializer(typeof(MessageResponses));
            object result;
            using (TextReader reader = new StringReader(sResponse1))
            {
                result = serializer.Deserialize(reader);
            }
            var messageResponses = (result as MessageResponses);
            if (messageResponses != null && messageResponses.MessageResponse.Error != null)
                throw new ResponseException(messageResponses.MessageResponse.Error);
            return result;
        }
    }
}