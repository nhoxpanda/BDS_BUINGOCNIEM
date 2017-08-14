using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECTBDS.Models;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    public class ConfigManageController : Controller
    {
        // GET: Admin/ConfigManage
        private LandSoftEntities _db = new LandSoftEntities();
        public ActionResult Index()
        {
            var item = _db.tblConfig.Find(1);
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(tblConfig model, HttpPostedFileBase Logo)
        {
            // Logo
            if (Logo != null)
            {
                String newName = Logo.FileName.Insert(Logo.FileName.LastIndexOf('.'), String.Format("{0:_ddMMyyyy}", DateTime.Now));
                String path = Server.MapPath("~/Images/" + newName);
                Logo.SaveAs(path);
                model.Logo = newName;
            }
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return View(model);
        }
    }
}