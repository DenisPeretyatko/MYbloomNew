using System.Web;

namespace BloomService.Web.Utils
{
    public class BloomServiceSession : ISession
    {
        public HttpSessionStateWrapper Session
        {
            get
            {
                return new HttpSessionStateWrapper(HttpContext.Current.Session);
            }
        }
    }
}