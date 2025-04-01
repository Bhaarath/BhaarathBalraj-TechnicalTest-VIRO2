using SalesDataInterfaces;
using System.Web.Http;
using SalesDataServices;
using Unity;
using Unity.WebApi;
using System.Configuration;
using System.Web.Hosting;
using Unity.Injection;

namespace SalesDataAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();


            var providerType = ConfigurationManager.AppSettings["SalesDataProviderType"];
            var csvPath = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["SalesDataCsvPath"]);

            // Register Csv provider type
            container.RegisterType<ISalesDataProvider, CsvSalesDataProvider>("Csv", new InjectionConstructor(csvPath));


            //Data Provider can be swapped in future with this approach
            //for example:

            //SharePoint provider
            // container.RegisterType<ISalesDataProvider, JsonSalesDataProvider>("SharePoint");

            //ExternalApi
            // container.RegisterType<ISalesDataProvider, JsonSalesDataProvider>("ExternalApi");

            
            ISalesDataProvider selectedProvider = container.Resolve<ISalesDataProvider>(providerType);

            container.RegisterInstance<ISalesDataProvider>(selectedProvider);
            container.RegisterType<ISalesDataService, SalesDataService>();


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}