using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECTBDS.Models;
using PROJECTBDS.Services.News;
using PagedList;
using PagedList.Mvc;

namespace PROJECTBDS.Controllers
{
    public class NewsController : Controller
    {
        private LandSoftEntities _db = new LandSoftEntities();
        private NewServices _data = new NewServices();

        public ActionResult Index()
        {
            ViewBag.CategoryList = _data.GetCategoryList();
            return View(_data.GetNewsTop());
        }

        public ActionResult Cate(int id, int? page)
        {
            int pageN = page ?? 1;
            var model = _data.GetCateNews(id).ToList();
            ViewBag.CategoryList = _data.GetCategoryList();
            ViewBag.GetCateNewsTop = _data.GetCateNewsTop(id);
            ViewBag.Dictionary = _db.tblDictionary.Find(id);
            return View(model.ToPagedList(pageN, 30));
        }

        public ActionResult Detail(int? id)
        {
            var model = _data.GetNewsDetail(id).FirstOrDefault();
            ViewBag.GetNewsOther = _data.GetNewsOther(id ?? 0, model.CateId ?? 0);
            ViewBag.CategoryList = _data.GetCategoryList();
            return View(model);
        }

        public ActionResult Search(string s, int? page)
        {
            ViewBag.CategoryList = _data.GetCategoryList();
            ViewBag.Keyword = s;
            int pageN = page ?? 1;
            var model = _data.SearchNews(s).ToList();
            return View(model.ToPagedList(pageN, 30));
        }
    }
}