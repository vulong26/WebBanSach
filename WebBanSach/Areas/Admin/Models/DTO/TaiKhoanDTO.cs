using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanSach.Areas.Admin.Models.DTO
{
    public class TaiKhoanDTO
    {
        [Required(ErrorMessage = "Nhập tên đăng nhập")]
        public string TenDangNhap { get; set; }
        [Required(ErrorMessage = "Nhập mật khẩu")]
        public string MatKhau { get; set; }

        public string MaTK { get; set; }

        public string MatKhauMoi { get; set; }
    }
}