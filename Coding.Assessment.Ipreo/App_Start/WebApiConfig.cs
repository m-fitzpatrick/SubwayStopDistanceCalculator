using System.Web.Http;
using System.Web.Http.Cors;
using Coding.Assessment.Ipreo.App_Start;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Coding.Assessment.Ipreo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.EnableCors(new EnableCorsAttribute("*", "*", "GET,POST"));

            config.Routes.MapHttpRoute(name: "DefaultApi",
                                       routeTemplate: "api/{controller}/{id}",
                                       defaults: new {id = RouteParameter.Optional}
                                      );

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
                                                                                            {
                                                                                                Formatting = Formatting.Indented,
                                                                                                TypeNameHandling = TypeNameHandling.Objects,
                                                                                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                                                                                            };

            StructuremapWebApi.Start();
        }
    }
}
