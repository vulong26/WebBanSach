using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Areas.Admin.Models.DTO
{
    public class TacGiaDTO
    {
        public string MaTG { get; set; }

        public string TenTG { get; set; }

        public string GioiTinh { get; set; }

        public string MoTa { get; set; }

        public int? SoTacPham { get; set; }

        public string Image { get; set; }
    }
}