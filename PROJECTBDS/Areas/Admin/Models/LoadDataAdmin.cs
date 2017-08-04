using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROJECTBDS.Models;

namespace PROJECTBDS.Areas.Admin.Models
{
    public static class LoadDataAdmin
    {
        private static Web_NiemBDSEntities _db = new Web_NiemBDSEntities();
        public static List<tblNews> CompanyInfo()
        {
            return _db.tblNews.Where(p => p.CateId == 23).ToList();
        }
    }
}