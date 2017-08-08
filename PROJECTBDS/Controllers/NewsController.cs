using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECTBDS.Models;
using PagedList;
using PagedList.Mvc;

namespace PROJECTBDS.Controllers
{
    public class NewsController : Controller
    {

        LandSoftEntities db = new LandSoftEntities();
        public ActionResult Index(int? page)
        {
            int pageN = page ?? 1;
            var model = db.tblNews.Where(n => n.tblDictionary.CategoryId==6).ToList();
            return View(model.ToPagedList(pageN, 30));
        }

        public ActionResult Detail(int? id)
        {
            var model = db.tblNews.Find(id);
            return View(model);
        }
    }
}