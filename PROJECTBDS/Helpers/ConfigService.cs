using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROJECTBDS.Models;

namespace PROJECTBDS.Helpers
{
    public class ConfigService
    {
        private LandSoftEntities _db = new LandSoftEntities();
        public tblConfig GetData()
        {
            return _db.tblConfig.Find(1);
        }
    }
}