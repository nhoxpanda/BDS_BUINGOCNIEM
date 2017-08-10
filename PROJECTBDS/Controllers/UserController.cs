using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.AspNet.Identity;
using PROJECTBDS.Models;
using PROJECTBDS.ViewModels;

namespace PROJECTBDS.Controllers
{
    public class UserController : Controller
    {
        LandSoftEntities db = new LandSoftEntities();

        private HttpCookie CreateStudentCookie(string value)
        {
            HttpCookie User = new HttpCookie("LandSoft");
            User.Value = value;
            User.Expires = DateTime.Now.AddHours(1);
            return User;
        }
        // GET: Register
        public ActionResult Register()
        {
            var model = db.tblLand;

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
            ViewBag.DistrictId = new SelectList(null, "Id", "Name");
            //chon xã phường 
            ViewBag.WardId = new SelectList(null, "Id", "Name");

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

        public ActionResult Create()
        {
            return View();
        }
    }
}
