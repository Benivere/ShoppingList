using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShoppingList
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{id}",
                defaults: new { controller = "ShoppingEventPage", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
