using PROJECTBDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace PROJECTBDS.Controllers
{
    public class AboutsController : Controller
    {
        // GET: Abouts
        private LandSoftEntities _db = new LandSoftEntities();
        public ActionResult Index(int? page)
        {
            return View();
        }

        public ActionResult Detail(int? id)
        {
            return View();
        }
    }
}