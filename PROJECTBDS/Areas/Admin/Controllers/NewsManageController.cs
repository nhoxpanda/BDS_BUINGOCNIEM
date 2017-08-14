using PROJECTBDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsManageController : Controller
    {
        // GET: Admin/NewsManage
        private LandSoftEntities _db = new LandSoftEntities();
        public ActionResult Index(int? page, string query, int? CategoryId)
        {
            ViewBag.query = query;
            ViewBag.CategoryId = CategoryId;
            ViewBag.Category = _db.tblDictionary.Where(n => n.CategoryId == 6).ToList();
            int pageN = page ?? 1;
            int pageS = 30;
            int CateID = CategoryId ?? 0;
            var model = new List<tblNews>();
            if (query == null)
            {
                if (CateID == 0)
                {
                    model = _db.tblNews.Where(n => n.tblDictionary.CategoryId==6).OrderByDescending(p => p.CreateDate).ToList();
                }
                else
                {
                    model = _db.tblNews.Where(n => n.CateId == CateID).OrderByDescending(p => p.CreateDate).ToList();
                }
            }
            else
            {
                if (CateID == 0)
                {
                    model = _db.tblNews.Where(n => (n.Title.Equals(query) || n.MetaTitle.Equals(query) || n.MetaDesc.Equals(query)) && n.tblDictionary.CategoryId == 6).OrderByDescending(p => p.CreateDate).ToList();
                }
                else
                {
                    model = _db.tblNews.Where(n => (n.Title.Equals(query) || n.MetaTitle.Equals(query) || n.MetaDesc.Equals(query)) && n.CateId == CateID).OrderByDescending(p => p.CreateDate).ToList();
                }
            }

            return View(model.ToPagedList(pageN,pageS));//
        }
         [ValidateInput(false)]
        public ActionResult Create(tblNews model)
        {
            if (Request["btnSave"] != null)
            {
                model.CreateDate = DateTime.Now;
                _db.tblNews.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CateId = new SelectList(_db.tblDictionary.Where(p => p.CategoryId == 6).ToList(), "Id", "Title", model.CateId);
            return View();
        }
        public ActionResult Update(int id)
        {
            var model = _db.tblNews.Find(id);
            ViewBag.CateId = new SelectList(_db.tblDictionary.Where(p => p.CategoryId == 6).ToList(), "Id", "Title", model.CateId);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(tblNews model)
        {
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            ViewBag.CateId = new SelectList(_db.tblDictionary.Where(p => p.CategoryId == 6).ToList(), "Id", "Title", model.CateId);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var model = _db.tblNews.FirstOrDefault(p => p.Id == id);
                _db.tblNews.Remove(model);
                _db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}