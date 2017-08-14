using System;
using PROJECTBDS.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using PagedList;
using PROJECTBDS.Infrastructure;
using PROJECTBDS.Services.Home;
using PROJECTBDS.ViewModels.Home;

namespace PROJECTBDS.Controllers
{
    [UserViewBag]
    public class HomeController : Controller
    {
        private readonly LandSoftEntities _db = new LandSoftEntities();

        private readonly HomeServices _data = new HomeServices();
        private const int PostsPerPage = 10;

        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
           
            var model = new HomeViewModel
            {
                DuAnNoiBat = _data.GetDuAnNoiBat(),
                BatDongSan = _data.GetBatDongSan().OrderByDescending(t=>t.Id).ToPagedList(pageNumber, PostsPerPage)
            };
            return View(model);
        }
        public ActionResult SieuThiDiaOc(string slug)
        {
            var id = slug.Split('-');

            if (id.Length <= 0) return HttpNotFound();

            var idDuAn = id[id.Length - 1];
            var post = _data.GetLand(int.Parse(idDuAn));

            if (post == null) return HttpNotFound();

            return View(post);
        }

        public ActionResult DuAn(string slug, string key)
        {
            if (string.IsNullOrEmpty(slug)) return HttpNotFound();

            var id = slug.Split('-');

            if (id.Length <= 0) return HttpNotFound();

            var idDuAn = id[id.Length - 1];

            var idnew = 0;

            if (!int.TryParse(idDuAn, out idnew)) return HttpNotFound();

            var post = _db.tblProject.Include("tblProjectDetail").FirstOrDefault(t => t.Id == idnew);

            if (post == null) return HttpNotFound();


            var duAnKhac =
                _db.tblProject.Include("tblProjectDetail")
                    .Where(t => t.Id != post.Id)
                    .OrderByDescending(t => t.Id)
                    .Take(4).ToList();

            if (string.IsNullOrEmpty(key)) return View(new DuAnViewModel { DuAn = post, DuAnKhac = _data.GetDuAnNoiBat(4, post.Id.ToString()) });

            var idDic = key.Split('-');

            if (idDic.Length > 0)
            {
                var idDa = idDic[idDic.Length - 1];

                var idNew = 0;
                if (int.TryParse(idDa, out idNew))
                {
                    var detail = _db.tblProjectDetail.FirstOrDefault(a => a.DictionaryId == idNew && a.ProjectId == idnew);

                    if (detail != null)
                    {
                        ViewBag.KeyDetails = detail;
                        post.Title = null;
                        post.Content = null;
                    }
                }
            }


            return View(new DuAnViewModel { DuAn = post, DuAnKhac = _data.GetDuAnNoiBat(4, post.Id.ToString()) });
        }
        //http://localhost:56788/tim-kiem-bat-dong-san?
        //s=1111
        //&province=4
        //&district=55
        //&type=1
        //&method=5
        //&area=Di%E1%BB%87n+t%C3%ADch
        //&direction=9
        //&price=0%7C500
        public ActionResult TimKiemBatDongSan(int? page)
        {
            //string s, int? province, int? district,
            //int? type, int? method, int? direction,
            //string price;

            var s = Request["s"]; 
            var province = Request["province"].AsInt(); 
            var district = Request["district"].AsInt(); 
            var type = Request["type"].AsInt(); 
            var method = Request["method"].AsInt(); 
            var direction = Request["direction"].AsInt(); 
            var price = Request["price"];

            var priceS = price.Split('|');
            var priceFrom = 0;
            var priceTo = 0;
            var flag = false || priceS.Length > 1 && 
                       int.TryParse(priceS[0], out priceFrom) &&
                       int.TryParse(priceS[1], out priceTo);

            IQueryable<tblLand> results = 
                _db.tblLand.Where(t=>t.Title.ToLower().Contains(s.ToLower()) || 
                                    t.ProvinceId == province ||
                                    t.DistrictId == district ||
                                    t.CategoryId == method ||
                                    t.DistrictId == direction ||
                                    t.TypeId == type ||
                                    (t.Price <= priceFrom && t.Price >= priceTo)
                                    );

            var pageNumber = page ?? 1;
           
            return View(results.OrderByDescending(t=>t.Id).ToPagedList(pageNumber, PostsPerPage));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// sự kiện
        /// </summary>
        /// <returns></returns>
        public ActionResult _Partial_Event()
        {
            return PartialView("_Partial_Event");
        }

        /// <summary>
        /// nhà đất
        /// </summary>
        /// <returns></returns>
        public ActionResult _Partial_Land()
        {
            return PartialView("_Partial_Land");
        }

        /// <summary>
        /// đối tác
        /// </summary>
        /// <returns></returns>
        public ActionResult _Partial_Partner()
        {
            return PartialView("_Partial_Partner");
        }

        /// <summary>
        /// dự án
        /// </summary>
        /// <returns></returns>
        public ActionResult _Partial_Project()
        {
            return PartialView("_Partial_Project");
        }

        /// <summary>
        /// danh sách quận huyện
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DistrictList(int? id)
        {
            return Json(new SelectList(_db.tblDistrict.Where(p => p.ProvinceId == id).ToList(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
    }
}