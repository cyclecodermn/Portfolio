using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DvdListService1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();
            var jsonSerializerSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializerSettings.Formatting = Formatting.Indented;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
