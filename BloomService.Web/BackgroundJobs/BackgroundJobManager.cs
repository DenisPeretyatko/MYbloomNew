namespace BloomService.Web.BackgroundJobs
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Utils;

    using Ninject;

    using RestSharp;

    public class BackgroundJobManager
    {
        [Inject]
        private IRestClient RestClient { get; set; }

        [Inject]
        private IToken Token { get; set; }

        [Inject]
        private IUnitOfWork UnitOfWork { get; set; }

        //public void DbSync()
        //{
        //    var changes = UnitOfWork.Changes.SearchFor(c => c.Status == StatusType.NotSynchronized);

        //    foreach (var change in changes)
        //    {
        //        var entityType = Type.GetType(change.EntityType);

        //        var endpointBase = ConfigurationManager.AppSettings["EndPointBase"];

        //        var request = new RestRequest(endpointBase + entityType + 's', Method.GET);
        //        request.AddHeader("Authorization", Token.Token);
        //        var response = RestClient.Execute<List<TEntity>>(request);
        //        var results = response.Data;
        //    }
        //}

        //private void SynchronizeStatement(SageChange change)
        //{
        //}
    }
}