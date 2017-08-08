using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECTBDS.Models;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryNewsManageController : Controller
    {
        // GET: Admin/CategoryNewsManageController

        private LandSoftEntities _db = new LandSoftEntities();
        public ActionResult Index(int id)
        {
            var model = _db.tblDictionary.Where(p => p.CategoryId == id && p.Delete == false);
            ViewBag.tblCategory = _db.tblCategory.Find(id);
            return View(model);
        }

        public ActionResult Insert(int id)
        {
            ViewBag.ID = id;
            return PartialView("_Partial_Insert");
        }

        public ActionResult Create( tblDictionary model)
        {
            model.Delete = false;
            _db.tblDictionary.Add(model);
            _db.SaveChanges();

            return RedirectToAction("/Index/" + model.CategoryId);
        }

        public ActionResult Edit(int id)
        {
            return PartialView("_Partial_Edit", _db.tblDictionary.Find(id));
        }

        public ActionResult Update(tblDictionary model)
        {
            model.Delete = false;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("/Index/" + model.CategoryId);
        }

        public ActionResult Delete(int id)
        {
            var model = _db.tblDictionary.Find(id);
            model.Delete = true;
            _db.SaveChanges();

            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}