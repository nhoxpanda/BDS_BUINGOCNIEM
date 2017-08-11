using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.AspNet.Identity;
using PROJECTBDS.Areas.Admin.Dto;
using PROJECTBDS.Models;
using PROJECTBDS.ViewModels;

namespace PROJECTBDS.Controllers
{
    public class UserController : Controller
    {
        LandSoftEntities db = new LandSoftEntities();

        // GET: Register
        public ActionResult Register()
        {

            ViewBag.ProvinceId = new SelectList(db.tblProvince, "Id", "Name");
            //chọn quận huyện 
            ViewBag.DistrictId = new SelectList(db.tblDistrict, "Id", "Name");
            //chon xã phường 
            ViewBag.WardId = new SelectList(db.tblWard, "Id", "Name");
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel form, HttpPostedFileBase imageUser)
        {
            ViewBag.ProvinceId = new SelectList(db.tblProvince, "Id", "Name");
            //chọn quận huyện 
            ViewBag.DistrictId = new SelectList(db.tblDistrict, "Id", "Name");
            //chon xã phường 
            ViewBag.WardId = new SelectList(db.tblWard, "Id", "Name");

            var customer = db.tblCustomer.Where(t => t.Email.Equals(form.Email.Trim()));
            if (customer.Any())
            {
                ModelState.AddModelError("Email", "Email này đã có người sử dụng. Vui lòng sử dụng email khác");
            }

            customer = db.tblCustomer.Where(t => t.Username == form.Username.Trim());
            if (customer.Any())
            {
                ModelState.AddModelError("Username", "Username đã có người sử dụng. Vui lòng sử dụng Username khác!");
            }

            if (!ModelState.IsValid) return View(form);

            using (var data = new LandSoftEntities())
            {
                form.SetPassword(form.Password);
                var user = new tblCustomer
                {
                    Address = form.Address,
                    Birthday = form.Birthday,
                    Email = form.Email,
                    ProvinceId = form.ProvinceId,
                    DistrictId = form.DistrictId,
                    WardId = form.WardId,
                    Skype = form.Skype,
                    Sex = (form.Gender == EnumGender.Nam) ? true : false,
                    Username = form.Username,
                    Password = form.Password,
                    FullName = form.FullName,
                    Phone = form.Phone,

                };
                data.tblCustomer.Add(user);
                data.SaveChanges();
            }
            return View(form);
        }

       
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        public bool CheckPassword(string password, string check)
        {
            return BCrypt.Net.BCrypt.Verify(password, check);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(UserViewLogin form, string returnUrl)
        {
            var db = new LandSoftEntities();

            var user = db.tblCustomer.FirstOrDefault(e => e.Username == form.Username);

            if (user == null)
            {
                ModelState.AddModelError("Username", "Username or password is incorrect");
            }

            if (!ModelState.IsValid)
            {
                form.Password = string.Empty;
                return View(form);
            }

            Session.Add("UserName", user.Username);
            System.Web.HttpContext.Current.Session["UserName"] = user.Username;
            FormsAuthentication.SetAuthCookie(user.Username, true);

            Session.Timeout = 60;

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");


        }



        public ActionResult ProfileUser(int Id)
        {
            return View();
        }

        public ActionResult RealAdd()
        {
            
            var model = new RealViewModel
            {
                Projects = new SelectList(db.tblProject, "Id", "Title"),
                Provinces = new SelectList(db.tblProvince, "Id", "Name"),
                Districts = new SelectList(db.tblDistrict, "Id", "Name"),
                Methods = new SelectList(db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.LoaiGd), "Id", "Title"),
                Types = new SelectList(db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.LoaiBds), "Id", "Title"),
                Units = new SelectList(db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.GiaCa), "Id", "Title"),
                Rules = new SelectList(db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.PhapLy), "Id", "Title"),
                Directions = new SelectList(db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.HuongNha), "Id", "Title"),
            };

            return View(model);
        }

        public ActionResult RealIndex()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken,ValidateInput(false)]
        public ActionResult RealAdd(RealViewModel f, List<HttpPostedFileBase> imagesUser)
        {
            if (ModelState.IsValid)
            {
                var land = new tblLand
                {
                    Address = f.Address,
                    Area = f.Area,
                    Code = f.BlockCode,
                    Content = f.Content,
                    CreateDate = DateTime.Now,
                    CustomerId = Auth.User.UserId,
                    DirectionId = f.DirectionId,
                    ProjectId = f.ProjectId,
                    TypeId = f.TypeId,
                    Price = f.Price,
                    ProvinceId = f.ProvinceId,
                    DistrictId = f.DistrictId,
                    UnitId = f.UnitId,
                    Title = f.Title,
                    Email = f.ClientEmail,
                    Phone = f.ClientCellPhone,
                    Road = f.Facede,
                    RuleId = f.RuleId,
                };
                db.tblLand.Add(land);
                db.SaveChanges();
                return RedirectToAction("RealAdd");
            }
            return View(f);
        }
    }
}
