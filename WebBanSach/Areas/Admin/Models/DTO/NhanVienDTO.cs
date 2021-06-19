using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Areas.Admin.Models.DTO
{
    public class NhanVienDTO
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public int? Luong { get; set; }
        public string MaBP { get; set; }
        public string TenBP { get; set; }
    }
}