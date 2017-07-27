using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    [Authorize]
    public class EventsManageController : Controller
    {
        // GET: Admin/EventsManage

        // categoryid = 8
        public ActionResult Index()
        {
            return View();
        }
    }
}