using BloomService.Web.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using BloomService.Web;


[assembly: OwinStartup(typeof(OwinConfig))]

namespace BloomService.Web
{
    public class OwinConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            OAuth(app);
        }

        public void OAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            
            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14)
            });
        }
    }
}