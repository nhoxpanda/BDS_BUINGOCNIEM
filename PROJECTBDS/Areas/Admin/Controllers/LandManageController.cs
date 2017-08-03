using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using PROJECTBDS.Models;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    [Authorize]
    public class LandManageController : Controller
    {

        WEBSITEBDSEntities db = new WEBSITEBDSEntities();
        // GET: Admin/LandManage
        public ActionResult Index(int? page,string query)
        {
            ViewBag.query = query;
            int pageN = page ?? 1;
            var model = new List<tblLand>();
            if (!string.IsNullOrEmpty(query))
            {
                model = db.tblLand.Where(q => q.Title.Contains(query) || q.MetaTitle.Contains(query)).OrderByDescending(n => n.CreateDate).ToList();
            }
            else
            {
                model = db.tblLand.OrderByDescending(n => n.CreateDate).ToList();
            }
            return View(model.ToPagedList(pageN,30));
        }
        public ActionResult Create(tblLand model, IEnumerable<HttpPostedFileBase> ListImage)
        {

            if (Request["btnSave"] != null)
            {
                model.CreateDate = DateTime.Now;
               
                db.tblLand.Add(model);
                db.SaveChanges();
                int id = model.Id;
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
                        a.LandId = id;
                        a.DictionaryId = 47;
                        db.tblImage.Add(a);

                    }

                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            load(model);
            return View();
        }

        public ActionResult Update(int id)
        {
            var model = db.tblLand.Find(id);
            load(model);
            ViewBag.LsImage = db.tblImage.Where(n => n.LandId == id && n.DictionaryId == 47).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(tblLand model, IEnumerable<HttpPostedFileBase> ListImage)
        {
            if (ListImage!=null)
            {
                foreach (var item in ListImage)
                    {
                        String newName = item.FileName.Insert(item.FileName.LastIndexOf('.'), String.Format("{0:_ddMMyyyy}", DateTime.Now));
                        String path = Server.MapPath("~/Images/News/" + newName);
                        item.SaveAs(path);
                        tblImage a = new tblImage();
                        a.Image = newName;
                        a.URL = path;
                        a.LandId = model.Id;
                        a.DictionaryId = 47;
                        db.tblImage.Add(a);

                    }
            }
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var model = db.tblLand.FirstOrDefault(p => p.Id == id);
                db.tblLand.Remove(model);
                db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public void load(tblLand model)
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