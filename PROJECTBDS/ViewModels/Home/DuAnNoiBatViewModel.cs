using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PROJECTBDS.ViewModels.Home
{

    public class HomeViewModel
    {
        public List<DuAnBdsViewModel> BatDongSan { get; set; }
        public List<DuAnNoiBatViewModel> DuAnNoiBat { get; set; }

    }

    public class DuAnNoiBatViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }

        public string Address
        {
            get
            {
                if (string.IsNullOrEmpty(District) && !string.IsNullOrEmpty(Province))
                    return Province;

                if (!string.IsNullOrEmpty(District) && string.IsNullOrEmpty(Province))
                    return District;

                return District + " - " + Province;
            }
        }

        public string Price { get; set; }

        public string Province { get; set; }
        public string District { get; set; }

        public string Image { get; set; }
    }

    public class DuAnBdsViewModel
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string Image { get; set; }
        public string MaSoNhaDat { get; set; }
        public string DienTich { get; set; }
        public string DuongMatTruoc { get; set; }
        public string HuongNha { get; set; }
        public string LoaiBatDongSan { get; set; }
        public string DonVi { get; set; }
        public string LoaiGiaoDich { get; set; }
        public string ThanhPho { get; set; }
        public string Quan { get; set; }
        public string Phuong { get; set; }
        public string PhapLy { get; set; }
        public string DuAn { get; set; }
        public string Gia { get; set; }

    }

    public class Land : DuAnBdsViewModel
    {
        public string Content { get; set; }
        public string Address { get; set; }
    }
}