using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Areas.Admin.Models.DAO;
using WebBanSach.Areas.Admin.Models.Entities;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        private BookModel db = new BookModel();
        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KhachHang(string searchString, string searchSDT, string searchDC, string searchGioiTinh, string searchTuNgaySinh,
            string searchDenNgaySinh, string searchEmail, int PageNum = 1, int PageSize = 1000)
        {
            ViewBag.SearchString = searchString;
            ViewBag.SearchSDT = searchSDT;
            ViewBag.SearchDC = searchDC;
            ViewBag.SearchGioiTinh = searchGioiTinh;
            ViewBag.SearchTuNgaySinh = searchTuNgaySinh;
            ViewBag.SearchDenNgaySinh = searchDenNgaySinh;
            ViewBag.SearchEmail = searchEmail;
            KhachHangDAO dao = new KhachHangDAO();
            return View(dao.ListKhachHangPage(searchString, searchSDT, searchDC, searchGioiTinh, searchTuNgaySinh, searchDenNgaySinh,
                searchEmail, PageNum, PageSize));
        }

        public ActionResult Delete(int MaKH)
        {
            KhachHangDAO dao = new KhachHangDAO();
            dao.Delete(MaKH);
            return Redirect("~/Admin/KhachHang/KhachHang");
        }

        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Edit(int MaKH)
        {
            List<KhachHang> ls = new List<KhachHang>();
            KhachHangDAO khDao = new KhachHangDAO();
            ViewBag.kh = khDao.getByMaKH(MaKH);
            return View();
        }
        public ActionResult ChiTiet(int MaKH)
        {
            List<KhachHang> ls = new List<KhachHang>();
            KhachHangDAO khDao = new KhachHangDAO();
            ViewBag.kh = khDao.getByMaKH(MaKH);
            return View();
        }
        [HttpPost]
        public ActionResult Add(string HoTen, string Email, string DiaChi, string DienThoai, string GioiTinh, string NgaySinh, string TaiKhoan, string MatKhau)
        {
            KhachHangDAO dao = new KhachHangDAO();
            KhachHang khachhang = new KhachHang();
            khachhang.HoTen = HoTen;
            khachhang.Email = Email;
            khachhang.DiaChi = DiaChi;
            khachhang.DienThoai = DienThoai;
            khachhang.GioiTinh = GioiTinh;
            khachhang.NgaySinh = DateTime.Parse(NgaySinh);
            khachhang.TaiKhoan = TaiKhoan;
            khachhang.MatKhau = MatKhau;
            if (ModelState.IsValid)
            {
                dao.Add(khachhang);
                return RedirectToAction("KhachHang");
            }
            else
            {
                return View(khachhang);
            }
        }
        [HttpPost]
        public ActionResult Edit(int MaKH, string HoTen, string Email, string DiaChi, string DienThoai, string GioiTinh, string NgaySinh, string TaiKhoan, string MatKhau)
        {
            KhachHangDAO dao = new KhachHangDAO();
            KhachHang khachhang = dao.getByMaKH(MaKH);
            khachhang.HoTen = HoTen;
            khachhang.Email = Email;
            khachhang.DiaChi = DiaChi;
            khachhang.DienThoai = DienThoai;
            khachhang.GioiTinh = GioiTinh;
            khachhang.NgaySinh = DateTime.Parse(NgaySinh);
            khachhang.TaiKhoan = TaiKhoan;
            khachhang.MatKhau = MatKhau;
            if (ModelState.IsValid)
            {
                dao.Edit(khachhang);
                return RedirectToAction("KhachHang");
            }
            else
            {
                return View(khachhang);
            }
        }

        [HttpPost]
        public ActionResult ChiTiet(int MaKH, string HoTen, string Email, string DiaChi, string DienThoai, string GioiTinh, string NgaySinh, string TaiKhoan, string MatKhau)
        {
            KhachHangDAO dao = new KhachHangDAO();
            KhachHang khachhang = dao.getByMaKH(MaKH);
            return View(khachhang);
        }
    }
}