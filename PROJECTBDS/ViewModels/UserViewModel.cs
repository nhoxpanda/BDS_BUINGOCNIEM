using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PROJECTBDS.Models;

namespace PROJECTBDS.ViewModels
{
    public class UserViewLogin
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username không để trống", AllowEmptyStrings = false)]
        [RegularExpression("(^[a-zA-Z0-9_'-]+$)", ErrorMessage = "Username chỉ chứa các ký tự chữ, số và _")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không để trống"), MinLength(3, ErrorMessage = "Mật khẩu ít nhất 3 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Email { get; set; }

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
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
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

        [Required(ErrorMessage = "Email không để trống"), EmailAddress(ErrorMessage = "Địa chỉ email không đúng đinh dạng ab@host.xy")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Điện thoại không để trống"), MinLength(8, ErrorMessage = "Ít nhất 8 ký tự")]
        public string Phone { get; set; }
        public string Skype { get; set; }

        public EnumAccountType LoaiTaiKhoan { get; set; }


    }
}