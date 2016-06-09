namespace BloomService.Web.Managers.Concrete
{
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

            //this.restClient.AddHandler("application/json", new DynamicJsonDeserializer());
            restClient.AddHandler("text/plain", new JsonDeserializer());
            this.token = token;
        }

        protected string EndPoint { get; set; }

        public virtual SageResponse<TEntity> Add(TEntity entity)
        {
            var request = new RestRequest(EndPoint, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(entity);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        public virtual SageResponse<TEntity> Delete(string id)
        {
            var request = new RestRequest(EndPoint, Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddParameter("id", id);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        public virtual SageResponse<TEntity> Edit(TEntity entity)
        {
            var request = new RestRequest(EndPoint, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(entity);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var result = response.Data;
            return result;
        }

        public virtual SageResponse<TEntity> Get()
        {
            var request = new RestRequest(EndPoint, Method.GET) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        public virtual SageResponse<TEntity> Get(string id)
        {
            var request = new RestRequest(EndPoint, Method.GET) { RequestFormat = DataFormat.Json };
            request.AddUrlSegment("id", id);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<SageResponse<TEntity>>(request);
            var result = response.Data;
            return result;
        }
    }
}