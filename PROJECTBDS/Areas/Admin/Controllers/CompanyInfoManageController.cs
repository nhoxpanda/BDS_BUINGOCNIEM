using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECTBDS.Models;
using PagedList;
using PagedList.Mvc;
namespace PROJECTBDS.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompanyInfoManageController : Controller
    {
        // GET: Admin/CompanyInfoManage
        private LandSoftEntities _db = new LandSoftEntities();
        public ActionResult Index(int? page, string query)
        {
            ViewBag.query = query;
            int pageN = page ?? 1;
            int pageS = 30;
            var model = new List<tblNews>();
            if (query == null)
            {
                    model = _db.tblNews.Where(n => n.CateId==23).OrderByDescending(p => p.CreateDate).ToList();
            }
            else
            {

                    model = _db.tblNews.Where(n => (n.Title.Equals(query) || n.MetaTitle.Equals(query) || n.MetaDesc.Equals(query)) && n.CateId == 23).OrderByDescending(p => p.CreateDate).ToList();
            }

            return View(model.ToPagedList(pageN, pageS));//
        }

        [ValidateInput(false)]
        public ActionResult Create(tblNews model)
        {
            if (Request["btnSave"] != null)
            {
                model.CreateDate = DateTime.Now;
                model.CateId = 23;
                _db.tblNews.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Update(int id)
        {
            var model = _db.tblNews.Find(id);   
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(tblNews model)
        {
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges(); 
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var model = _db.tblNews.Find(id);
                _db.tblNews.Remove(model);
                _db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        // thêm mặc định cateid = 23
    }
}