﻿using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using PROJECTBDS.Infrastructure;
using PROJECTBDS.Models;
using PROJECTBDS.ViewModels;

namespace PROJECTBDS.Controllers
{
    [UserViewBag]
    public class UserController : Controller
    {
        readonly LandSoftEntities _db = new LandSoftEntities();

        int RowsPerPage = 15;

        public ActionResult Register()
        {
            ViewBag.ProvinceId = new SelectList(_db.tblProvince, "Id", "Name");
            ViewBag.DistrictId = new SelectList(_db.tblDistrict, "Id", "Name");
            ViewBag.WardId = new SelectList(_db.tblWard, "Id", "Name");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel form, HttpPostedFileBase imageUser)
        {
            ViewBag.ProvinceId = new SelectList(_db.tblProvince, "Id", "Name");
            ViewBag.DistrictId = new SelectList(_db.tblDistrict, "Id", "Name");
            ViewBag.WardId = new SelectList(_db.tblWard, "Id", "Name");

            var customer = _db.tblCustomer.Where(t => t.Email.Equals(form.Email.Trim()));
            if (customer.Any())
            {
                ModelState.AddModelError("Email", "Email này đã có người sử dụng. Vui lòng sử dụng email khác");
            }

            customer = _db.tblCustomer.Where(t => t.Username == form.Username.Trim());
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
            var user = _db.tblCustomer.FirstOrDefault(e => e.Username == form.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(form.Password, user.Password))
            {
                ModelState.AddModelError("Username", "Username hoặc password không chính sát");
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
            return RedirectToAction("Index", "User");
        }
        
        public ActionResult RealAdd()
        {
            var model = new RealViewModel
            {
                Projects = new SelectList(_db.tblProject, "Id", "Title"),
                Provinces = new SelectList(_db.tblProvince, "Id", "Name"),
                Districts = new SelectList(_db.tblDistrict, "Id", "Name"),
                Methods = new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.LoaiGd), "Id", "Title"),
                Types = new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.LoaiBds), "Id", "Title"),
                Units = new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.GiaCa), "Id", "Title"),
                Rules = new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.PhapLy), "Id", "Title"),
                Directions = new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.HuongNha), "Id", "Title"),
            };

            return View(model);
        }

        public ActionResult RealIndex(int? page, string query)
        {
            IQueryable<tblLand> model = _db.tblLand.Where(t => t.CustomerId == Auth.User.UserId).OrderByDescending(t => t.Id);

            var pageN = page ?? 1;

            return View(model.ToPagedList(pageN, RowsPerPage));
        }

        

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult RealAdd(RealViewModel f, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid) return View(f);

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

            if (image != null && image.AllowFile() && image.ContentLength > 0)
            {
                Directory.CreateDirectory(HostingEnvironment.ApplicationPhysicalPath + @"\Uploads\Reals\");
                
                var newName = image.FileName.Insert(image.FileName.LastIndexOf('.'), $"{DateTime.Now:_ddMMyyyy_hhss}");

                var path = Server.MapPath("~/Uploads/Reals/" + newName);

                if (!string.IsNullOrEmpty(newName))
                {
                    image.SaveAs(path);
                    land.Image = "/Uploads/Reals/" + newName;
                }
            }


            _db.tblLand.Add(land);
            _db.SaveChanges();

            return RedirectToAction("RealIndex");
        }

        public ActionResult DeleteReal()
        {
            throw new NotImplementedException();
        }

        public ActionResult EditReal(int id)
        {
            var r = _db.tblLand.Find(id);

            if (r == null) return HttpNotFound();

            var model = new RealViewModel
            {
                Id = r.Id,
                ProjectId = r.ProjectId ?? default(int),
                Title = r.Title,
                Area = r.Area,
                Content = r.Content,
                Address = r.Address,
                ProvinceId = r.ProvinceId ?? default(int),
                Image = r.Image,
                DistrictId = r.DirectionId ?? default(int),
                Price = r.Price ?? default(decimal),
                BlockCode = r.Code,
                DirectionId = r.DirectionId ?? default(int),
                Facede = r.Road,
                MethodId = r.TypeId ?? default(int),
                RuleId =  r.RuleId ?? default(int),
                UnitId = r.UnitId ?? default(int),
                TypeId = r.TypeId ?? default(int),
                Projects = new SelectList(_db.tblProject, "Id", "Title", r.ProjectId),
                Provinces = new SelectList(_db.tblProvince, "Id", "Name", r.ProvinceId),
                Districts = new SelectList(_db.tblDistrict, "Id", "Name", r.DistrictId),
                Methods = new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.LoaiGd), "Id", "Title", r.CategoryId),
                Types = new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.LoaiBds), "Id", "Title", r.TypeId),
                Units = new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.GiaCa), "Id", "Title", r.UnitId),
                Rules = new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.PhapLy), "Id", "Title", r.RuleId),
                Directions = new SelectList(_db.tblDictionary.Where(m => m.CategoryId == (int)EnumCategory.HuongNha), "Id", "Title")
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult EditReal(RealViewModel f, HttpPostedFileBase image)
        {
            var r = _db.tblLand.Find(f.Id);

            if (r == null) return HttpNotFound();

            if (!ModelState.IsValid) return View(f);

            r.Address = f.Address;
            r.Area = f.Area;
            r.Code = f.BlockCode;
            r.Content = f.Content;
            r.CustomerId = Auth.User.UserId;
            r.DirectionId = f.DirectionId;
            r.ProjectId = f.ProjectId;
            r.TypeId = f.TypeId;
            r.Price = f.Price;
            r.ProvinceId = f.ProvinceId;
            r.DistrictId = f.DistrictId;
            r.UnitId = f.UnitId;
            r.Title = f.Title;
            r.Email = f.ClientEmail;
            r.Phone = f.ClientCellPhone;
            r.Road = f.Facede;
            r.RuleId = f.RuleId;

            if (image != null && image.AllowFile() && image.ContentLength > 0)
            {
                Directory.CreateDirectory(HostingEnvironment.ApplicationPhysicalPath + @"\Uploads\Reals\");

                if (r.Image != null)
                {
                    FileExtensions.DeleteFile(r.Image.Split('/').Last(), "~/Uploads/Reals/");
                }
                var newName = image.FileName.Insert(image.FileName.LastIndexOf('.'), $"{DateTime.Now:_ddMMyyyy_hhss}");

                var path = Server.MapPath("~/Uploads/Reals/" + newName);

                if (!string.IsNullOrEmpty(newName))
                {
                    image.SaveAs(path);
                    r.Image = "/Uploads/Reals/" + newName;
                }
            }

            _db.Entry(r).State = EntityState.Modified;
            _db.SaveChanges();
            TempData["Update"] = "Cập nhật thành công";
            return RedirectToAction("EditReal", new {Id = r.Id });
        }

        public ActionResult Index()
        {
            var m = _db.tblCustomer.AsNoTracking().FirstOrDefault(t => t.Id == Auth.User.UserId);

            if (m == null) return HttpNotFound();

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
                Provinces = new SelectList(_db.tblProvince, "Id", "Name", m.ProvinceId),
                Districts = new SelectList(_db.tblDistrict.Where(t => t.ProvinceId == m.ProvinceId), "Id", "Name", m.DistrictId),
                Wards = new SelectList(_db.tblWard.Where(t => t.DistrictId == m.DistrictId), "Id", "Name", m.WardId),
                Country = m.Address
            };
            
            return View(user);
        }
        

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UpdateUser(UserEditViewModel f, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Hãy chọn Tình/Thành Phố và Quận/Huyện, Xã/Phường";
                return RedirectToAction("Index");
            }
            var user = _db.tblCustomer.FirstOrDefault(t => t.Id == Auth.User.UserId);

            if (user == null) Logout();

            user.DistrictId = f.DistrictId;
            user.Phone = f.Phone;
            user.Skype = f.Skype;
            user.Birthday = f.Birthday;
            user.Address = f.Country;
            user.ProvinceId = f.ProvinceId;
            user.FullName = f.FullName;
            user.WardId = f.WardId;
            user.Sex = f.Gender == EnumGender.Nam ? true : false;

            if (file != null  && file.AllowFile() && file.ContentLength > 0)
            {
                Directory.CreateDirectory(HostingEnvironment.ApplicationPhysicalPath + @"\Uploads\Avatars\");

                if (user.Image != null)
                {

                    FileExtensions.DeleteFile(user.Image.Split('/').Last(), "~/Uploads/Avatars/");
                }
                var newName = file.FileName.Insert(file.FileName.LastIndexOf('.'), $"{DateTime.Now:_ddMMyyyy_hhss}");
                var path = Server.MapPath("~/Uploads/Avatars/" + newName);

                if (!string.IsNullOrEmpty(newName))
                {
                    file.SaveAs(path);
                    user.Image = "/Uploads/Avatars/" + newName;
                }
            }
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult RealContact()
        {
            return View();
        }

        public ActionResult ChangePass()
        {

            var m = _db.tblCustomer.AsNoTracking().FirstOrDefault(t => t.Id == Auth.User.UserId);

            if (m == null) return RedirectToAction("Index", "Home");

            return View(new ChangePassUserViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangePass(ChangePassUserViewModel f)
        {

            var m = _db.tblCustomer.AsNoTracking().FirstOrDefault(t => t.Id == Auth.User.UserId);

            if (m == null) return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                m.Password = BCrypt.Net.BCrypt.HashPassword(f.Password1, 13);
                _db.Entry(m).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(f);
        }
    }
}
