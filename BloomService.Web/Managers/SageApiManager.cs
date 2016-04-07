namespace BloomService.Web.Managers
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities;

    using RestSharp;
    using Utils;

    public class SageApiManager : ISageApiManager
    {
        private readonly IRestClient restClient;
        private readonly ISession session;

        public SageApiManager(IRestClient restClient, ISession session)
        {
            this.restClient = restClient;
            this.session = session;
        }

        private string Token
        {
            get
            {
                if (session.Session["oauth_token"] != null)
                {
                    return session.Session["oauth_token"].ToString();
                }

                return GetAuthToken();
            }

            set
            {
                session.Session["oauth_token"] = value;
            }
        }

        public IEnumerable<SageAssignment> AddAssignments(Properties properties)
        {
            var request = new RestRequest("api/v1/sm/addassignment", Method.POST);
            request.AddObject(properties);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageAssignment>>(request);
            var result = response.Data;
            return result;
        }

        public IEnumerable<SageCallType> Calltypes()
        {
            var request = new RestRequest("api/v1/sm/calltypes", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageCallType>>(request);
            var result = response.Data;
            return result;
        }
    
        public IEnumerable<SageDepartment> Departments()
        {
            var request = new RestRequest("api/v1/sm/departments", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageDepartment>>(request);
            var result = response.Data;
            return result;
        }

        public IEnumerable<SageAssignment> EditAssignments(Properties properties)
        {
            var request = new RestRequest("api/v1/sm/editassignments", Method.POST);
            request.AddObject(properties);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageAssignment>>(request);
            var result = response.Data;
            return result;
        }

        public IEnumerable<SageEmployee> Employees()
        {
            var request = new RestRequest("api/v1/sm/employees", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageEmployee>>(request);
            var result = response.Data;
            return result;
        }

        public IEnumerable<SageEquipment> Equipment()
        {
            var request = new RestRequest("api/v1/sm/equipment", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageEquipment>>(request);
            var result = response.Data;
            return result;
        }

        public IEnumerable<SageLocation> Locations()
        {
            var request = new RestRequest("api/v1/sm/locations", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageLocation>>(request);
            var result = response.Data;
            return result;
        }

        public IEnumerable<SagePart> Parts()
        {
            var request = new RestRequest("api/v1/sm/parts", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SagePart>>(request);
            var result = response.Data;
            return result;
        }

        public IEnumerable<string> PermissionCode()
        {
            var request = new RestRequest("api/v1/sm/permissioncodes", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<string>>(request);
            var result = response.Data;
            return result;
        }

        public IEnumerable<SageProblem> Problems()
        {
            var request = new RestRequest("api/v1/sm/problems", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageProblem>>(request);
            var result = response.Data;
            return result;
        }

        public IEnumerable<string> RateSheet()
        {
            var request = new RestRequest("api/v1/sm/ratesheet", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<string>>(request);
            var result = response.Data;
            return result;
        }

        public IEnumerable<SageRepair> Repairs()
        {
            var request = new RestRequest("api/v1/sm/repairs", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageRepair>>(request);
            var result = response.Data;
            return result;
        }

        public SageWorkOrder Workorders(string id)
        {
            var request = new RestRequest("api/v1/sm/workorders", Method.GET);
            request.AddUrlSegment("id", id);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<SageWorkOrder>(request);
            var result = response.Data;
            return result;
        }

        public IEnumerable<SageWorkOrder> Workorders(Properties properties)
        {
            var request = new RestRequest("api/v1/sm/workorders", Method.POST);
            request.AddObject(properties);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageWorkOrder>>(request);
            var result = response.Data;
            return result;
        }

        public SageAssignment Assignments(string id)
        {
            var request = new RestRequest("api/v1/sm/assignments", Method.GET);
            request.AddUrlSegment("id", id);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<SageAssignment>(request);
            var result = response.Data;
            return result;
        }

        private string GetAuthToken()
        {
            var username = System.Configuration.ConfigurationManager.AppSettings["SageUsername"];
            var password = System.Configuration.ConfigurationManager.AppSettings["SagePassword"];

            var request = new RestRequest("oauth/token", Method.POST);
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("grant_type", "password");
            var response = restClient.Execute(request);
            var json = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
            var result = json.First.First.ToString();
            return result;
        }

        public IEnumerable<SageAssignment> Assignments()
        {
            var request = new RestRequest("api/v1/sm/assignments", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageAssignment>>(request);
            var result = response.Data;
            return result;
        }

        public IEnumerable<SageWorkOrder> Workorders()
        {
            var request = new RestRequest("api/v1/sm/workorders", Method.GET);
            request.AddHeader("Authorization", "bearer " + Token);
            var response = restClient.Execute<List<SageWorkOrder>>(request);
            var result = response.Data;
            return result;
        }
    }
}