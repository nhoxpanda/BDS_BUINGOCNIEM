using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using PROJECTBDS.Areas.Admin.Controllers.Api;
using PROJECTBDS.Areas.Admin.ViewModels;
using PROJECTBDS.Models;
using PROJECTBDS.ViewModels.Home;

namespace PROJECTBDS.Areas.Admin.Services
{
    public class LandServices
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<JsonHome> GetDistricts(long idProvince)
        {
            var query = "SELECT  Id, Name " +
                        "FROM tblDistrict " +
                        "WHERE ProvinceId = " + idProvince;

            return (List<JsonHome>)_db.Query<JsonHome>(query);
        }

        public List<JsonHome> GetWards(long idDistrict)
        {
            var query = "SELECT  Id, Name " +
                        "FROM tblWard " +
                        "WHERE DistrictId = " + idDistrict;

            return (List<JsonHome>)_db.Query<JsonHome>(query);
        }

        public List<JsonForProject> GetProjectDetail(int dtoProjectId, int dtoDictionaryId)
        {
            var query = "SELECT *" +
                        "FROM tblprojectdetail " +
                        "WHERE projectid = " + dtoProjectId + " " +
                        "AND DictionaryId = " + dtoDictionaryId;

            return (List<JsonForProject>)_db.Query<JsonForProject>(query);
        }
    }
}