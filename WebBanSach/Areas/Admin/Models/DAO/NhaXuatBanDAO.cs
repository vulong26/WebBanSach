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
    public class NhaXuatBanDAO
    {
        BookModel model;
        public NhaXuatBanDAO()
        {
            model = new BookModel();
        }
        public List<NhaXuatBan> ListNhaXuatBan()
        {
            return model.NhaXuatBans.ToList();
        }
        public List<NhaXuatBan> ListNhaXuatBan(int Pagenum, int PageSize)
        {
            return model.NhaXuatBans.Skip((Pagenum - 1) * PageSize).Take(PageSize).ToList();
        }
        public void Add(NhaXuatBan nxb)
        {
            model.NhaXuatBans.Add(nxb);
            model.SaveChanges();
        }
        public void Edit(NhaXuatBan nxb)
        {
            NhaXuatBan NhaXuatBan = getByMaNXB(nxb.MaNXB);
            if (NhaXuatBan != null)
            {
                NhaXuatBan.TenNXB = nxb.TenNXB;
                NhaXuatBan.SDT = nxb.SDT;
                NhaXuatBan.DiaChi = nxb.DiaChi;
                model.SaveChanges();
            }
        }
        public int Delete(int MaNXB)
        {
            NhaXuatBan nxb = model.NhaXuatBans.Find(MaNXB);
            if (nxb != null)
            {
                model.NhaXuatBans.Remove(nxb);
                return model.SaveChanges();
            }
            else
                return -1;
        }



        public NhaXuatBan getByMaNXB(int MaNXB)
        {
            return model.NhaXuatBans.FirstOrDefault(i => i.MaNXB == MaNXB);
        }
        //public IEnumerable<NhaXuatBan> ListNhaXuatBanPage(string searchString, string searchSDT, string searchDC, int PageNum, int PageSize) // neu muon tim kiem thi them string key 
        //{
        //    IQueryable<NhaXuatBan> lst = model.NhaXuatBans;
        //    bool a = string.IsNullOrEmpty(searchString);
        //    bool b = string.IsNullOrEmpty(searchSDT);
        //    bool c = string.IsNullOrEmpty(searchDC);
        //    // Từ khóa
        //    if(a && b && c)
        //    {
        //        lst = model.NhaXuatBans;
        //    }    
        //    else if (!a)
        //    {
        //        lst = lst.Where(x => x.TenNXB.Contains(searchString));
        //    }
        //    else if (!b)
        //    {
        //        lst = lst.Where(x => x.SDT.Contains(searchSDT));
        //    }
        //    else if (!c)
        //    {
        //        lst = lst.Where(x => x.DiaChi.Contains(searchDC));
        //    }
        //    else if(!a && !b)
        //    {
        //        lst = lst.Where(x => x.TenNXB.Contains(searchString) && x.SDT.Contains(searchSDT));
        //    }
        //    else if (!a && !c)
        //    {
        //        lst = lst.Where(x => x.TenNXB.Contains(searchString) && x.DiaChi.Contains(searchDC));
        //    }
        //    else if (!b && !c)
        //    {
        //        lst = lst.Where(x => x.SDT.Contains(searchSDT) && x.DiaChi.Contains(searchDC));
        //    }
        //    else 
        //    {
        //        lst = lst.Where(x => x.TenNXB.Contains(searchString) && x.SDT.Contains(searchSDT) && x.DiaChi.Contains(searchDC));
        //    }
        //    return lst.OrderByDescending(x => x.MaNXB).ToPagedList(PageNum, PageSize);
        //}
        public IEnumerable<NhaXuatBan> ListNhaXuatBanPage(string searchString, string searchSDT, string searchDC, int PageNum, int PageSize) // neu muon tim kiem thi them string key 
        {
            bool a = string.IsNullOrEmpty(searchString);
            bool b = string.IsNullOrEmpty(searchSDT);
            bool c = string.IsNullOrEmpty(searchDC);
            StringBuilder SqlCommand = new StringBuilder();
            SqlCommand.Append(" SELECT ");
            SqlCommand.Append(" nxb.MaNXB MaNXB, ");
            SqlCommand.Append(" nxb.TenNXB TenNXB, ");
            SqlCommand.Append(" nxb.SDT SDT, ");
            SqlCommand.Append(" nxb.DiaChi DiaChi ");
            SqlCommand.Append(" FROM NhaXuatBan nxb ");
            SqlCommand.Append(" WHERE 1=1 ");
            if(!a)
            {
                SqlCommand.Append(" and TenNXB like N'%"+searchString+"%'");
            }
            if (!b)
            {
                SqlCommand.Append(" and SDT like N'%"+searchSDT+"%'");
            }
            if (!c)
            {
                SqlCommand.Append(" and DiaChi like N'%"+searchDC+"%'");
            }
            var lst = model.Database.SqlQuery<NhaXuatBan>(""+SqlCommand)
                .ToPagedList<NhaXuatBan>(PageNum, PageSize);    // phan trang thay cho limit va offset trong sql
            return lst;
        }
        //public bool Login(string TenDangNhap, string MatKhau)
        //{
        //    object[] sqlParams = new SqlParameter[]
        //    {
        //    new SqlParameter("TenDangNhap",TenDangNhap),
        //    new SqlParameter("MatKhau", MatKhau),
        //    };
        //    var res = db.Database.SqlQuery<bool>("DangNhapAdmin @TenDangNhap,@MatKhau", sqlParams).SingleOrDefault();
        //    return res;
        //}
    }
}