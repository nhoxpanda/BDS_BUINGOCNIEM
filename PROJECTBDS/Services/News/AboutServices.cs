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
    public class AboutServices
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<tblNews> GetAboutList()
        {
            var query = "SELECT Id, Title FROM tblNews WHERE CateId = 23 ORDER BY Id";
            return (List<tblNews>)_db.Query<tblNews>(query);
        }

        public List<tblNews> GetAboutDetail(int? id)
        {
            var query = "SELECT top 1 * " +
                        "FROM tblNews " +
                        "WHERE Id = " + id + " and CateId = 23 " +
                        "ORDER BY Id ";
            return (List<tblNews>)_db.Query<tblNews>(query);
        }
    }
}