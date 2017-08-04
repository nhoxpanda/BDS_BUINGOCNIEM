using PROJECTBDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        private Web_NiemBDSEntities _db = new Web_NiemBDSEntities();
        public ActionResult Index()
        {
            return View();
        }
    }
}