namespace BloomService.Web.Managers.Concrete.EntityManagers
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Managers.Abstract.EntityManagers;
    using BloomService.Web.Utils;

    using RestSharp;

    public class AddableEntityApiManager<TEntity> : EntityApiManager<TEntity>, IAddableEntityApiManager<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IRestClient restClient;

        private readonly IToken token;

        public AddableEntityApiManager(IRestClient restClient, IToken token)
            : base(restClient, token)
        {
            this.restClient = restClient;
            this.token = token;
        }

        public virtual IEnumerable<TEntity> Add(string endPoint, PropertyDictionary properties)
        {
            var request = new RestRequest(endPoint, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(properties);
            request.AddHeader("Authorization", token.Token);
            var response = restClient.Execute<List<TEntity>>(request);
            var results = response.Data;
            return results;
        }
    }
}