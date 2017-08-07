using PROJECTBDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJECTBDS.Controllers
{
    public class HomeController : Controller
    {
        private Web_NiemBDSEntities _db = new Web_NiemBDSEntities();
        public ActionResult Index()
        {
            return View();
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