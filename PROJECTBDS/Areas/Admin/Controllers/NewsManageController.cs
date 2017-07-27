using PROJECTBDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    [Authorize]
    public class NewsManageController : Controller
    {
        // GET: Admin/NewsManage
        private WEBSITEBDSEntities _db = new WEBSITEBDSEntities();
        public ActionResult Index()
        {
            return View(_db.tblNews.OrderByDescending(p => p.CreateDate).ToList());
        }

        public ActionResult Create(tblNews model)
        {
            if (Request["btnSave"] != null)
            {
                model.CreateDate = DateTime.Now;
                _db.tblNews.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CateId = _db.tblDictionary.Where(p => p.CategoryId == 6).ToList();
            return View();
        }
    }
}