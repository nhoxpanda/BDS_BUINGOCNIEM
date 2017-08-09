using System.Web.Mvc;
using System.Web.Routing;

namespace PROJECTBDS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
               name: "DuAn",
               url: "du-an/{slug}/{key}",
               defaults: new { controller = "Home", action = "DuAn", key = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "SieuThiDiaOc",
               url: "sieu-thi-dia-oc/{slug}",
               defaults: new { controller = "Home", action = "SieuThiDiaOc"}
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
