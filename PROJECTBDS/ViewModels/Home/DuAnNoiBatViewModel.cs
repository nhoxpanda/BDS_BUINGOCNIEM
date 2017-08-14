using System;
using System.Collections.Generic;
using PagedList;

namespace PROJECTBDS.ViewModels.Home
{

    public class JsonHome
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class HomeViewModel
    {
        public IPagedList<DuAnBdsViewModel> BatDongSan { get; set; }
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
        public string Unit { get; set; }

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



        public string DisplayPrice()
        {
            if (string.IsNullOrEmpty(Gia) || Gia.Equals("0")) return "Thỏa thuận";

            decimal number;

            var value = string.Empty;

            if (!decimal.TryParse(Gia, out number)) return value;

            if (number % 1 == 0) value = Convert.ToDecimal(Gia).ToString("N0") + " " + DonVi;
            if (number % 1 != 0) value = Convert.ToDecimal(Gia).ToString("N2") + " " + DonVi;
            return value;
        }
    }

    public class Land : DuAnBdsViewModel
    {
        public string Content { get; set; }
        public string Address { get; set; }

        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string Phone { get; set; }
    }
}