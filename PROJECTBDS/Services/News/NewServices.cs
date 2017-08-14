using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PROJECTBDS.Models;
using PROJECTBDS.ViewModels;
using PROJECTBDS.ViewModels.Home;

namespace PROJECTBDS.Services.News
{
    public class NewServices
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<EventViewModel> GetNewsListByCate(int id)
        {
            var query = "SELECT top 10 n.Id, n.Title, n.Image, n.[Desc],  n.CreateDate FROM tblNews n "
                + "WHERE n.CateId = " + id + " ORDER BY n.CreateDate DESC";
            return (List<EventViewModel>)_db.Query<EventViewModel>(query);
        }

        public List<EventViewModel> GetNewsTop()
        {
            var query = "SELECT top 4 n.Id, n.Title, n.Image, n.[Desc],  n.CreateDate FROM tblNews n "
                + "JOIN tblDictionary d ON d.Id = n.CateId "
                + "WHERE d.CategoryId = 6 and d.[Delete] = 0 ORDER BY n.CreateDate DESC";
            return (List<EventViewModel>)_db.Query<EventViewModel>(query);
        }

        public List<EventViewModel> GetCateNewsTop(int id)
        {
            var query = "SELECT top 4 n.Id, n.Title, n.Image, n.[Desc],  n.CreateDate FROM tblNews n "
                + "JOIN tblDictionary d ON d.Id = n.CateId "
                + "WHERE n.CateId = " + id + " ORDER BY n.CreateDate DESC";
            return (List<EventViewModel>)_db.Query<EventViewModel>(query);
        }

        public List<EventViewModel> GetCateNews(int id)
        {
            var query = "SELECT n.Id, n.Title, n.Image, n.[Desc], n.CreateDate FROM tblNews n "
                + "JOIN tblDictionary d ON d.Id = n.CateId "
                + "WHERE n.CateId = " + id + " ORDER BY n.CreateDate DESC";
            return (List<EventViewModel>)_db.Query<EventViewModel>(query);
        }

        public List<EventViewModel> GetNewsOther(int id, int cate)
        {
            var query = "SELECT top 10 n.Id, n.Title, n.Image, n.[Desc], n.CreateDate FROM tblNews n "
                + "WHERE n.CateId = " + cate + " and n.Id <> " + id + " ORDER BY n.CreateDate DESC";
            return (List<EventViewModel>)_db.Query<EventViewModel>(query);
        }

        public List<tblNews> GetNewsDetail(int? id)
        {
            var query = "SELECT top 1 * FROM tblNews "
                + "WHERE Id = " + id;
            return (List<tblNews>)_db.Query<tblNews>(query);
        }

        public List<CategoryNewsViewModel> GetCategoryList()
        {
            var query = "SELECT Id, Title FROM tblDictionary WHERE CategoryId = 6";
            return (List<CategoryNewsViewModel>)_db.Query<CategoryNewsViewModel>(query);
        }

        public List<EventViewModel> SearchNews(string s)
        {
            var query = "select  n.Id, n.Title, n.Image, n.[Desc], n.CreateDate from tblNews n JOIN tblDictionary d ON d.Id = n.CateId"
                    + " CROSS JOIN (SELECT LTrim(RTRIM(sp.Data)) Splitdata FROM SplitString(N'" + s + "',' ')"
                    + " as sp UNION SELECT NULL) Split"
                    + " where (N'" + s + "' IS NULL"
                    + " OR(n.Title LIKE('%' + Split.splitdata + '%'))"
                    + " OR(n.[Desc] LIKE('%' + Split.splitdata + '%'))"
                    + " OR(n.Contents LIKE('%' + Split.splitdata + '%'))) AND d.CategoryId = 6";
            return (List<EventViewModel>)_db.Query<EventViewModel>(query);
        }
    }
}