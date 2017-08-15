using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROJECTBDS.Models;

namespace PROJECTBDS.Helpers
{
    public static class LoadData
    {
        private static LandSoftEntities _db = new LandSoftEntities();

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

        /// <summary>
        /// Danh mục thời gian
        /// </summary>
        /// <returns></returns>
        public static List<tblDictionary> TimeList()
        {
            return _db.tblDictionary.Where(p => p.CategoryId == 9 && p.Delete == false).ToList();
        }

        /// <summary>
        /// Danh mục loại dự án
        /// </summary>
        /// <returns></returns>
        public static List<tblDictionary> ProjectTypeList()
        {
            return _db.tblDictionary.Where(p => p.CategoryId == 14 && p.Delete == false).ToList();
        }
    }
}