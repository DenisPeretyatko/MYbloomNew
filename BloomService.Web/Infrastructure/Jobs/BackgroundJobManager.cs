using BloomService.Web.Utils;
using Ninject;
using RestSharp;

namespace BloomService.Web.BackgroundJobs
{
    public class BackgroundJobManager
    {
        //[Inject]
        private IRestClient _restClient { get; set; }

        //[Inject]
        private IToken _token { get; set; }

        public BackgroundJobManager(IRestClient restClient, IToken token)
        {
            _restClient = restClient;
            _token = token;
        }

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