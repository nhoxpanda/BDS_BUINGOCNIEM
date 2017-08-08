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
    public class LandsController : Controller
    {
        LandSoftEntities db = new LandSoftEntities();
        public ActionResult Index(int? page)
        {
            int pageN = page ?? 1;
            var model = db.tblLand.ToList();
            return View(model.ToPagedList(pageN,30));
        }

        public ActionResult Detail(int? id)
        {
            var model = db.tblLand.Find(id);
            return View(model);
        }
    }
}