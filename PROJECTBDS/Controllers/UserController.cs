using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using PROJECTBDS.Models;
using PROJECTBDS.ViewModels;

namespace PROJECTBDS.Controllers
{
    public class UserController : Controller
    {
        LandSoftEntities db = new LandSoftEntities();

        private void CheckLoggin()
        {
            RedirectToAction("Index", !User.Identity.IsAuthenticated ? "Home" : "User");
        }
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

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(UserViewLogin form, string returnUrl)
        {
            var user = db.tblCustomer.FirstOrDefault(e => e.Username == form.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(form.Password, user.Password))
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


        public ActionResult RealAdd()
        {
            CheckLoggin();
            var idUser = Auth.User.UserId;

            if (idUser < 0) Logout();

            var u = db.tblCustomer.AsNoTracking().FirstOrDefault(t => t.Id == idUser);

            if (u == null) return HttpNotFound();

            ViewBag.User = new UserProfileViewModel { FullName = u.FullName, Avatar = u.Image };

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

        public ActionResult RealIndex(int? page, string query)
        {
            CheckLoggin();

            var idUser = Auth.User.UserId;

            if (idUser < 0) Logout();

            var m = db.tblCustomer.AsNoTracking().FirstOrDefault(t => t.Id == idUser);

            if (m == null) return HttpNotFound();

            ViewBag.User = new UserProfileViewModel { FullName = m.FullName, Avatar = m.Image };

            IQueryable<tblLand> model = db.tblLand.Where(t => t.CustomerId == Auth.User.UserId).OrderByDescending(t => t.Id);

            int pageN = page ?? 1;

            ViewBag.User = new UserProfileViewModel { FullName = m.FullName, Avatar = m.Image };

            return View(model.ToPagedList(pageN, RowsPerPage));
        }

        public int RowsPerPage = 15;

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult RealAdd(RealViewModel f, List<HttpPostedFileBase> imagesUser)
        {
            CheckLoggin();
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

        public ActionResult DeleteReal()
        {
            throw new NotImplementedException();
        }

        public ActionResult EditReal(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Index()
        {
            CheckLoggin();

            var idUser = Auth.User.UserId;

            if (idUser < 0) Logout();

            var m = db.tblCustomer.AsNoTracking().FirstOrDefault(t => t.Id == idUser);

            if (m == null) return HttpNotFound();

            ViewBag.User = new UserProfileViewModel { FullName = m.FullName, Avatar = m.Image };

            var user = new UserEditViewModel
            {
                FullName = m.FullName,
                Gender = m.Sex == true ? EnumGender.Nam : EnumGender.Nu,
                Phone = m.Phone,
                DistrictId = m.DistrictId ?? default(int),
                WardId = m.WardId ?? default(int),
                ProvinceId = m.ProvinceId ?? default(int),
                Skype = m.Skype,
                Image = m.Image,
                Birthday = m.Birthday ?? default(DateTime),
                Provinces = new SelectList(db.tblProvince, "Id", "Name", m.ProvinceId),
                Districts = new SelectList(db.tblDistrict.Where(t => t.ProvinceId == m.ProvinceId), "Id", "Name", m.DistrictId),
                Wards = new SelectList(db.tblWard.Where(t => t.DistrictId == m.DistrictId), "Id", "Name", m.WardId),
                Country = m.Address
            };


            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UpdateUser(UserEditViewModel f, HttpPostedFileBase file)
        {
            var userId = Auth.User.UserId;

            var user = db.tblCustomer.AsNoTracking().FirstOrDefault(t => t.Id == userId);

            if (user == null) Logout();

            user.DistrictId = f.DistrictId;
            user.Image = f.Image;
            user.Phone = f.Phone;
            user.Skype = f.Skype;
            user.Birthday = f.Birthday;
            user.Address = f.Country;
            user.ProvinceId = f.ProvinceId;
            user.FullName = f.FullName;
            user.WardId = f.WardId;
            user.Sex = f.Gender == EnumGender.Nam ? true : false;

            var imageAvatar = Request["file"];
            if (file != null && file.ContentLength > 0)
            {
                var newName = file.FileName.Insert(file.FileName.LastIndexOf('.'),
                    $"{DateTime.Now:_ddMMyyyy_hhss}");
                var path = Server.MapPath("~/Uploads/Avatars/" + newName);
                if (!string.IsNullOrEmpty(newName))
                {
                    file.SaveAs(path);
                    user.Image = "/Uploads/Avatars/" + newName;
                }
            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult RealContact()
        {
            return View();
        }

        public ActionResult ChangePass()
        {
            CheckLoggin();

            var idUser = Auth.User.UserId;

            if (idUser < 0) Logout();

            var m = db.tblCustomer.AsNoTracking().FirstOrDefault(t => t.Id == idUser);

            if (m == null) return RedirectToAction("Index", "Home");

            ViewBag.User = new UserProfileViewModel { FullName = m.FullName, Avatar = m.Image };

            return View(new ChangePassUserViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangePass(ChangePassUserViewModel f)
        {
            var idUser = Auth.User.UserId;

            if (idUser < 0) Logout();

            var m = db.tblCustomer.AsNoTracking().FirstOrDefault(t => t.Id == idUser);

            if (m == null) return RedirectToAction("Index", "Home");

            ViewBag.User = new UserProfileViewModel { FullName = m.FullName, Avatar = m.Image };


            if (ModelState.IsValid)
            {
                m.Password = BCrypt.Net.BCrypt.HashPassword(f.Password1, 13);
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(f);
        }
    }
}
