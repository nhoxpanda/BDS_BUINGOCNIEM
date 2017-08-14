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
        private LandSoftEntities _db = new LandSoftEntities();
        private HomeServices _data = new HomeServices();

        public ActionResult Index(int? page)
        {
            int pageN = page ?? 1;
            var model = _data.GetDuAnNoiBat();
            ViewBag.LeftColumn = _data.GetDuAnNoiBat(5, "0");
            return View(model.ToPagedList(pageN, 30));
        }
    }
}