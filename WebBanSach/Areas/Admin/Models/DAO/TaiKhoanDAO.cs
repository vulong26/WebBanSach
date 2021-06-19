using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebBanSach.Areas.Admin.Models.Entities;

namespace WebBanSach.Areas.Admin.Models.DAO
{
    public class TaiKhoanDAO
    {
        private BookModel db = null;
        public TaiKhoanDAO()
        {
            db = new BookModel();
        }

        public bool Login(string TenDangNhap, string MatKhau)
        {
            object[] sqlParams = new SqlParameter[]
            {
            new SqlParameter("TenDangNhap",TenDangNhap),
            new SqlParameter("MatKhau", MatKhau),
            };
            var res = db.Database.SqlQuery<bool>("DangNhapAdmin @TenDangNhap,@MatKhau", sqlParams).SingleOrDefault();
            return res;
        }
        public bool checkLogin(string username, string password)
        {
            bool check = false;
            int a = db.TaiKhoans.Where(y => y.TenDangNhap == username && y.MatKhau == password).ToList().Count();

            if (a >= 1)
                check = true;
            return check;

        }
        //public void UpdateTaiKhoan(TaiKhoan taikhoan)
        //{
        //    TaiKhoan tk = db.TaiKhoans.Find(taikhoan.TenDangNhap);
        //    if (tk != null)
        //    {
        //        tk.TenDangNhap = taikhoan.TenDangNhap;
        //        if(tk.MatKhau == taikhoan.MatKhau)
        //        {
        //            tk.MatKhau = taikhoan.MatKhauMoi;
        //        }  
        //        else
        //        db.SaveChanges();
        //    }
        //}
    }
}