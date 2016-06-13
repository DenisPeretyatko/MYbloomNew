using BloomService.Web.Infrastructure.Services.Interfaces;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace BloomService.Web.Infrastructure.Services
{
    public class HttpContextProvider : IHttpContextProvider
    {
        public HttpContext Current
        {
            get { return HttpContext.Current; }
        }

        public HttpRequest GetHttpRequest()
        {
            return Current.Request;
        }

        public HttpResponse GetHttpResponse()
        {
            return Current.Response;
        }

        public IPrincipal GetUser()
        {
            return Thread.CurrentPrincipal;
        }
    }
}