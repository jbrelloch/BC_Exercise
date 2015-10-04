using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Raven.Client.Embedded;

namespace BC_Exercise.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var documentStore = new EmbeddableDocumentStore { DataDirectory = @"C:\RavenDB\Databases" };
            documentStore.Initialize();

            using (var session = documentStore.OpenSession())
            {
                var i = 0;
            }

            var j = 1;
        }
    }
}
