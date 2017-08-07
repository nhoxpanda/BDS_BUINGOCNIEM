using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PROJECTBDS.ViewModels.Home
{

    public class DuAnNoiBatViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }
        public string Address { get; set; }
        public string Cost { get; set; }

        public string Image { get; set; }
    }
}