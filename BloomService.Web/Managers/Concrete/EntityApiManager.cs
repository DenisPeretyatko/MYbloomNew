namespace BloomService.Web.Managers.Concrete
{
    using System;
    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Utils;

    using RestSharp;
    using RestSharp.Deserializers;
    public class EntityApiManager<TEntity> : IEntityApiManager<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IRestClient restClient;

        private readonly IToken token;

        public EntityApiManager(IRestClient restClient, IToken token)
        {
            this.restClient = restClient;
            this.token = token;
        }

        public string DeleteEndPoint { get; set; }

        public string EditEndPoint { get; set; }

        public string GetEndPoint { get; set; }

        public string CreateEndPoint { get; set; }

        public virtual SageResponse<TEntity> Add(TEntity entity)
        {
            var request = new RestRequest(CreateEndPoint, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(entity);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        public virtual SageResponse<TEntity> Delete(string id)
        {
            var request = new RestRequest(DeleteEndPoint, Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddParameter("id", id);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        public virtual SageResponse<TEntity> Edit(TEntity entity)
        {
            var request = new RestRequest(EditEndPoint, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(entity);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var result = response.Data;
            return result;
        }

        public virtual SageResponse<TEntity> Get()
        {
            var request = new RestRequest(GetEndPoint, Method.GET) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        public virtual SageResponse<TEntity> Get(string id)
        {
            var request = new RestRequest(GetEndPoint, Method.GET) { RequestFormat = DataFormat.Json };
            request.AddUrlSegment("id", id);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var result = response.Data;
            return result;
        }
    }
}