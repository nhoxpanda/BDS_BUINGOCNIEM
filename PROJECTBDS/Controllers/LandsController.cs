using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECTBDS.Models;
using PROJECTBDS.Infrastructure;
using PROJECTBDS.ViewModels;
using PROJECTBDS.Services.Home;
using PagedList;
using PagedList.Mvc;

namespace PROJECTBDS.Controllers
{
    public class LandsController : Controller
    {
        private LandSoftEntities _db = new LandSoftEntities();
        private HomeServices _data = new HomeServices();

        public ActionResult Index(int? page)
        {
            int pageN = page ?? 1;
            var model = _data.GetBatDongSan();
            return View(model.ToPagedList(pageN, 30));
        }
        
    }
}