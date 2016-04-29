namespace BloomService.Web.Managers.Concrete.EntityManagers
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Web.Managers.Abstract.EntityManagers;
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
            var request = new RestRequest(endPoint, Method.GET);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<List<TEntity>>(request);
            var results = response.Data;
            return results;
        }

        public virtual TEntity Get(string endPoint, string id)
        {
            var request = new RestRequest(endPoint, Method.GET);
            request.AddUrlSegment("id", id);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<dynamic>(request);
            var result = response.Data;
            return result;
        }
    }
}