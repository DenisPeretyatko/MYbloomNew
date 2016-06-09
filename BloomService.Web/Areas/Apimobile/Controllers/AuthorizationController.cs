using System.Web.Http;
using System.Net;
using System.Configuration;
using System.Text;
using System.Runtime.Serialization.Json;

namespace BloomService.Web.Areas.Apimobile.Controllers
{
    public class AuthorizationController : ApiController
    {
        public IHttpActionResult Get(string name, string password)
        {
            var token = GetToken(name, password);
            if (token == null)
                return BadRequest();
            return Json(token);
            //if (name == "kris" && password=="111")
            //    return "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1bmlxdWVfbmFtZSI6ImtyaXMiLCJmYW1pbHlfbmFtZSI6InNhZ2VERVYhISIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6MTI0NDIiLCJhdWQiOiI0MTRlMTkyN2EzODg0ZjY4YWJjNzlmNzI4MzgzN2ZkMSIsImV4cCI6MTQ2MTA3MDA4MywibmJmIjoxNDYwOTgzNjgzfQ.RUpXvqnam1_0HSl6SUrhFSakq2187EA7g7ASZeKKyxU";
            //return "Login failed";
        }


        private LoginResponse GetToken(string mail, string password)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "username=" + mail;
            postData += ("&password=" + password);
            postData += ("&grant_type=" + "password");
            byte[] data = encoding.GetBytes(postData);

            var url = ConfigurationManager.AppSettings["url"] + "apimobile/Token";
            var request = HttpWebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.Authorization, "usernamepassword");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            var newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var dataStream = response.GetResponseStream();

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(LoginResponse));
                var model = (LoginResponse)ser.ReadObject(dataStream);
                return model;
            }
            catch
            {
                return null;
            }
        }

        public class LoginResponse
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
        }
    }
}
