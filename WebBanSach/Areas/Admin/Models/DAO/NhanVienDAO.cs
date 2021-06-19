using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebBanSach.Areas.Admin.Models.Entities;

namespace WebBanSach.Areas.Admin.Models.DAO
{
    public class NhanVienDAO
    {
        BookModel model;
        public NhanVienDAO()
        {
            model = new BookModel();
        }
        public void Add(NhanVien nv)
        {
            model.NhanViens.Add(nv);
            model.SaveChanges();
        }
        public void Edit(NhanVien nv)
        {
            NhanVien nhanvien = getByMaNV(nv.MaNV);
            if (nhanvien != null)
            {
                nhanvien.TenNV = nv.TenNV;
                nhanvien.SDT = nv.SDT;
                nhanvien.DiaChi = nv.DiaChi;
                nhanvien.GioiTinh = nv.GioiTinh;
                nhanvien.NgaySinh = nv.NgaySinh;
                nhanvien.Luong = nv.Luong;
                nhanvien.MaBP = nv.MaBP;
                nhanvien.Anh = nv.Anh;
                model.SaveChanges();
            }
        }
        public int Delete(int MaNV)
        {
            NhanVien nv = model.NhanViens.Find(MaNV);
            if (nv != null)
            {
                model.NhanViens.Remove(nv);
                return model.SaveChanges();
            }
            else
                return -1;
        }



        public NhanVien getByMaNV(int MaNV)
        {
            return model.NhanViens.FirstOrDefault(i => i.MaNV == MaNV);
        }

        public IEnumerable<NhanVien> ListNhanVienPage(string searchString, string searchSDT, string searchDC, string searchGioiTinh, string searchTuNgaySinh,
            string searchDenNgaySinh, string searchTuLuong, string searchDenLuong, string searchBoPhan ,int PageNum, int PageSize) // neu muon tim kiem thi them string key 
        {
            bool String = string.IsNullOrEmpty(searchString);

            bool SDT = string.IsNullOrEmpty(searchSDT);

            bool DC = string.IsNullOrEmpty(searchDC);

            bool GioiTinh = string.IsNullOrEmpty(searchGioiTinh);

            bool TuNgaySinh = string.IsNullOrEmpty(searchTuNgaySinh);

            bool DenNgaySinh = string.IsNullOrEmpty(searchDenNgaySinh);

            bool TuLuong = string.IsNullOrEmpty(searchTuLuong);

            bool DenLuong = string.IsNullOrEmpty(searchDenLuong);

            bool BoPhan = string.IsNullOrEmpty(searchBoPhan);

            StringBuilder SqlCommand = new StringBuilder();

            SqlCommand.Append(" SELECT ");
            SqlCommand.Append(" nv.MaNV MaNV, ");
            SqlCommand.Append(" nv.TenNV TenNV, ");
            SqlCommand.Append(" nv.SDT SDT, ");
            SqlCommand.Append(" nv.DiaChi DiaChi, ");
            SqlCommand.Append(" nv.GioiTinh GioiTinh, ");
            //SqlCommand.Append(" FORMAT (nv.NgaySinh, 'dd/MM/yyyy ') NgaySinh, ");
            //select convert(varchar, NGAYSINH, 103) as NGAYSINH from NhanVien nv
            SqlCommand.Append(" nv.NgaySinh NgaySinh, ");
            SqlCommand.Append(" nv.Luong Luong, ");
            SqlCommand.Append(" nv.MaBP MaBP, ");
            SqlCommand.Append(" nv.Anh Anh ");
            SqlCommand.Append(" FROM NhanVien nv ");
            SqlCommand.Append(" WHERE 1=1 ");

            if (!String)
            {
                SqlCommand.Append(" and TenNV like N'%" + searchString + "%'");
            }
            if (!SDT)
            {
                SqlCommand.Append(" and SDT like N'%" + searchSDT + "%'");
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
                SqlCommand.Append(" and NgaySinh >= '" + strTuNgaySinh + "'");
            }
            if (!DenNgaySinh && TuNgaySinh)
            {
                DateTime dtDenNgaySinh = DateTime.Parse(searchDenNgaySinh);
                string strDenNgaySinh = dtDenNgaySinh.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgaySinh <= '" + strDenNgaySinh + "'");
            }
            if (!TuNgaySinh && !DenNgaySinh)
            {
                DateTime dtTuNgaySinh = DateTime.Parse(searchTuNgaySinh);
                DateTime dtDenNgaySinh = DateTime.Parse(searchDenNgaySinh);

                string strTuNgaySinh = dtTuNgaySinh.ToString("yyyy/MM/dd");
                string strDenNgaySinh = dtDenNgaySinh.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgaySinh >= '" + strTuNgaySinh + "'"+ " and NgaySinh <= '" + strDenNgaySinh + "'");
            }
            if(!TuLuong && DenLuong)
            {
                float fTuLuong = float.Parse(searchTuLuong);
                SqlCommand.Append(" and Luong >= " + fTuLuong);
            }
            if (!DenLuong && TuLuong)
            {
                float fDenLuong = float.Parse(searchDenLuong);
                SqlCommand.Append(" and Luong <= " + fDenLuong);
            }
            if (!TuLuong && !DenLuong)
            {
                float fTuLuong = float.Parse(searchTuLuong);
                float fDenLuong = float.Parse(searchDenLuong);

                SqlCommand.Append(" and Luong >= " + fTuLuong + " and Luong <= " + fDenLuong);
            }
            if(!BoPhan)
            {
                SqlCommand.Append(" and MaBP = N'" + searchBoPhan + "'" + "and nv.MaBP in(select MaBP from BoPhan bp)");
            }    
            var lst = model.Database.SqlQuery<NhanVien>("" + SqlCommand)
                .ToPagedList<NhanVien>(PageNum, PageSize);    // phan trang thay cho limit va offset trong sql
            return lst;
        }
    }
}