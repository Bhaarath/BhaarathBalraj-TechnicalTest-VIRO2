using System.Web.Http;
using WebActivatorEx;
using SalesDataAPI;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace SalesDataAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "SalesDataAPI");
                })
                .EnableSwaggerUi();

        }
    }
}
