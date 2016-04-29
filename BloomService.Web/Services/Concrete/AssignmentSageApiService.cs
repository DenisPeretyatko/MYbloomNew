using System.Collections.Generic;

namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Services.Abstract;
    using BloomService.Web.Utils;

    using RestSharp;

    public class AssignmentSageApiService : SageApiService<SageAssignment>, IAssignmentSageApiService
    {
        private IRestClient restClient;

        private ISession session;

        private IUnitOfWork unitOfWork;

        public AssignmentSageApiService(IRestClient restClient, IUnitOfWork unitOfWork /*, ISession session*/)
            : base(restClient, unitOfWork /*, session*/)
        {
            this.restClient = restClient;
            this.unitOfWork = unitOfWork;

            // this.session = session;
            EndPoint = ConfigurationManager.AppSettings["AssignmentEndPoint"];
        }

        public override IEnumerable<SageAssignment> Add(PropertyDictionary properties)
        {
            var request = new RestRequest("api/v1/sm/Addassignments", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(properties);
            //request.AddParameter("properties", request.JsonSerializer.Serialize(properties));
            //request.AddObject(properties);
            request.AddHeader("Authorization", GetAuthToken());

            var response = restClient.Execute<List<SageAssignment>>(request);

            var results = response.Data;

            foreach (var result in results)
            {
                unitOfWork.GetEntities<SageAssignment>().Insert(result);
            }

            return results;
        }
    }
}