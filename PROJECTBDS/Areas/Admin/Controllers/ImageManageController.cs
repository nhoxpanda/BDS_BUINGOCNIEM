using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECTBDS.Models;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    public class ImageManageController : Controller
    {
        Web_NiemBDSEntities db = new Web_NiemBDSEntities();
        public ActionResult Index()
        {
            //var model = db.tblImage.Where(n => n.DictionaryId == 44 || n.DictionaryId == 45 || n.DictionaryId == 46).ToList();
            ViewBag.LoaiImage = db.tblDictionary.Where(n => n.Id == 44 || n.Id == 45 || n.Id == 46).ToList();
            return View();
        }
        public PartialViewResult LoadImage(int id)
        {
            List<tblImage> Model = db.tblImage.Where(n => n.DictionaryId == id).ToList();
            return PartialView("_PartialListImage",Model);
        }
        public ActionResult CreateImage(tblImage model)
        {
            if (Request["btnSave"] != null)
            {
          
                db.tblImage.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DictionaryId = new SelectList(db.tblDictionary.Where(n => n.Id == 44 || n.Id == 45 || n.Id == 46).ToList(), "Id", "Title", model.DictionaryId);
            return View();
        }

        public PartialViewResult EditImage(int id)
        {
            var model = db.tblImage.Find(id);
            ViewBag.DictionaryId = new SelectList(db.tblDictionary.Where(n => n.Id == 44 || n.Id == 45 || n.Id == 46).ToList(), "Id", "Title", model.DictionaryId);
            return PartialView("_PartialEditImage", model);
        }
        [HttpPost]
        public ActionResult EditImageS(tblImage model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteImage(int id)
        {
            try
            {
                var model = db.tblImage.FirstOrDefault(p => p.Id == id);
                db.tblImage.Remove(model);
                db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}