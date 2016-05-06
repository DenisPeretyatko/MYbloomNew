namespace BloomService.Web.Utils
{
    using System.Configuration;

    using Newtonsoft.Json.Linq;

    using RestSharp;

    public class SageAuthUtility
    {
        private readonly IRestClient restClient;

        public SageAuthUtility(IRestClient restClient)
        {
            this.restClient = restClient;
        }


        //public string GetAuthToken()
        //{
        //    if (session.Session["oauth_token"] != null)
        //    {
        //        return session.Session["oauth_token"].ToString();
        //    }

        //    var username = ConfigurationManager.AppSettings["SageUsername"];
        //    var password = ConfigurationManager.AppSettings["SagePassword"];

        //    var request = new RestRequest("oauth/token", Method.POST);
        //    request.AddParameter("username", username);
        //    request.AddParameter("password", password);
        //    request.AddParameter("grant_type", "password");
        //    var response = restClient.Execute(request);
        //    var json = JObject.Parse(response.Content);
        //    var result = string.Format("{0}", json.First.First);

        //    session.Session["oauth_token"] = result;
        //    return result;
        //}
    }
}