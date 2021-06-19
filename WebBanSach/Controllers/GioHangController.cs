using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;
namespace WebBanSach.Controllers
{
    public class GioHangController : Controller
    {
        EBookModel db = new EBookModel();
        #region GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;

            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(string iMaS,string sUrl)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaS == iMaS);
            if (sach == null)
            {
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.Find(n => n.iMaS == iMaS);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaS);
                lstGioHang.Add(sanpham);
                return Redirect(sUrl);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(sUrl);
            }
        }
        public ActionResult CapNhatGioHang(string iMaSp, FormCollection f)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaS == iMaSp);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaS == iMaSp);
            if (sanpham  != null)
            {
            
                sanpham.iSoLuong = int.Parse( f["txtSoLuong"].ToString());
                if (sanpham.iSoLuong > sach.SoLuong)
                {
                    return View("ThongBao");
                    sanpham.iSoLuong = 1;
                }
               
            }

            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang(string iMaSp)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaS == iMaSp);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaS == iMaSp);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaS == iMaSp);
                
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "User"); 
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "User");

            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult SuaGH()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "User");

            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
#endregion
        [HttpPost]
        #region DatHang
        public ActionResult DatHang()
        {
            if(Session["TaiKhoan"]==null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "User");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            DonHang donHang = new DonHang();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            donHang.MaKH = kh.MaKH;
            donHang.NgayDat = DateTime.Now;
            db.DonHangs.Add(donHang);
            db.SaveChanges();
            foreach(var item in gh)
            {
                ChiTietDonHang ct = new ChiTietDonHang();
                ct.MaDonHang = donHang.MaDonHang;
                ct.MaS = item.iMaS;
                ct.SoLuong = item.iSoLuong;
                ct.DonGia = item.dDonGia.ToString();
                db.ChiTietDonHangs.Add(ct);
                

            }
            db.SaveChanges();
            return View("View");
            
            
            
            
            return RedirectToAction("Index","User");
        }
        #endregion
    }
}