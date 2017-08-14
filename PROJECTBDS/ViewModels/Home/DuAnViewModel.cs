using System.Collections.Generic;
using PROJECTBDS.Models;

namespace PROJECTBDS.ViewModels.Home
{
    public class DuAnViewModel
    {
        public tblProject DuAn { get; set; }
        public List<DuAnNoiBatViewModel> DuAnKhac { get; set; }

    }
}