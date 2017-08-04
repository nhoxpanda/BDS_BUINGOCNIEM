using PROJECTBDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJECTBDS.Controllers
{
    public class AboutsController : Controller
    {
        // GET: Abouts
        private WEBSITEBDSEntities _db = new WEBSITEBDSEntities();
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