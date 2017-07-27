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
            return View();
        }
    }
}