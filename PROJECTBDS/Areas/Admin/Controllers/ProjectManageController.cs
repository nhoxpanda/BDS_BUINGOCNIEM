using PROJECTBDS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProjectManageController : Controller
    {
        // GET: Admin/ProjectManage
        #region Quản lý dự án
        private LandSoftEntities db = new LandSoftEntities();

        private const int RowsPerPage = 15;
        public ActionResult Index(int? page, string query)
        {
            int pageN = page ?? 1;
            ViewBag.query = query;
            IQueryable<tblProject> model;
            if (!string.IsNullOrEmpty(query))
            {
                model = db.tblProject.Where(q => q.Title.Contains(query) || q.MetaTitle.Contains(query)).OrderByDescending(n => n.CreateDate);
            }
            else
            {
                model = db.tblProject.OrderByDescending(n => n.Id);
            }
            return View(model.ToPagedList(pageN, RowsPerPage));
        }
        [ValidateInput(false)]
        public ActionResult CreateProject(tblProject model, IEnumerable<HttpPostedFileBase> ListImage)
        {
            if (Request["btnSave"] != null)
            {
                model.CreateDate = DateTime.Now;
                db.tblProject.Add(model);
                db.SaveChanges();
                var idPro = model.Id;

                if (ListImage == null) return RedirectToAction("Index");

                foreach (var item in ListImage)
                {
                    if (item == null) continue;

                    var newName = item.FileName.Insert(item.FileName.LastIndexOf('.'), $"{DateTime.Now:_ddMMyyyy_hhss}");
                    var path = Server.MapPath("~/Uploads/News/" + newName);
                    item.SaveAs(path);
                    var image = new tblImage
                    {
                        Image = newName,
                        URL = "/Uploads/News/" + newName,
                        ProjectId = idPro,
                        DictionaryId = 47
                    };
                    db.tblImage.Add(image);

                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            // loại dự án
            ViewBag.TypeId = new SelectList(db.tblDictionary.Where(p=>p.CategoryId == 14), "Id", "Title");
            ViewBag.ProvinceId = new SelectList(db.tblProvince, "Id", "Name", model.ProvinceId);
            //chọn quận huyện 
            ViewBag.DistrictId = new SelectList(db.tblDistrict, "Id", "Name", model.DistrictId);
            return View(model);
        }
        public ActionResult EditProject(int id)
        {
            var model = db.tblProject.Find(id);
            if (model == null) return HttpNotFound();

            ViewBag.LsImage = db.tblImage.Where(n => n.ProjectId == id && n.DictionaryId == 47).ToList();
            ViewBag.ProvinceId = new SelectList(db.tblProvince, "Id", "Name", model.ProvinceId);
            ViewBag.DistrictId = new SelectList(db.tblDistrict, "Id", "Name", model.DistrictId);
            ViewBag.TypeId = new SelectList(db.tblDictionary.Where(p => p.CategoryId == 14), "Id", "Title", model.TypeId);

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProject(tblProject model, List<HttpPostedFileBase> listImage)
        {
            
            foreach (var item in listImage)
            {
                if (item == null) continue;

                var newName = item.FileName.Insert(item.FileName.LastIndexOf('.'),$"{DateTime.Now:_ddMMyyyy}");

                var path = Server.MapPath("/Uploads/News/" + newName);

                item.SaveAs(path);

                var image = new tblImage
                {
                    Image = newName,
                    URL = "/Uploads/News/" + newName,
                    ProjectId = model.Id,
                    DictionaryId = 47
                };

                db.tblImage.Add(image);
            }
            
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void DeleteFile(string file, string folder)
        {
            var location = System.Web.HttpContext.Current.Server.MapPath(folder);

            var fileName = file;

            if (location == null) return;

            var path = Path.Combine(location, fileName);
            var fileOrg = new FileInfo(path);
            if (fileOrg.Exists)
            {
                fileOrg.Delete();
            }
        }
        public void DeleteImage(int projectId)
        {
            try
            {
                var model = db.tblImage.Where(p => p.ProjectId == projectId);
                foreach (var image in model)
                {
                    db.tblImage.Remove(image);
                    DeleteFile(image.Image, "~/Uploads/News/");
                }
                db.SaveChanges();
            }
            catch
            {
                // ignored
            }
        }
        public ActionResult DeleteProject(int id)
        {
            try
            {
                DeleteImage(id);
                var model = db.tblProject.Find(id);
                if (model == null) return Json(1, JsonRequestBehavior.AllowGet);
                db.tblProject.Remove(model);
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Chi tiết dự án
        public ActionResult DetailProject(int id)
        {
            var model = db.tblProjectDetail.Where(n => n.ProjectId == id).ToList();
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult CreateDetailProject(tblProjectDetail model)
        {
            if (Request["btnSave"] != null)
            {
                db.tblProjectDetail.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            loadDropdow(model);
            return View();
        }
        public ActionResult EditDetailProject(int id)
        {
            var model = db.tblProjectDetail.Find(id);
            loadDropdow(model);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditDetailProject(tblProjectDetail model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteDetailProject(int id)
        {
            try
            {
                var model = db.tblProjectDetail.Find(id);

                if (model != null) db.tblProjectDetail.Remove(model);

                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public void loadDropdow(tblProjectDetail model)
        {
            ViewBag.ProjectId = new SelectList(db.tblProject, "Id", "Title", model.ProjectId);
            //danh sách hướng
            ViewBag.DictionaryId = new SelectList(db.tblDictionary.Where(n => n.CategoryId == 10).ToList(), "Id", "Title", model.DictionaryId);
        }
        #endregion

    }
}