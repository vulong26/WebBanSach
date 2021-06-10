using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class TimKiemController : Controller
    {
        EBookModel db = new EBookModel();
        // GET: TimKiem
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f,int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            List<Sach> lstKQTK = db.Saches.Where(n => n.TenS.Contains(sTuKhoa)).ToList();
            ViewBag.TuKhoa = sTuKhoa;
            
            int pageNum = (page ?? 1);
            int pageSize = 8;
            if (lstKQTK.Count == 0)
            {
               
                return View(db.Saches.OrderBy(n => n.TenS).ToPagedList(pageNum,pageSize));

            }
            ViewBag.ThongBao = "Đã tìm thấy" + lstKQTK.Count + "kết quả";
            return View(lstKQTK.OrderBy(n => n.TenS).ToPagedList(pageNum, pageSize));

            
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(string sTuKhoa , int? page)
        {
            
            List<Sach> lstKQTK = db.Saches.Where(n => n.TenS.Contains(sTuKhoa)).ToList();


            int pageNum = (page ?? 1);
            int pageSize = 8;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không có kết quả nào";
                return View(db.Saches.OrderBy(n => n.TenS).ToPagedList(pageNum, pageSize));

            }
            ViewBag.ThongBao = "Đã tìm thấy" + lstKQTK.Count + "kết quả";
            return View(lstKQTK.OrderBy(n => n.TenS).ToPagedList(pageNum, pageSize));


        }

    }
}