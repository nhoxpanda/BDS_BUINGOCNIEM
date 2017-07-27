using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    [Authorize]
    public class LandManageController : Controller
    {
        // GET: Admin/LandManage
        public ActionResult Index()
        {
            return View();
        }
    }
}