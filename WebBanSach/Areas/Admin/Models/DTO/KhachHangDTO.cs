using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Areas.Admin.Models.DTO
{
    public class KhachHangDTO
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string TaiKhoan { get; set; }
    }
}