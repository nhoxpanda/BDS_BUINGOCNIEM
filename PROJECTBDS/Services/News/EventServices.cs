using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System;
using System.Linq;
using Dapper;
using PROJECTBDS.Models;
using PROJECTBDS.ViewModels;
using PROJECTBDS.ViewModels.Home;
using System.Data.SqlClient;

namespace PROJECTBDS.Services.News
{
    public class EventServices
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<EventViewModel> GetEventList()
        {
            var query = "SELECT n.Id, n.Title, n.Image, n.Address, n.[Desc], n.Date, p.Name as Province, d.Name as District FROM tblNews n "
                + "LEFT JOIN tblProvince p ON p.Id = n.ProvinceId "
                + "LEFT JOIN tblDistrict d ON d.Id = n.DistrictId "
                + "WHERE n.CateId = 24 ORDER BY Date DESC";
            return (List<EventViewModel>)_db.Query<EventViewModel>(query);
        }

        public List<EventViewModel> GetEventDetail(int? id)
        {
            var query = "SELECT top 1 n.Id, n.Title, n.Image, n.Address, n.MetaTitle, n.MetaDesc, n.[Desc], n.Contents, n.Date, n.Map, p.Name as Province, d.Name as District FROM tblNews n "
                + "LEFT JOIN tblProvince p ON p.Id = n.ProvinceId "
                + "LEFT JOIN tblDistrict d ON d.Id = n.DistrictId "
                + "WHERE n.Id = " + id;
            return (List<EventViewModel>)_db.Query<EventViewModel>(query);
        }

        public List<EventViewModel> SearchEvent(string s, int timetype, DateTime day, int province)
        {
            var query = "select n.Id, n.Title, n.[Image], n.[Desc], n.CreateDate from tblNews n "
                    + " JOIN tblProvince p ON p.Id = n.ProvinceId"
                    + " JOIN tblDictionary d ON d.Id = n.TimeId"
                    + " CROSS JOIN (SELECT LTrim(RTRIM(sp.Data)) Splitdata FROM SplitString(N'" + s + "',' ')"
                    + " as sp UNION SELECT NULL) Split"
                    + " where (N'" + s + "' IS NULL"
                    + " OR(n.Title LIKE('%' + Split.splitdata + '%'))"
                    + " OR(n.[Desc] LIKE('%' + Split.splitdata + '%'))"
                    + " OR(n.Contents LIKE('%' + Split.splitdata + '%'))) AND n.CateId = 24"
                    + (timetype == 0 || timetype == -1 ? "" : " AND n.TimeId = " + timetype)
                    + (province == 0 ? "" : " AND n.ProvinceId = " + province)
                    + (day == null ? "" : " AND convert(varchar(10), n.[Date], 120) = '" + day.ToString("yyyy-MM-dd") + "'");

            return (List<EventViewModel>)_db.Query<EventViewModel>(query);
        }
    }
}