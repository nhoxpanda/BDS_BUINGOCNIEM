using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROJECTBDS.Models;

namespace PROJECTBDS.Helpers
{
    public static class LoadData
    {
        private static Web_NiemBDSEntities _db = new Web_NiemBDSEntities();

        public static List<tblProvince> ProvinceList()
        {
            return _db.tblProvince.OrderBy(p => p.Name).ToList();
        }

        /// <summary>
        /// Loại BĐS
        /// </summary>
        /// <returns></returns>
        public static List<tblDictionary> CategoryList()
        {
            return _db.tblDictionary.Where(p => p.CategoryId == 1 && p.Delete == false).ToList();
        }

        /// <summary>
        /// Loại giao dịch
        /// </summary>
        /// <returns></returns>
        public static List<tblDictionary> TransactionList()
        {
            return _db.tblDictionary.Where(p => p.CategoryId == 2 && p.Delete == false).ToList();
        }

        /// <summary>
        /// Loại hướng nhà
        /// </summary>
        /// <returns></returns>
        public static List<tblDictionary> DirectionList()
        {
            return _db.tblDictionary.Where(p => p.CategoryId == 3 && p.Delete == false).ToList();
        }

        /// <summary>
        /// Loại pháp lý
        /// </summary>
        /// <returns></returns>
        public static List<tblDictionary> RuleList()
        {
            return _db.tblDictionary.Where(p => p.CategoryId == 4 && p.Delete == false).ToList();
        }

        /// <summary>
        /// Danh mục tin tức
        /// </summary>
        /// <returns></returns>
        public static List<tblDictionary> CateNewsList()
        {
            return _db.tblDictionary.Where(p => p.CategoryId == 6 && p.Delete == false).ToList();
        }


    }
}