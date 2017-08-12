using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PROJECTBDS.ViewModels
{
    public class RealViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nhập tiêu đề")]
        [MinLength(10, ErrorMessage = "Ít nhất 10 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Nhập địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Chọn Thành Phố/Tỉnh")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Chọn Quân/Huyện")]
        public int DistrictId { get; set; }

        [Required(ErrorMessage = "Chọn phương thức thanh toán")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Chọn kiểu bất động sản")]
        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        [Required(ErrorMessage = "Chọn kiểu đơn vị")]
        public int UnitId { get; set; }

        [Required(ErrorMessage = "Chọn hướng nhà")]
        public int DirectionId { get; set; }

        [Required(ErrorMessage = "Chọn loại pháp lý")]
        public int RuleId { get; set; }

        [Required(ErrorMessage = "Chọn dự án cho bất động sản này")]
        public int ProjectId { get; set; }
        public string BlockCode { get; set; }

        [Required(ErrorMessage = "Nhập độ rộng đường trước nhà")]
        public string Facede { get; set; }

        [Required(ErrorMessage = "Nhập diện tích (dài x rộng)")]
        public string Area { get; set; }

        [Required(ErrorMessage = "Nhập nội dung chính")]
        [AllowHtml]
        public string Content { get; set; }
        public string Image { get; set; }
        public string ImageThumb { get; set; }

        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientCompany { get; set; }
        public string ClientCellPhone { get; set; }
        public string ClientPhone { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string ClientEmail { get; set; }

        public SelectList Projects { get; set; }

        public SelectList Types { get; set; }
        public SelectList Categories { get; set; }

        public SelectList Rules { get; set; }

        public SelectList Units { get; set; }
        public SelectList Directions { get; set; }

        public SelectList Provinces { get; set; }
        public SelectList Districts { get; set; }

    }
}