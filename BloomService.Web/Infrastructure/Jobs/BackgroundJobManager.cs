namespace BloomService.Web.BackgroundJobs
{
    public class BackgroundJobManager
    {
        public BackgroundJobManager()
        {
            
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
    }
}