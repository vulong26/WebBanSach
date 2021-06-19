using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using WebBanSach.Areas.Admin.Models.Entities;

namespace WebBanSach.Areas.Admin.Models.DAO
{
    public class TacGiaDAO
    {
        BookModel model;
        public TacGiaDAO()
        {
            model = new BookModel();
        }
        public List<TacGia> ListTacGia()
        {
            return model.TacGias.ToList();
        }
        public List<TacGia> ListTacGia(int Pagenum, int PageSize)
        {
            return model.TacGias.Skip((Pagenum - 1) * PageSize).Take(PageSize).ToList();
        }
        public void Add(TacGia tg)
        {
            model.TacGias.Add(tg);
            model.SaveChanges();
        }
        public void Edit(TacGia tg)
        {
            TacGia tacgia = getByMaTG(tg.MaTG);
            if (tacgia != null)
            {
                tacgia.TenTG = tg.TenTG;
                tacgia.MoTa = tg.MoTa;
                tacgia.GioiTinh = tg.GioiTinh;
                tacgia.NgaySinh = tg.NgaySinh;
                tacgia.SoTacPham = tg.SoTacPham;
                tacgia.Image = tg.Image;
                model.SaveChanges();
            }
        }
        public int Delete(int MaTG)
        {
            TacGia tg = model.TacGias.Find(MaTG);
            if (tg != null)
            {
                model.TacGias.Remove(tg);
                return model.SaveChanges();
            }
            else
                return -1;
        }



        public TacGia getByMaTG(int MaTG)
        {
            return model.TacGias.FirstOrDefault(i => i.MaTG == MaTG);
        }

        public IEnumerable<TacGia> ListTacGiaPage(string searchString, string searchGioiTinh, string searchTuNgaySinh,
            string searchDenNgaySinh, string searchTuSoTacPham, string searchDenSoTacPham, int PageNum, int PageSize) // neu muon tim kiem thi them string key 
        {
            bool String = string.IsNullOrEmpty(searchString);

            bool GioiTinh = string.IsNullOrEmpty(searchGioiTinh);

            bool TuNgaySinh = string.IsNullOrEmpty(searchTuNgaySinh);

            bool DenNgaySinh = string.IsNullOrEmpty(searchDenNgaySinh);

            bool TuSoTacPham = string.IsNullOrEmpty(searchTuSoTacPham);

            bool DenSoTacPham = string.IsNullOrEmpty(searchDenSoTacPham);

            StringBuilder SqlCommand = new StringBuilder();

            SqlCommand.Append(" SELECT ");
            SqlCommand.Append(" tg.MaTG MaTG, ");
            SqlCommand.Append(" tg.TenTG TenTG, ");
            SqlCommand.Append(" tg.MoTa MoTa, ");
            SqlCommand.Append(" tg.GioiTinh GioiTinh, ");
            SqlCommand.Append(" tg.NgaySinh NgaySinh, ");
            SqlCommand.Append(" tg.SoTacPham SoTacPham, ");
            SqlCommand.Append(" tg.Image Image ");
            SqlCommand.Append(" FROM TacGia tg ");
            SqlCommand.Append(" WHERE 1=1 ");

            if (!String)
            {
                SqlCommand.Append(" and TenTG like N'%" + searchString + "%'");
                SqlCommand.Append(" or MoTa like N'%" + searchString + "%'");
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
                SqlCommand.Append(" and NgaySinh >= '" + strTuNgaySinh + "'" + " and NgaySinh <= '" + strDenNgaySinh + "'");
            }
            if (!TuSoTacPham && DenSoTacPham)
            {
                int fTuSoTacPham = Int32.Parse(searchTuSoTacPham);
                SqlCommand.Append(" and SoTacPham >= " + fTuSoTacPham);
            }
            if (!DenSoTacPham && TuSoTacPham)
            {
                int fDenSoTacPham = Int32.Parse(searchDenSoTacPham);
                SqlCommand.Append(" and SoTacPham <= " + fDenSoTacPham);
            }
            if (!TuSoTacPham && !DenSoTacPham)
            {
                int fTuSoTacPham = Int32.Parse(searchTuSoTacPham);
                int fDenSoTacPham = Int32.Parse(searchDenSoTacPham);

                SqlCommand.Append(" and SoTacPham >= " + fTuSoTacPham + " and SoTacPham <= " + fDenSoTacPham);
            }
            var lst = model.Database.SqlQuery<TacGia>("" + SqlCommand)
                .ToPagedList<TacGia>(PageNum, PageSize);    // phan trang thay cho limit va offset trong sql
            return lst;
        }
    }
}