using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJECTBDS.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
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