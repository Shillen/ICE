using Microsoft.Owin.Security.OAuth;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ICE_Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }

        // Return json
        //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

        //// Ignore self referencing errors
        //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
        //= Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        // Custom validate model attribute to prevent the web api from addings its own error
        // messages to the model state.
        // config.Filters.Add(new ValidateModelAttribute());
    }
}