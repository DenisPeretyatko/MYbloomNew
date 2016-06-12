namespace BloomService.Web.Managers.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Utils;
    using Domain.Entities.Concrete.Auxiliary;
    using RestSharp;
    using System.Collections.Generic;
    public class CustomerApiManager : EntityApiManager<SageCustomer>, ICustomerApiManager
    {
        private readonly IRestClient restClient;

        private readonly IToken token;

        public CustomerApiManager(IRestClient restClient, IToken token)
            : base(restClient, token)
        {
            this.restClient = restClient;
            this.token = token;

            GetEndPoint = EndPoints.GetCustomer;
        }

        public override SageResponse<SageCustomer> Get()
        {
            var request = new RestRequest(GetEndPoint, Method.GET) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<List<SageCustomer>>(request);

            if(response.Data != null)
            {
                return new SageResponse<SageCustomer>()
                {
                    IsSucceed = true,
                    Entities = response.Data
                };
            }
            return new SageResponse<SageCustomer>()
            {
                IsSucceed = false,
                ErrorMassage = "Data is null"
            };
        }
    }
}