using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Areas.Admin.Models.DTO
{
    public class SachDTO
    {
        public string MaS { get; set; }
        public string TenS { get; set; }

        public string MoTa { get; set; }

        public string TenFileAnh { get; set; }

        public int? GiaBan { get; set; }

        public DateTime? NgayTaiLen { get; set; }

        public string TrangThai { get; set; }

        public int? SoLuong { get; set; }

        public string MaNXB { get; set; }
        public string TenNXB { get; set; }

        public string MaTL { get; set; }

        public string TenTL { get; set; }

        public string MaTG { get; set; }
        public string TenTG { get; set; }
    }
}