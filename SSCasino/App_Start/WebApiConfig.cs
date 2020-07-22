//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// WebApiConfig.cs
//      Configuration of any Web API related configuration, including Web-API-specific routes, Web API services, and 
//      other Web API settings.
//
// Developer Notes
//      Web API supports ONLY code based configuration. It cannot be configured in web.config file. We can configure 
//      Web API to customize the behaviour of Web API hosting infrastructure and components such as routes, formatters, 
//      filters, DependencyResolver, MessageHandlers, ParamterBindingRules, properties, services etc.
//========================================================================================================================

using System.Web.Http;

namespace SSCasino
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
