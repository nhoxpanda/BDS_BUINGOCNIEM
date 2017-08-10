using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECTBDS.Models;
using PROJECTBDS.ViewModels;

namespace PROJECTBDS
{
    //public class UserSettingsServiceModelBinder : IModelBinder
    //{
    //    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //    {
    //        var username = string.Empty;

    //        if (controllerContext.HttpContext.Session != null)
    //        {
    //            username = (string)controllerContext.HttpContext.Session["Username"];
    //        }

    //        return username;
    //    }
    //}
    //public class AreaAuthorizeAttribute : AuthorizeAttribute
    //{
    //    private readonly string _area;

    //    public AreaAuthorizeAttribute(string area)
    //    {
    //        this._area = area;
    //    }

    //    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    //    {
    //        var loginUrl = "";

    //        switch (_area)
    //        {
    //            case "Admin":
    //                loginUrl = "~/Admin/";
    //                break;
    //            case "Members":
    //                loginUrl = "~/User/Login";
    //                break;
    //        }

    //        filterContext.Result = new RedirectResult(loginUrl + "?returnUrl=" + filterContext.HttpContext.Request.Url.PathAndQuery);
    //    }
    //}

    public class Auth
    {
        private static readonly LandSoftEntities Db  = new LandSoftEntities();

        private const string UserKey = "LandSoft.Auth.UserKey";

        public static UserViewLogin User
        {
            
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated) return null;

                var user = HttpContext.Current.Items[UserKey] as UserViewLogin;

                if (user != null) return user;

                var userCus = Db.tblCustomer.FirstOrDefault(u => u.Username == HttpContext.Current.User.Identity.Name);

                if (userCus == null) return null;

                user = new UserViewLogin
                {
                    Username = userCus.Username,
                    UserId = userCus.Id,
                    Email = userCus.Email
                };

                HttpContext.Current.Items[UserKey] = user;

                return user;
            }
        }
    }
}