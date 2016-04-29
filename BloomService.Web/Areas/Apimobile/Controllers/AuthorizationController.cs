using System.Web.Http;
using AttributeRouting.Web.Mvc;

namespace BloomService.Web.Areas.Apimobile.Controllers
{
    public class AuthorizationController : ApiController
    {
        public string Get(string name, string password)
        {
            if(name == "kris" && password=="111")
                return "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1bmlxdWVfbmFtZSI6ImtyaXMiLCJmYW1pbHlfbmFtZSI6InNhZ2VERVYhISIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6MTI0NDIiLCJhdWQiOiI0MTRlMTkyN2EzODg0ZjY4YWJjNzlmNzI4MzgzN2ZkMSIsImV4cCI6MTQ2MTA3MDA4MywibmJmIjoxNDYwOTgzNjgzfQ.RUpXvqnam1_0HSl6SUrhFSakq2187EA7g7ASZeKKyxU";
            return "Login failed";
        }
    }
}
