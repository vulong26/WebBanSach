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
    public class TheLoaiDAO
    {
        BookModel model;
        public TheLoaiDAO()
        {
            model = new BookModel();
        }
        public List<TheLoai> ListTheLoai()
        {
            return model.TheLoais.ToList();
        }
        public List<TheLoai> ListTheLoai(int Pagenum, int PageSize)
        {
            return model.TheLoais.Skip((Pagenum - 1) * PageSize).Take(PageSize).ToList();
        }
        public void Add(TheLoai tl)
        {
            model.TheLoais.Add(tl);
            model.SaveChanges();
        }
        public void Edit(TheLoai tl)
        {
            TheLoai theloai = getByMaTL(tl.MaTL);
            if (theloai != null)
            {
                theloai.TenTL = tl.TenTL;
                theloai.MoTa = tl.MoTa;
                model.SaveChanges();
            }
        }
        public int Delete(int MaTL)
        {
            TheLoai tl = model.TheLoais.Find(MaTL);
            if (tl != null)
            {
                model.TheLoais.Remove(tl);
                return model.SaveChanges();
            }
            else
                return -1;
        }



        public TheLoai getByMaTL(int MaTL)
        {
            return model.TheLoais.Single(i => i.MaTL == MaTL);
        }
        public IEnumerable<TheLoai> ListTheLoaiPage(string searchString, int PageNum, int PageSize) // neu muon tim kiem thi them string key 
        {
            IQueryable<TheLoai> lst = model.TheLoais;
            // Từ khóa
            if (!string.IsNullOrEmpty(searchString))
            {
                lst = lst.Where(x => x.TenTL.Contains(searchString) || x.MoTa.Contains(searchString));
            }
            return lst.OrderByDescending(x => x.MaTL).ToPagedList(PageNum, PageSize);
        }
    }
}