using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using BloomService.Web;

[assembly: OwinStartup(typeof(OwinConfig))]

namespace BloomService.Web
{
    public class OwinConfig
    {
        public static OAuthAuthorizationServerOptions OAuthOptions;

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            OAuth(app);
        }

        public void OAuth(IAppBuilder app)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14)
            };

            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}