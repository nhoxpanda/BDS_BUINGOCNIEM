using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROJECTBDS.Models;

namespace PROJECTBDS.Areas.Admin.Models
{
    public static class LoadDataAdmin
    {
        private static WEBSITEBDSEntities _db = new WEBSITEBDSEntities();
        public static List<tblNews> CompanyInfo()
        {
            return _db.tblNews.Where(p => p.CateId == 23).ToList();
        }
    }
}