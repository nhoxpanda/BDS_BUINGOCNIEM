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
        private Web_NiemBDSEntities _db = new Web_NiemBDSEntities();
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