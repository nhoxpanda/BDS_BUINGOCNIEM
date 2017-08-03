using PROJECTBDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjectManageController : Controller
    {
        // GET: Admin/ProjectManage
        #region Quản lý dự án
            private WEBSITEBDSEntities db = new WEBSITEBDSEntities();
            public ActionResult Index(int? page, string query)
            {
                int pageN = page ?? 1;
                ViewBag.query = query;
                var model = new List<tblProject>();
                if (!string.IsNullOrEmpty(query))
                {
                    model = db.tblProject.Where(q => q.Title.Contains(query) || q.MetaTitle.Contains(query)).OrderByDescending(n => n.CreateDate).ToList();
                }
                else
                {
                    model = db.tblProject.OrderByDescending(n => n.CreateDate).ToList();
                }
                return View(model.ToPagedList(pageN, 30));
            }
            public ActionResult CreateProject(tblProject model, IEnumerable<HttpPostedFileBase> ListImage)
            {
                if (Request["btnSave"] != null)
                {
                    model.CreateDate = DateTime.Now;
                    db.tblProject.Add(model);
                    db.SaveChanges();
                    int idPro = model.Id;
                    if (ListImage != null)
                    {
                        foreach (var item in ListImage)
                        {
                            String newName = item.FileName.Insert(item.FileName.LastIndexOf('.'), String.Format("{0:_ddMMyyyy}", DateTime.Now));
                            String path = Server.MapPath("~/Images/News/" + newName);
                            item.SaveAs(path);
                            tblImage a = new tblImage();
                            a.Image = newName;
                            a.URL = path;
                            a.ProjectId = idPro;
                            a.DictionaryId = 47;
                            db.tblImage.Add(a);

                        }
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View();
            }
            public ActionResult EditProject(int id)
            {
                var model = db.tblProject.Find(id);
                ViewBag.LsImage = db.tblImage.Where(n => n.ProjectId == id && n.DictionaryId == 47).ToList();
                return View(model);
            }
            [HttpPost]
            public ActionResult EditProject(tblProject model, IEnumerable<HttpPostedFileBase> ListImage)
            {

                if (ListImage != null)
                {
                    foreach (var item in ListImage)
                    {
                        String newName = item.FileName.Insert(item.FileName.LastIndexOf('.'), String.Format("{0:_ddMMyyyy}", DateTime.Now));
                        String path = Server.MapPath("~/Images/News/" + newName);
                        item.SaveAs(path);
                        tblImage a = new tblImage();
                        a.Image = newName;
                        a.URL = path;
                        a.ProjectId = model.Id;
                        a.DictionaryId = 47;
                        db.tblImage.Add(a);

                    }
                }
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            public ActionResult DeleteProject(int id)
            {
                try
                {
                    var model = db.tblProject.Find(id);
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
                    db.tblProjectDetail.Remove(model);
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