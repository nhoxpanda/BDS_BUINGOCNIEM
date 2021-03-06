﻿using System;
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

           
        }
    }
}