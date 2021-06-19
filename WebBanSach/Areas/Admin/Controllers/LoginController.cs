using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Areas.Admin.Code;
using WebBanSach.Areas.Admin.Models.DAO;
using WebBanSach.Areas.Admin.Models.DTO;
using WebBanSach.Areas.Admin.Models.Entities;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private BookModel db = new BookModel();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["username"] = null;
            return Redirect("Login");
        }
        public ActionResult AdminPage()
        {    
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TaiKhoanDTO tk)
        {
            var dao = new TaiKhoanDAO();
            if (tk.TenDangNhap != null && tk.MatKhau != null)
            {
                var result = dao.Login(tk.TenDangNhap, tk.MatKhau);
                if (result && ModelState.IsValid)
                {
                    Session["username"] = tk.TenDangNhap;
                    SessionHelper.SetSession(new UserSession() { MaTK = tk.MaTK });
                    return RedirectToAction("AdminPage", "Login");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }
            }
            else if (tk.TenDangNhap == null && tk.MatKhau == null)
            {
                ModelState.AddModelError("", "Hãy nhập tên đăng nhập và mật khẩu");
            }
            else if (tk.TenDangNhap != null && tk.MatKhau == null)
            {
                ModelState.AddModelError("", "Hãy nhập mật khẩu");
            }
            else if (tk.TenDangNhap == null && tk.MatKhau != null)
            {
                ModelState.AddModelError("", "Hãy nhập tên đăng nhập");
            }
            return View(tk);
        }

        //public ActionResult DoiMatKhau(string TenDangNhap)
        //{
        //    if(TenDangNhap == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TaiKhoan taiKhoan = db.TaiKhoans.Find(TenDangNhap);
        //    if(taiKhoan == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(taiKhoan);
        //}

        //[HttpPost]
        //public ActionResult DoiMatKhau([Bind(Include = "TenDangNhap,MatKhau,MatKhauMoi")] TaiKhoan tk)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        TaiKhoanDAO dao = new TaiKhoanDAO();
        //        dao.UpdateTaiKhoan(tk);
        //        return RedirectToAction("Login");
        //    }
        //    return View(tk);
        //}
    }
}