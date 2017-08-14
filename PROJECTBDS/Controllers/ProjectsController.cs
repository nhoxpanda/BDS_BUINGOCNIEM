using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECTBDS.Models;
using PagedList;
using PagedList.Mvc;
using PROJECTBDS.Services.Home;

namespace PROJECTBDS.Controllers
{
    public class ProjectsController : Controller
    {
        private LandSoftEntities db = new LandSoftEntities();
        private HomeServices data = new HomeServices();

        public ActionResult Index(int? page)
        {
            //int pageN = page ?? 1;
            //var model = db.tblProject.ToList();
            //return View(model.ToPagedList(pageN, 30));
            var model = data.GetDuAnNoiBat();
            return View();

        }

        public ActionResult Detail(int? id)
        {
            var model = db.tblProject.Find(id);
            return View(model);
        }
    }
}