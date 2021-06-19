using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Areas.Admin.Models.DTO
{
    public class NhaXuatBanDTO
    {
        public int MaNXB { get; set; }

        public string TenNXB { get; set; }

        public string SDT { get; set; }

        public string DiaChi { get; set; }
    }
}