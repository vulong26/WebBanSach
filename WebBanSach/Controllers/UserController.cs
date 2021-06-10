using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;
using PagedList;
using PagedList.Mvc;

namespace WebBanSach.Controllers
{
    public class UserController : Controller
    {
        EBookModel db = new EBookModel();
        // GET: User
        public ActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(db.Saches.ToList().OrderBy(n=>n.MaS).ToPagedList(pageNumber,pageSize));
        }

        public ViewResult DetailsBook(string MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaS == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            ViewBag.TenChuDe = db.TheLoais.Single(n => n.MaTL == sach.MaTL).TenTL;
            ViewBag.TenNXB = db.NhaXuatBans.Single(n => n.MaNXB == sach.MaNXB).TenNXB;
            ViewBag.TenTacGia = db.TacGias.Single(n => n.MaTG == sach.MaTG).TenTG;
            return View(sach);
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
       
        public ActionResult DangKy(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ThongBao("Đăng ký thành công!!");
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ThongBao("Đăng ký thất bại"); 
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f.Get("txtMatKhau").ToString();
            KHACHHANG tk = new KHACHHANG();
            KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Đăng nhập thành công!!";
                Session["TaiKhoan"] = kh;
                return RedirectToAction("Index");
            }
            else if (sTaiKhoan == null && sMatKhau == null)
            {
                ModelState.AddModelError("", "Hãy nhập tên đăng nhập và mật khẩu");
            }
            else if (sTaiKhoan != null && sMatKhau == null)
            {
                ModelState.AddModelError("", "Hãy nhập mật khẩu");
            }
            else if (sTaiKhoan == null && sMatKhau != null)
            {
                ModelState.AddModelError("", "Hãy nhập tên đăng nhập");
            }
            ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            
            return RedirectToAction("Index");
            
        }
    }
}