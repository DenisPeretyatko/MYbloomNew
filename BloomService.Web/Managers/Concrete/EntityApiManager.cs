namespace BloomService.Web.Managers.Concrete
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Utils;

    using RestSharp;

    public class EntityApiManager<TEntity> : IEntityApiManager<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IRestClient restClient;

        private readonly IToken token;

        public EntityApiManager(IRestClient restClient, IToken token)
        {
            this.restClient = restClient;

            // this.restClient.AddHandler("application/json", new DynamicJsonDeserializer());

            this.token = token;
        }

        public virtual IEnumerable<TEntity> Get(string endPoint)
        {
            var request = new RestRequest(endPoint, Method.GET) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<List<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        public virtual TEntity Get(string endPoint, string id)
        {
            var request = new RestRequest(endPoint, Method.GET) { RequestFormat = DataFormat.Json };
            request.AddUrlSegment("id", id);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<dynamic>(request);
            var result = response.Data;
            return result;
        }

        public virtual IEnumerable<TEntity> Delete(string endPoint, string id)
        {
            var request = new RestRequest(endPoint, Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddUrlSegment("id", id);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<List<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        public virtual IEnumerable<TEntity> Add(string endPoint, SagePropertyDictionary properties)
        {
            var request = new RestRequest(endPoint, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(properties);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<List<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        public virtual IEnumerable<TEntity> Edit(string endPoint, SagePropertyDictionary properties)
        {
            var request = new RestRequest(endPoint, Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddObject(properties);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<List<TEntity>>(request);
            var result = response.Data;
            return result;
        }
    }
}