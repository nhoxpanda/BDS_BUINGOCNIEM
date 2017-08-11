using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PROJECTBDS.Models;
using PROJECTBDS.ViewModels.Home;

namespace PROJECTBDS.Services.Home
{
    public class HomeServices
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);


        public List<DuAnNoiBatViewModel> GetDuAnNoiBat(int take = 0)
        {
            var query = "SELECT p.id, title, Content as excerpt, MetaTitle, metadesc, p.Image, Price, v.Name as Province, t.Name as District " +
                        "FROM tblProject p " +
                        "LEFT JOIN tblProvince v ON(v.Id = p.ProvinceId) " +
                        "LEFT JOIN tblDistrict t ON(t.Id = p.DistrictId and v.Id = t.ProvinceId) " +
                        "ORDER BY p.id DESC ";
            return (List<DuAnNoiBatViewModel>)_db.Query<DuAnNoiBatViewModel>(query);
        }

        public List<DuAnBdsViewModel> GetBatDongSan()
        {
            var query = "SELECT l.Id,l.Title as TieuDe,l.Image,Code as MaSoNhaDat,Area as DienTich,Road as DuongMatTruoc,d1.Title as HuongNha " +
                        ",d2.Title as LoaiBatDongSan " +
                        ",d3.Title as DonVi " +
                        ",d4.Title as LoaiGiaoDich " +
                        ",d.Name as ThanhPho " +
                        ",d0.Name as Quan " +
                        ",d5.Name as Phuong " +
                        ",d6.Title as PhapLy " +
                        ",d7.Title as DuAn " +
                        ",l.Price as Gia " +
                        "FROM tblLand l " +
                        "LEFT JOIN tblProvince d ON(d.Id = l.ProvinceId) " +
                        "LEFT JOIN tblDistrict d0 ON(d0.Id = l.DistrictId) " +
                        "LEFT JOIN tblWard d5 ON(d5.Id = l.WardId) " +
                        "LEFT JOIN tblDictionary d1 ON(d1.Id = l.DirectionId) " +
                        "LEFT JOIN tblDictionary d2 ON(d2.Id = l.CategoryId) " +
                        "LEFT JOIN tblDictionary d3 ON(d3.Id = l.UnitId) " +
                        "LEFT JOIN tblDictionary d4 ON(d4.Id = l.TypeId) " +
                        "LEFT JOIN tblDictionary d6 ON(d6.Id = l.RuleId) " +
                        "LEFT JOIN tblProject d7 ON(d7.Id = l.ProjectId) " +
                        "ORDER BY l.Id DESC";

            return (List<DuAnBdsViewModel>)_db.Query<DuAnBdsViewModel>(query);
        }

        public Land GetLand(long idLand)
        {
            var query = "SELECT l.Id, l.Title as TieuDe, l.Content, l.Address, " +
                        "l.Image, Code as MaSoNhaDat, Area as DienTich, " +
                        "Road as DuongMatTruoc, d1.Title as HuongNha " +
                        ",d2.Title as LoaiBatDongSan " +
                        ",d3.Title as DonVi " +
                        ",d4.Title as LoaiGiaoDich " +
                        ",d.Name as ThanhPho " +
                        ",d0.Name as Quan " +
                        ",d5.Name as Phuong " +
                        ",d6.Title as PhapLy " +
                        ",d7.Title as DuAn " +
                        ",l.Price as Gia " +
                        ",d8.FullName as ClientName " +
                        ",d8.Email as ClientEmail " +
                        "FROM tblLand l " +
                        "LEFT JOIN tblProvince d ON(d.Id = l.ProvinceId) " +
                        "LEFT JOIN tblDistrict d0 ON(d0.Id = l.DistrictId) " +
                        "LEFT JOIN tblWard d5 ON(d5.Id = l.WardId) " +
                        "LEFT JOIN tblDictionary d1 ON(d1.Id = l.DirectionId) " +
                        "LEFT JOIN tblDictionary d2 ON(d2.Id = l.CategoryId) " +
                        "LEFT JOIN tblDictionary d3 ON(d3.Id = l.UnitId) " +
                        "LEFT JOIN tblDictionary d4 ON(d4.Id = l.TypeId) " +
                        "LEFT JOIN tblDictionary d6 ON(d6.Id = l.RuleId) " +
                        "LEFT JOIN tblProject d7 ON(d7.Id = l.ProjectId) " +
                        "LEFT JOIN tblCustomer d8 ON(d8.Id = l.CustomerId) " +
                        "WHERE l.Id = " + idLand +
                        " ORDER BY l.Id DESC";

            return _db.Query<Land>(query).Single();
        }


        public List<JsonHome> GetDistricts(long idProvince)
        {
            var query = "SELECT  Id, Name " +
                        "FROM tblDistrict " +
                        "WHERE ProvinceId = " + idProvince;

            return (List<JsonHome>)_db.Query<JsonHome>(query);
        }

    }

}