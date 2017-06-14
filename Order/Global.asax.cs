using Autofac;
using Autofac.Integration.WebApi;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Order
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

            ConfigureEndpoint();
        }

        private void ConfigureEndpoint()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            var endpointConfiguration = new EndpointConfiguration("WebApi");
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UseContainer<AutofacBuilder>(
                customizations =>
                {
                    customizations.ExistingLifetimeScope(container);
                });

            var endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

            var updater = new ContainerBuilder();
            updater.RegisterInstance(endpoint);
            updater.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var updated = updater.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(updated);
        }
    }
}
