using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebBanSach.Areas.Admin.Models.Entities;

namespace WebBanSach.Areas.Admin.Models.DAO
{
    public class KhachHangDAO
    {
        BookModel model;
        public KhachHangDAO()
        {
            model = new BookModel();
        }
        public void Add(KhachHang kh)
        {
            model.KhachHangs.Add(kh);
            model.SaveChanges();
        }
        public void Edit(KhachHang kh)
        {
            KhachHang khachhang = getByMaKH(kh.MaKH);
            if (khachhang != null)
            {
                khachhang.HoTen = kh.HoTen;
                khachhang.Email = kh.Email;
                khachhang.DiaChi = kh.DiaChi;
                khachhang.DienThoai = kh.DienThoai;
                khachhang.GioiTinh = kh.GioiTinh;
                khachhang.NgaySinh = kh.NgaySinh;
                khachhang.MatKhau = kh.MatKhau;
                khachhang.TaiKhoan = kh.TaiKhoan;
                model.SaveChanges();
            }
        }
        public int Delete(int MaKH)
        {
            KhachHang kh = model.KhachHangs.Find(MaKH);
            if (kh != null)
            {
                model.KhachHangs.Remove(kh);
                return model.SaveChanges();
            }
            else
                return -1;
        }



        public KhachHang getByMaKH(int MaKH)
        {
            return model.KhachHangs.FirstOrDefault(i => i.MaKH == MaKH);
        }

        public IEnumerable<KhachHang> ListKhachHangPage(string searchString, string searchSDT, string searchDC, string searchGioiTinh, string searchTuNgaySinh,
            string searchDenNgaySinh, string searchEmail, int PageNum, int PageSize) // neu muon tim kiem thi them string key 
        {
            bool String = string.IsNullOrEmpty(searchString);

            bool SDT = string.IsNullOrEmpty(searchSDT);

            bool DC = string.IsNullOrEmpty(searchDC);

            bool GioiTinh = string.IsNullOrEmpty(searchGioiTinh);

            bool TuNgaySinh = string.IsNullOrEmpty(searchTuNgaySinh);

            bool DenNgaySinh = string.IsNullOrEmpty(searchDenNgaySinh);

            bool sEmail = string.IsNullOrEmpty(searchEmail);

            StringBuilder SqlCommand = new StringBuilder();

            SqlCommand.Append(" SELECT ");
            SqlCommand.Append(" kh.MaKH MaKH, ");
            SqlCommand.Append(" kh.HoTen HoTen, ");
            SqlCommand.Append(" kh.Email Email, ");
            SqlCommand.Append(" kh.DiaChi DiaChi, ");
            SqlCommand.Append(" kh.DienThoai DienThoai, ");
            SqlCommand.Append(" kh.GioiTinh GioiTinh, ");
            SqlCommand.Append(" kh.NgaySinh NgaySinh, ");
            SqlCommand.Append(" kh.MatKhau MatKhau, ");
            SqlCommand.Append(" kh.TaiKhoan TaiKhoan ");
            SqlCommand.Append(" FROM KhachHang kh ");
            SqlCommand.Append(" WHERE 1=1 ");

            if (!String)
            {
                SqlCommand.Append(" and HoTen like N'%" + searchString + "%'");
            }
            if (!SDT)
            {
                SqlCommand.Append(" and DienThoai like N'%" + searchSDT + "%'");
            }
            if (!DC)
            {
                SqlCommand.Append(" and DiaChi like N'%" + searchDC + "%'");
            }
            if (!GioiTinh)
            {
                SqlCommand.Append(" and GioiTinh like N'%" + searchGioiTinh + "%'");
            }
            if (!TuNgaySinh && DenNgaySinh)
            {
                DateTime dtTuNgaySinh = DateTime.Parse(searchTuNgaySinh);
                string strTuNgaySinh = dtTuNgaySinh.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgaySinh > '" + strTuNgaySinh + "'");
            }
            if (!DenNgaySinh && TuNgaySinh)
            {
                DateTime dtDenNgaySinh = DateTime.Parse(searchDenNgaySinh);
                string strDenNgaySinh = dtDenNgaySinh.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgaySinh < '" + strDenNgaySinh + "'");
            }
            if (!TuNgaySinh && !DenNgaySinh)
            {
                DateTime dtTuNgaySinh = DateTime.Parse(searchTuNgaySinh);
                DateTime dtDenNgaySinh = DateTime.Parse(searchDenNgaySinh);

                string strTuNgaySinh = dtTuNgaySinh.ToString("yyyy/MM/dd");
                string strDenNgaySinh = dtDenNgaySinh.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgaySinh > '" + strTuNgaySinh + "'" + " and NgaySinh < '" + strDenNgaySinh + "'");
            }
            if (!sEmail)
            {
                SqlCommand.Append(" and Email like N'%" + searchEmail + "%'");
            }
            var lst = model.Database.SqlQuery<KhachHang>("" + SqlCommand)
                .ToPagedList<KhachHang>(PageNum, PageSize);    // phan trang thay cho limit va offset trong sql
            return lst;
        }
    }
}