using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BC_Exercise.Core.Infrastructure;
using BC_Exercise.Core.Interfaces;
using BC_Exercise.Core.Services;
using BC_Exercise.Web.ApiControllers;
using Raven.Client;
using Raven.Client.Embedded;

namespace BC_Exercise.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static IDocumentStore DocumentStore { get; private set; }

        protected void Application_Start()
        {
            SetupDependencyResolver();

            //AreaRegistration.RegisterAllAreas();
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

        private static void SetupDependencyResolver()
        {
            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
                .InstancePerRequest()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
            /*builder.RegisterControllers(Assembly.GetExecutingAssembly())
                .InstancePerApiRequest()
                .InstancePerHttpRequest()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();*/

            builder.Register(c => new ExternalService()).As<IExternalService>()
                .InstancePerRequest()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.Register(c => new UserService(c.Resolve<IExternalService>())).As<IUserService>()
                .InstancePerRequest()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<UserApiController>().InstancePerRequest().PropertiesAutowired();
            //builder.RegisterType<UserService>().InstancePerRequest().PropertiesAutowired();
            //builder.RegisterType<ExternalService>().InstancePerRequest().PropertiesAutowired();

            // Build the container.
            var container = builder.Build();

            // Create the depenedency resolver.
            var apiResolver = new AutofacWebApiDependencyResolver(container);

            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = apiResolver;

            var mvcResolver = new Autofac.Integration.Mvc.AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
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
