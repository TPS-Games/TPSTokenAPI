using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using TPSTokenGranterService.Providers;

[assembly: OwinStartup(typeof(TPSTokenGranterService.Startup))]

namespace TPSTokenGranterService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oauthserverOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                Provider = new SimpleAuthorizationServiceProvider()
            };

            //TPS Alpha Token Generator v1.00a
            app.UseOAuthAuthorizationServer(oauthserverOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
