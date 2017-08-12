using PROJECTBDS.Models;
using System.Linq;
using System.Web.Mvc;
using PROJECTBDS.Services.Home;
using PROJECTBDS.ViewModels.Home;

namespace PROJECTBDS.Controllers
{
    public class HomeController : Controller
    {
        private readonly LandSoftEntities _db = new LandSoftEntities();

        private readonly HomeServices _data = new HomeServices();

        public ActionResult Index()
        {
            var model = new HomeViewModel
            {
                DuAnNoiBat = _data.GetDuAnNoiBat(),
                BatDongSan = _data.GetBatDongSan()
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

            if (string.IsNullOrEmpty(key)) return View(post);

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

            return View(post);
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