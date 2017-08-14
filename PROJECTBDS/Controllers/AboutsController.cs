using PROJECTBDS.Models;
using PROJECTBDS.Services.News;
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
        private AboutServices data = new AboutServices();
        public ActionResult Index(int id)
        {
            var model = data.GetAboutDetail(id).FirstOrDefault();
            ViewBag.AboutList = data.GetAboutList();
            return View(model);
        }
    }
}