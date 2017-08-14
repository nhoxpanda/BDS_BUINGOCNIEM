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
               name: "LienHe",
               url: "lien-he",
               defaults: new { controller = "Home", action = "Contact" }
            );

            routes.MapRoute(
               name: "TimKiemBatDongSan",
               url: "tim-kiem-bat-dong-san/",
               defaults: new { controller = "Home", action = "TimKiemBatDongSan" }
           );

            routes.MapRoute(
               name: "TimKiemSuKien",
               url: "tim-kiem-su-kien",
               defaults: new { controller = "Events", action = "Search" }
           );

            routes.MapRoute(
               name: "DanhSachSuKien",
               url: "su-kien",
               defaults: new { controller = "Events", action = "Index" }
            );

            routes.MapRoute(
               name: "ChiTietSuKien",
               url: "su-kien/{slug}-sk{id}",
               defaults: new { controller = "Events", action = "Detail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "TimKiemTinTuc",
               url: "tim-kiem-tin-tuc",
               defaults: new { controller = "News", action = "Search" }
            );

            routes.MapRoute(
               name: "DanhSachTinTuc",
               url: "tin-tuc",
               defaults: new { controller = "News", action = "Index" }
            );

            routes.MapRoute(
               name: "ChiTietTinTuc",
               url: "tin-tuc/{slug}-tt{id}",
               defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "DanhMucTinTuc",
               url: "tin-tuc/danh-muc-tin-tuc/{slug}-ds{id}",
               defaults: new { controller = "News", action = "Cate", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "GioiThieu",
               url: "gioi-thieu/{slug}-{id}",
               defaults: new { controller = "Abouts", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "DangKy",
               url: "dang-ky/",
               defaults: new { controller = "User", action = "Register", }
           );

            routes.MapRoute(
               name: "DangNhap",
               url: "dang-nhap/",
               defaults: new { controller = "User", action = "Login" }
           );
            routes.MapRoute(
               name: "DuAn",
               url: "du-an/{slug}/{key}",
               defaults: new { controller = "Home", action = "DuAn", key = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "SieuThiDiaOc",
               url: "sieu-thi-dia-oc/{slug}",
               defaults: new { controller = "Home", action = "SieuThiDiaOc" }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
