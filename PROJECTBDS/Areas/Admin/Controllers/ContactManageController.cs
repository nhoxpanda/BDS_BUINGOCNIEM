using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECTBDS.Models;
using PagedList;
using PagedList.Mvc;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    public class ContactManageController : Controller
    {
        // GET: Admin/ContactManage

        private readonly LandSoftEntities _db = new LandSoftEntities();
        public ActionResult Index(int? page=1)
        {
            int pageN = page ?? 1;
            var model = _db.tblContact.Where(p => p.Title != null).OrderByDescending(p => p.Date);
            return View(model.ToPagedList(pageN, 10));
        }

        public ActionResult Subscribe(int? page = 1)
        {
            int pageN = page ?? 1;
            var model = _db.tblContact.Where(p => p.Title == null).OrderByDescending(p => p.Date);
            return View(model.ToPagedList(pageN, 10));
        }
    }
}