using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.IO;
using System.Reflection;
using WebCLI.Core.Client;
using System.Web.Http;

[assembly: OwinStartup(typeof(SampleHost.Startup))]

namespace SampleHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            foreach (var resource in new ClientResourceManager().Resources)
            {
                using (var resourceFile = Assembly.Load("WebCLI.Core").GetManifestResourceStream(resource.Key))
                {
                    using (var file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + resource.Value, FileMode.Create, FileAccess.Write))
                    {
                        resourceFile.CopyTo(file);
                    }
                }
            }

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}
