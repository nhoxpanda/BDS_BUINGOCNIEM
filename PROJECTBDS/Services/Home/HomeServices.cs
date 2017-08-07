using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using PROJECTBDS.ViewModels.Home;

namespace PROJECTBDS.Services.Home
{
    public class HomeServices
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);


        public List<DuAnNoiBatViewModel> GetDuAnNoiBat(int take=0)
        {
            var query = "SELECT p.id, title, Content as excerpt, MetaTitle, metadesc, m.Image " +
                        "FROM tblProject p " +
                        "LEFT JOIN tblImage m ON (m.ProjectId = p.Id) " +
                        "ORDER BY p.id DESC ";
            return (List<DuAnNoiBatViewModel>)_db.Query<DuAnNoiBatViewModel>(query);
        }
    }
}