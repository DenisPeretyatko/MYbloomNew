namespace BloomService.Web.Managers.Concrete.EntityManagers
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Managers.Abstract.EntityManagers;
    using BloomService.Web.Utils;

    using RestSharp;

    public class EditableEntityApiManager<TEntity> : EntityApiManager<TEntity>, IEditableEntityApiManager<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IRestClient restClient;

        private readonly IToken token;

        public EditableEntityApiManager(IRestClient restClient, IToken token)
            : base(restClient, token)
        {
            this.restClient = restClient;
            this.token = token;
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