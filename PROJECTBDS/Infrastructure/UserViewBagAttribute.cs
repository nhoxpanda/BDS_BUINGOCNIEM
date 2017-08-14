using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using PROJECTBDS.Models;
using PROJECTBDS.ViewModels;

namespace PROJECTBDS.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserViewBagAttribute : ActionFilterAttribute
    {
       // private readonly string _selectedTab;

        private readonly LandSoftEntities _db = new LandSoftEntities();

        //public UserViewBagAttribute(string selectedTab)
        //{
        //    _selectedTab = selectedTab;
        //}

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //filterContext.Controller.ViewBag.SelectedTab = _selectedTab;
            if (Auth.User == null) return;
                var idUser = Auth.User.UserId;

            if (idUser < 0) return ;

            var user = _db.tblCustomer.FirstOrDefault(t => t.Id == idUser);

            filterContext.Controller.ViewBag.User = user != null 
                ? new UserProfileViewModel { FullName = user.FullName, Avatar = user.Image } 
                : new UserProfileViewModel();

            
            var dis = filterContext.HttpContext.Request.QueryString["district"].AsInt();
            
            filterContext.Controller.ViewBag.ProvinceId = new SelectList(_db.tblProvince, "Id", "Name", filterContext.HttpContext.Request.QueryString["province"].AsInt());

            if (dis > 0)
            {
                filterContext.Controller.ViewBag.DistrictId = new SelectList(_db.tblDistrict.Where(t=>t.ProvinceId == dis), "Id", "Name",
                    filterContext.HttpContext.Request["district"]);

            }
            else
            {
                filterContext.Controller.ViewBag.DistrictId = new SelectList(_db.tblDistrict, "Id", "Name", filterContext.HttpContext.Request.QueryString["district"]);

            }

            filterContext.Controller.ViewBag.WardId = new SelectList(_db.tblWard, "Id", "Name");
            filterContext.Controller.ViewBag.Categories = new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int) EnumCategory.LoaiBds), "Id",
                "Title", filterContext.HttpContext.Request.QueryString["type"].AsInt());
            filterContext.Controller.ViewBag.Types =
                new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int) EnumCategory.LoaiGd), "Id", "Title", filterContext.HttpContext.Request.QueryString["method"].AsInt());
            filterContext.Controller.ViewBag.Directions =
                new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int) EnumCategory.HuongNha), "Id", "Title", filterContext.HttpContext.Request.QueryString["direction"].AsInt());


        }
    }
}