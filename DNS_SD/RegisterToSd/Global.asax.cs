using System.Web.Http;

namespace RegisterToSd
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ApplicationConfig.RegisterConfig("development");
            DiscoveryConfig.Register(ApplicationConfig.Configuration, null);
        }
    }
}
