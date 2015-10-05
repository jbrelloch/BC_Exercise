using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BC_Exercise.Core.Infrastructure;
using Raven.Client;
using Raven.Client.Embedded;

namespace BC_Exercise.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static IDocumentStore DocumentStore { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeDocumentStore();

            DataDocumentStore.Instance = DocumentStore;

            /*using (var session = documentStore.OpenSession())
            {
                var i = 0;
            }

            var j = 1;*/
        }

        private static void InitializeDocumentStore()
        {
            if (DocumentStore != null) return; // prevent misuse

            DocumentStore = new EmbeddableDocumentStore { DataDirectory = @"C:\RavenDB\Databases" };
            DocumentStore.Initialize();
            /*var convention = new DocumentConvention
            {
                FindTypeTagName = t =>
                {
                    if (typeof(Item).IsAssignableFrom(t)) return "Items";
                    if (typeof(Member).IsAssignableFrom(t)) return "Members";
                    if (typeof(Msg).IsAssignableFrom(t)) return "Msgs";
                    if (typeof(ProgramStage).IsAssignableFrom(t)) return "stages";
                    return DocumentConvention.DefaultTypeTagName(t);
                }
            };

            var parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName("RavenDB");
            parser.Parse();

            DocumentStore store = new DocumentStore
            {
                ApiKey = parser.ConnectionStringOptions.ApiKey,
                Url = parser.ConnectionStringOptions.Url,
                DefaultDatabase = parser.ConnectionStringOptions.DefaultDatabase,
                Conventions = convention
            };

            store.RegisterListener(new ValidationListener());
            store.RegisterListener(new AuditListener());
            DocumentStore = store.Initialize();

            TryCreatingIndexesOrRedirectToErrorPage();*/

            //RavenProfiler.InitializeFor(DocumentStore); //, "identifiers", "pwhash", "BirthDate");
        }
    }
}
