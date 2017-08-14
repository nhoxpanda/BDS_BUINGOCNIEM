using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROJECTBDS.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreateDate { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Contents { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
        public string Map { get; set; }

    }

    public class CategoryNewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

    }
}