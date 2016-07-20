using System;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Threading;
using Elmah;
using MailMessage = System.Net.Mail.MailMessage;

namespace BloomService.Web
{
    using Infrastructure.Jobs;
    using FluentScheduler;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ModelBinderConfig.RegisterAllBinders();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();
            JobManager.Initialize(new BloomJobRegistry());
        }
        void ErrorMail_Mailing(object sender, Elmah.ErrorMailEventArgs e)
        {
            e.Mail.Subject = "BloomService error: " + e.Error.Exception.Message;
        }


    }
}