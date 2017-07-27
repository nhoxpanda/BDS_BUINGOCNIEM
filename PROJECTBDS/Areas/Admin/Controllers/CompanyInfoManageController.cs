using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECTBDS.Models;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    [Authorize]
    public class CompanyInfoManageController : Controller
    {
        // GET: Admin/CompanyInfoManage
        private WEBSITEBDSEntities _db = new WEBSITEBDSEntities();
        // dictionary = 23
        public ActionResult Index()
        {
            return View(_db.tblNews.Where(p => p.CateId == 23).ToList());
        }

        // thêm mặc định cateid = 23
    }
}