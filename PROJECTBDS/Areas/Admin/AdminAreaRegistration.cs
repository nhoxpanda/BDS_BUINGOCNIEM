using System.Web.Http;
using System.Web.Mvc;

namespace PROJECTBDS.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute("AreaName_WebApiRoute",
                "Admin/Api/{controller}/{id}",
                new { id = RouteParameter.Optional }
                );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "HomeAdmin", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}