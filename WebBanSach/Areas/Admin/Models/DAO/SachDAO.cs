using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using WebBanSach.Areas.Admin.Models.DTO;
using WebBanSach.Areas.Admin.Models.Entities;

namespace WebBanSach.Areas.Admin.Models.DAO
{
    public class SachDAO
    {
        BookModel model;
        public SachDAO()
        {
            model = new BookModel();
        }
        public List<Sach> ListSach()
        {
            return model.Saches.ToList();
        }
        public List<Sach> ListSach(int Pagenum, int PageSize)
        {
            return model.Saches.Skip((Pagenum - 1) * PageSize).Take(PageSize).ToList();
        }
        public void Add(Sach nv)
        {
            model.Saches.Add(nv);
            model.SaveChanges();
        }
        public void Edit(Sach sa)
        {
            Sach sach = getByMaS(sa.MaS);
            if (sach != null)
            {
                sach.TenS = sa.TenS;
                sach.MoTa = sa.MoTa;
                sach.TenFileAnh = sa.TenFileAnh;
                sach.GiaBan = sa.GiaBan;
                sach.NgayTaiLen = sa.NgayTaiLen;
                sach.TrangThai = sa.TrangThai;
                sach.SoLuong = sa.SoLuong;
                sach.MaTL = sa.MaTL;
                sach.MaTG = sa.MaTG;
                sach.MaNXB = sa.MaNXB;
                model.SaveChanges();
            }
        }
        public int Delete(int MaS)
        {
            Sach sa = model.Saches.Find(MaS);
            if (sa != null)
            {
                model.Saches.Remove(sa);
                return model.SaveChanges();
            }
            else
                return -1;
        }



        public Sach getByMaS(int MaS)
        {
            return model.Saches.FirstOrDefault(i => i.MaS == MaS);
        }

        public IEnumerable<Sach> ListSachPage(string searchString, string searchTuNgay,
            string searchDenNgay, string searchTuGia, string searchDenGia, string searchTuSL, string searchDenSL, 
            string searchTheLoai, string searchTacGia, string searchNhaXuatBan, int PageNum, int PageSize) // neu muon tim kiem thi them string key 
        {
            bool String = string.IsNullOrEmpty(searchString);

            bool TuNgay = string.IsNullOrEmpty(searchTuNgay);

            bool DenNgay = string.IsNullOrEmpty(searchDenNgay);

            bool TuGia = string.IsNullOrEmpty(searchTuGia);

            bool DenGia = string.IsNullOrEmpty(searchDenGia);

            bool TuSL = string.IsNullOrEmpty(searchTuSL);

            bool DenSL = string.IsNullOrEmpty(searchDenSL);

            bool sTheLoai = string.IsNullOrEmpty(searchTheLoai);

            bool sTacGia = string.IsNullOrEmpty(searchTacGia);

            bool sNhaXuatBan = string.IsNullOrEmpty(searchNhaXuatBan);

            StringBuilder SqlCommand = new StringBuilder();

            SqlCommand.Append(" SELECT ");
            SqlCommand.Append(" sa.MaS MaS, ");
            SqlCommand.Append(" sa.TenS TenS, ");
            SqlCommand.Append(" sa.MoTa MoTa, ");
            SqlCommand.Append(" sa.TenFileAnh TenFileAnh, ");
            SqlCommand.Append(" sa.GiaBan GiaBan, ");
            SqlCommand.Append(" sa.NgayTaiLen NgayTaiLen, ");
            SqlCommand.Append(" sa.TrangThai TrangThai, ");
            SqlCommand.Append(" sa.SoLuong SoLuong, ");
            SqlCommand.Append(" sa.MaTL MaTL, ");
            SqlCommand.Append(" sa.MaTG MaTG, ");
            SqlCommand.Append(" sa.MaNXB MaNXB ");
            SqlCommand.Append(" FROM Sach sa ");
            SqlCommand.Append(" WHERE 1=1 ");

            if (!String)  //Tu khoa
            {
                SqlCommand.Append(" and TenS like N'%" + searchString + "%'");
                SqlCommand.Append(" or MoTa like N'%" + searchString + "%'");
            }

            //Tim kiem theo ngay
            if (!TuNgay && DenNgay)
            {
                DateTime dtTuNgay = DateTime.Parse(searchTuNgay);
                string strTuNgay = dtTuNgay.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgayTaiLen >= '" + strTuNgay + "'");
            }
            if (!DenNgay && TuNgay)
            {
                DateTime dtDenNgay = DateTime.Parse(searchDenNgay);
                string strDenNgay = dtDenNgay.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgayTaiLen <= '" + strDenNgay + "'");
            }
            if (!TuNgay && !DenNgay)
            {
                DateTime dtTuNgay = DateTime.Parse(searchTuNgay);
                DateTime dtDenNgay = DateTime.Parse(searchDenNgay);

                string strTuNgaySinh = dtTuNgay.ToString("yyyy/MM/dd");
                string strDenNgaySinh = dtDenNgay.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgayTaiLen >= '" + strTuNgaySinh + "'" + " and NgayTaiLen <= '" + strDenNgaySinh + "'");
            }

            //Tim kiem theo Gia
            if (!TuGia && DenGia)
            {
                float fTuGia = float.Parse(searchTuGia);
                SqlCommand.Append(" and GiaBan >= " + fTuGia);
            }
            if (!DenGia && TuGia)
            {
                float fDenGia = float.Parse(searchDenGia);
                SqlCommand.Append(" and GiaBan <= " + fDenGia);
            }
            if (!TuGia && !DenGia)
            {
                float fTuGia = float.Parse(searchTuGia);
                float fDenGia = float.Parse(searchDenGia);

                SqlCommand.Append(" and GiaBan >= " + fTuGia + " and GiaBan <= " + fDenGia);
            }

            //Tim kiem theo so luong
            if (!TuSL && DenSL)
            {
                float fTuSL = float.Parse(searchTuSL);
                SqlCommand.Append(" and SoLuong >= " + fTuSL);
            }
            if (!DenSL && TuSL)
            {
                float fDenSL = float.Parse(searchDenSL);
                SqlCommand.Append(" and SoLuong <= " + fDenSL);
            }
            if (!TuSL && !DenSL)
            {
                float fTuSL = float.Parse(searchTuSL);
                float fDenSL = float.Parse(searchDenSL);

                SqlCommand.Append(" and SoLuong >= " + fTuSL + " and SoLuong <= " + fDenSL);
            }

            if (!sTheLoai)  // tim theo the loai
            {
                SqlCommand.Append(" and MaTL = N'" + searchTheLoai + "'" + "and sa.MaTL in(select MaTL from TheLoai tl)");
            }

            if (!sTacGia)  // tim theo tac gia
            {
                SqlCommand.Append(" and MaTG = N'" + searchTacGia + "'" + "and sa.MaTG in(select MaTG from TacGia tg)");
            }

            if (!sNhaXuatBan)  // tim theo nha xuat ban
            {
                SqlCommand.Append(" and MaNXB = N'" + searchNhaXuatBan + "'" + "and sa.MaNXB in(select MaNXB from NhaXuatBan nxb)");
            }
            var lst = model.Database.SqlQuery<Sach>("" + SqlCommand)
                .ToPagedList<Sach>(PageNum, PageSize);    // phan trang thay cho limit va offset trong sql
            return lst;
        }
    }
}