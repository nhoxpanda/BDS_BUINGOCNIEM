using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PROJECTBDS.Models;

namespace PROJECTBDS.ViewModels
{
    public class ChangePassUserViewModel
    {
        public int UserId => Auth.User.UserId;

        [Required(ErrorMessage = "Mật khẩu không để trống"), MinLength(3, ErrorMessage = "Mật khẩu ít nhất 3 ký tự")]
        [DataType(DataType.Password)]
        public string Password1 { get; set; }

        [Required(ErrorMessage = "Mật khẩu không để trống"), MinLength(3, ErrorMessage = "Mật khẩu ít nhất 3 ký tự")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password1", ErrorMessage = "Mật khẩu không khớp")]
        public string Password2 { get; set; }

    }
    public class UserViewLogin
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username không để trống", AllowEmptyStrings = false)]
        [RegularExpression("(^[a-zA-Z0-9_'-]+$)", ErrorMessage = "Username chỉ chứa các ký tự chữ, số và _")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không để trống"), MinLength(3, ErrorMessage = "Mật khẩu ít nhất 3 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual string Email { get; set; }

        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }

        public virtual void SetPassword(string password)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(password, 13);
        }

    }

    public class UserViewModel : UserViewLogin
    {

        [Required(ErrorMessage = "Mật khẩu không để trống")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Họ tên không để trống")]
        public string FullName { get; set; }

        [Required]
        public EnumGender Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        [Required]
        public int ProvinceId { get; set; }

        [Required]
        public int DistrictId { get; set; }

        [Required]
        public int WardId { get; set; }

        [Required(ErrorMessage = "Email không để trống"),
         EmailAddress(ErrorMessage = "Địa chỉ email không đúng đinh dạng ab@host.xy")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Điện thoại không để trống"), MinLength(8, ErrorMessage = "Ít nhất 8 ký tự")]
        public string Phone { get; set; }

        public string Skype { get; set; }

        public EnumAccountType LoaiTaiKhoan { get; set; }


    }

    public class UserEditViewModel
    {
        public int UserId => Auth.User.UserId;

        [Required(ErrorMessage = "Nhập họ tên của bạn"), MinLength(3, ErrorMessage = "Ít nhất 3 ký tự")]
        public string FullName { get; set; }

        public DateTime Birthday { get; set; }
        public EnumGender Gender { get; set; }
        [Required(ErrorMessage = "Chọn Tỉnh/Thành Phố")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Chọn Huyện/Quận")]
        public int DistrictId { get; set; }

        [Required(ErrorMessage = "Chọn Xã/Phường")]
        public int WardId { get; set; }

        public string Country { get; set; }

        [Required(ErrorMessage = "Nhập điện thoại của bạn"), MinLength(8, ErrorMessage = "Ít nhất 8 ký tự")]
        public string Phone { get; set; }

        public string Skype { get; set; }
        public string Image { get; set; }

        public SelectList Provinces { get; set; }
        public SelectList Districts { get; set; }
        public SelectList Wards { get; set; }

    }

    public class UserProfileViewModel
    {
        public int UserId => Auth.User.UserId;
        public string FullName { get; set; }
        public string Avatar { get; set; }
    }

}