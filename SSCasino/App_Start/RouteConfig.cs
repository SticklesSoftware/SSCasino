//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// RouteConfig.cs
//      This class handles configuration of any application request routes.
//
// Developer Notes
//      In the ASP.NET Web Forms application, every URL must match with a specific.aspx file abd mapping was done on a 
//      one to one basis. ASP.NET introduced Routing to eliminate needs of mapping each URL with a physical file. Routing 
//      enable us to define URL pattern that maps to the request handler.
//========================================================================================================================

using System.Web.Mvc;
using System.Web.Routing;

namespace SSCasino
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}
