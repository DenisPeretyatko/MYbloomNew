namespace BloomService.Web.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BloomService.Domain.Entities;

    using RestSharp;

    public class SageApiManager : ISageApiManager
    {
        private readonly IRestClient restClient;

        private string token;

        public SageApiManager(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public string CatalogPath
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private string Token
        {
            get
            {
                if (token != null)
                {
                    return token;
                }

                return GetAuthToken();
            }

            set
            {
                token = value;
            }
        }

        public IEnumerable<SageAssignment> AddAssignments(Properties properties)
        {
            throw new NotImplementedException();
        }

        public object Agreements()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SageAssignment> Assignments()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SageAssignment> Assignments(string number)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SageCallType> Calltypes()
        {
            throw new NotImplementedException();
        }

        public void Create(string name, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SageDepartment> Departments()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SageAssignment> EditAssignments(Properties properties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SageEmployee> Employees()
        {
            var request = new RestRequest("api/v1/sm/employees", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageEmployee>>(request);
            var result = response.Data.ToList();
            return result;
        }

        public IEnumerable<SageEquipment> Equipment()
        {
            var request = new RestRequest("api/v1/sm/equipments", Method.GET);
            var response = restClient.Execute<List<SageEquipment>>(request);
            var result = response.Data.AsQueryable();
            return result;
        }

        public IEnumerable<SageEquipment> Equipments()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SageLocation> Locations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SagePart> Parts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> PermissionCode()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SageProblem> Problems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> RateSheet()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SageRepair> Repairs()
        {
            throw new NotImplementedException();
        }

        public object SendMessage(string message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SageWorkOrder> Workorders(string id)
        {
            var request = new RestRequest("workorders", Method.GET);
            request.AddUrlSegment("id", id);
            var response = restClient.Execute<List<SageWorkOrder>>(request);
            var result = response.Data.AsQueryable();
            return result;
        }

        public IEnumerable<SageWorkOrder> Workorders(Properties properties)
        {
            var request = new RestRequest("workorders", Method.GET);
            request.AddObject(properties);
            var response = restClient.Execute<List<SageWorkOrder>>(request);
            var result = response.Data.AsQueryable();
            return result;
        }

        public IEnumerable<SageWorkOrder> WorkOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SageWorkOrder> WorkOrders(string number)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SageWorkOrder> WorkOrders(Properties properties)
        {
            throw new NotImplementedException();
        }

        private string GetAuthToken()
        {
            var request = new RestRequest("oauth/token", Method.POST);
            request.AddParameter("username", "kris");
            request.AddParameter("password", "sageDEV!!");
            request.AddParameter("grant_type", "password");
            var response = restClient.Execute(request);
            var result = response.Headers.ToList().Find(x => x.Name == "access_token").Value.ToString();
            return result;
        }
    }
}