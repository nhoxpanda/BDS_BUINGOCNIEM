using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using PROJECTBDS.Areas.Admin.Services;
using PROJECTBDS.Models;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    [Authorize]
    public class LandManageController : Controller
    {

        LandSoftEntities db = new LandSoftEntities();

        private const int RowsPerPage = 5;


        // GET: Admin/LandManage
        public ActionResult Index(int? page, string query)
        {
            ViewBag.query = query;
            int pageN = page ?? 1;
            IQueryable<tblLand> model;
            if (!string.IsNullOrEmpty(query))
            {
                model =
                    db.tblLand.Where(q => q.Title.Contains(query) || q.MetaTitle.Contains(query))
                        .OrderByDescending(n => n.CreateDate);
            }
            else
            {
                model = db.tblLand.OrderByDescending(t => t.Id);
            }
            return View(model.ToPagedList(pageN, RowsPerPage));
        }

        [ValidateInput(false)]
        public ActionResult Create(tblLand model, IEnumerable<HttpPostedFileBase> listImage)
        {

            if (Request["btnSave"] != null)
            {
                model.CreateDate = DateTime.Now;
                model.Area = Request["DienTich"];
                db.tblLand.Add(model);
                db.SaveChanges();

                var id = model.Id;

                if (listImage != null)
                {
                    foreach (var item in listImage)
                    {
                        if (item == null) continue;

                        var newName = item.FileName.Insert(item.FileName.LastIndexOf('.'), $"{DateTime.Now:_ddMMyyyy}");

                        var path = Server.MapPath("~/Uploads/News/" + newName);

                        item.SaveAs(path);

                        var image = new tblImage
                        {
                            Image = newName,
                            URL = "/Uploads/News/" + newName,
                            LandId = id,
                            DictionaryId = 47
                        };
                        db.tblImage.Add(image);

                    }

                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Load(model);
            return View();
        }

        public ActionResult Update(int id)
        {
            var model = db.tblLand.Find(id);
            Load(model);
            ViewBag.LsImage = db.tblImage.Where(n => n.LandId == id && n.DictionaryId == 47).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(tblLand model, IEnumerable<HttpPostedFileBase> listImage)
        {
            var dba = new LandSoftEntities();
            var la = dba.tblLand.Find(model.Id);

            if (la == null) return RedirectToAction("Update");
           
            foreach (var item in listImage)
            {
                if (item == null) continue;

                var newName = item.FileName.Insert(item.FileName.LastIndexOf('.'), $"{DateTime.Now:_ddMMyyyy}");

                var path = Server.MapPath("~/Uploads/News/" + newName);

                item.SaveAs(path);

                var a = new tblImage
                {
                    Image = newName,
                    URL = "/Uploads/News/" + newName,
                    LandId = model.Id,
                    DictionaryId = 47
                };
                db.tblImage.Add(a);

            }
            model.Area = string.Empty;
            model.Area = Request["DienTich"];
            model.CustomerId = la.CustomerId;
            model.CreateDate = la.CreateDate;
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
        public void DeleteImage(int landId)
        {
            try
            {
                var model = db.tblImage.Where(p => p.LandId == landId);
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
        public ActionResult Delete(int id)
        {
            try
            {
                DeleteImage(id);

                var model = db.tblLand.FirstOrDefault(p => p.Id == id);

                if (model != null) db.tblLand.Remove(model);

                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public void Load(tblLand model)
        {
            //ds dự án
            ViewBag.ProjectId = new SelectList(db.tblProject, "Id", "Title", model.ProjectId);
            //danh sách hướng
            ViewBag.DirectionId = new SelectList(db.tblDictionary.Where(n => n.CategoryId == 3).ToList(), "Id", "Title", model.DirectionId);
            //danh sách loại giao dịch
            ViewBag.TypeId = new SelectList(db.tblDictionary.Where(n => n.CategoryId == 2).ToList(), "Id", "Title", model.TypeId);
            //loại bất động sản
            ViewBag.CategoryId = new SelectList(db.tblDictionary.Where(n => n.CategoryId == 1).ToList(), "Id", "Title", model.CategoryId);
            //Chọn Tỉnh  ProvinceId
            ViewBag.ProvinceId = new SelectList(db.tblProvince, "Id", "Name", model.ProvinceId);
            //chọn quận huyện 
            ViewBag.DistrictId = new SelectList(db.tblDistrict, "Id", "Name", model.DistrictId);
            //chon xã phường 
            ViewBag.WardId = new SelectList(db.tblWard, "Id", "Name", model.WardId);
            //pháp lý
            ViewBag.RuleId = new SelectList(db.tblDictionary.Where(n => n.CategoryId == 4).ToList(), "Id", "Title", model.RuleId);
        }
    }
}