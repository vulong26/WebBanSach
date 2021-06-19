using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebBanSach.Areas.Admin.Models.Entities;

namespace WebBanSach.Areas.Admin.Models.DAO
{
    public class BoPhanDAO
    {
        BookModel model;
        public BoPhanDAO()
        {
            model = new BookModel();
        }
        public List<BoPhan> ListBoPhan()
        {
            return model.BoPhans.ToList();
        }
        public List<BoPhan> ListBoPhan(int Pagenum, int PageSize)
        {
            return model.BoPhans.Skip((Pagenum - 1) * PageSize).Take(PageSize).ToList();
        }
        public void Add(BoPhan bp)
        {
            model.BoPhans.Add(bp);
            model.SaveChanges();
        }
        public void Edit(BoPhan bp)
        {
            BoPhan bophan = getByMaBP(bp.MaBP);
            if (bophan != null)
            {
                bophan.TenBP = bp.TenBP;
                //bophan.MoTa = bp.MoTa;
                model.SaveChanges();
            }
        }
        public int Delete(int MaBP)
        {
            BoPhan bp = model.BoPhans.Find(MaBP);
            if (bp != null)
            {
                model.BoPhans.Remove(bp);
                return model.SaveChanges();
            }
            else
                return -1;
        }



        public BoPhan getByMaBP(int MaBP)
        {
            return model.BoPhans.Single(i => i.MaBP == MaBP);
        }
        public IEnumerable<BoPhan> ListBoPhanPage(string searchString, int PageNum, int PageSize) // neu muon tim kiem thi them string key 
        {
            bool a = string.IsNullOrEmpty(searchString);
            //bool b = string.IsNullOrEmpty(searchSDT);
            //bool c = string.IsNullOrEmpty(searchDC);
            StringBuilder SqlCommand = new StringBuilder();
            SqlCommand.Append(" SELECT ");
            SqlCommand.Append(" bp.MaBP MaBP, ");
            SqlCommand.Append(" bp.TenBP TenBP ");
            //SqlCommand.Append(" nxb.SDT SDT, ");
            //SqlCommand.Append(" nxb.DiaChi DiaChi ");
            SqlCommand.Append(" FROM BoPhan bp ");
            SqlCommand.Append(" WHERE 1=1 ");
            if (!a)
            {
                SqlCommand.Append(" and TenBP like N'%" + searchString + "%'");
            }
            //if (!b)
            //{
            //    SqlCommand.Append(" and SDT like N'%" + searchSDT + "%'");
            //}
            //if (!c)
            //{
            //    SqlCommand.Append(" and DiaChi like N'%" + searchDC + "%'");
            //}
            var lst = model.Database.SqlQuery<BoPhan>("" + SqlCommand)
                .ToPagedList<BoPhan>(PageNum, PageSize);    // phan trang thay cho limit va offset trong sql
            return lst;
        }
    }
}