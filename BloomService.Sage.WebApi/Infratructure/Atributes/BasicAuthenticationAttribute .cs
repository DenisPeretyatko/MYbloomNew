using System;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Sage.WebApi.Infratructure.Atributes
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            if (!String.IsNullOrEmpty(auth))
            {
                var userName = WebConfigurationManager.AppSettings["SageUsername"];
                var password = WebConfigurationManager.AppSettings["SagePassword"];

                var cred = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                var user = new { Name = cred[0], Pass = cred[1] };
                if (user.Name == userName && user.Pass == password) return;
            }
            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", String.Format("Basic realm=\"{0}\"", "SageUser"));
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}