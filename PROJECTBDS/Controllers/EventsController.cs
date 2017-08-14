using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECTBDS.Models;
using PROJECTBDS.Services.News;
using PagedList;
using PagedList.Mvc;
namespace PROJECTBDS.Controllers
{
    public class EventsController : Controller
    {
        private LandSoftEntities _db = new LandSoftEntities();
        private EventServices _data = new EventServices();
        public ActionResult Index(int? page)
        {
            int pageN = page ?? 1;
            var model = _data.GetEventList().ToList();
            return View(model.ToPagedList(pageN, 30));
        }

        public ActionResult Detail(int? id)
        {
            var model = _data.GetEventDetail(id).FirstOrDefault();
            return View(model);
        }

        public JsonResult DistrictId(int id)
        {
            var model = _db.tblDistrict.Where(p => p.ProvinceId == id).ToList();
            return Json(new SelectList(model, "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string s, int? timetype, DateTime? day, int? province, int? page)
        {
            int pageN = page ?? 1;
            var model = _data.SearchEvent(s, timetype ?? 0, day ?? DateTime.Now, province ?? 0).ToList();
            ViewBag.Keyword = s;
            ViewBag.TimeType = timetype;
            ViewBag.Day = day;
            ViewBag.Province = province;
            return View(model.ToPagedList(pageN, 30));
        }
    }
}