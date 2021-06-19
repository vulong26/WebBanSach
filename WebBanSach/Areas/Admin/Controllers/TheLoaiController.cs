using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Areas.Admin.Models.DAO;
using WebBanSach.Areas.Admin.Models.Entities;
using System.IO;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class TheLoaiController : Controller
    {
        private BookModel db = new BookModel();
        // GET: Admin/TheLoai
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TheLoai(string searchString,int PageNum = 1, int PageSize = 1000)
        {
            TheLoaiDAO dao = new TheLoaiDAO();
            ViewBag.SearchString = searchString;
            return View(dao.ListTheLoaiPage(searchString,PageNum, PageSize));
        }
        public ActionResult Delete(int MaTL)
        {
            TheLoaiDAO dao = new TheLoaiDAO();
            dao.Delete(MaTL);
            return Redirect("~/Admin/TheLoai/TheLoai");
        }

        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Edit(int MaTL)
        {
            List<TheLoai> ls = new List<TheLoai>();
            TheLoaiDAO tlDao = new TheLoaiDAO();
            ViewBag.tl = tlDao.getByMaTL(MaTL);
            return View();
        }
        [HttpPost]
        public ActionResult Add(string TenTL, string MoTa)
        {
            TheLoaiDAO dao = new TheLoaiDAO();
            TheLoai theloai = new TheLoai();
            theloai.TenTL = TenTL;
            theloai.MoTa = MoTa;
            if (ModelState.IsValid)
            {
                dao.Add(theloai);
                return RedirectToAction("TheLoai");
            }
            else
            {
                return View(theloai);
            }
        }

        [HttpPost]
        public ActionResult Edit(int MaTL, string TenTL, string MoTa)
        {
            TheLoaiDAO dao = new TheLoaiDAO();
            TheLoai theloai = dao.getByMaTL(MaTL);
            theloai.TenTL = TenTL;
            theloai.MoTa = MoTa;
            if (ModelState.IsValid)
            {
                dao.Edit(theloai);
                return RedirectToAction("TheLoai");
            }
            else
            {
                return View(theloai);
            }
        }
        public ActionResult ChiTiet(int MaTL)
        {
            List<TheLoai> ls = new List<TheLoai>();
            TheLoaiDAO tlDao = new TheLoaiDAO();
            ViewBag.tl = tlDao.getByMaTL(MaTL);
            return View();
        }

        [HttpPost]
        public ActionResult ChiTiet(int MaTL, string TenTL)
        {
            TheLoaiDAO dao = new TheLoaiDAO();
            TheLoai theloai = dao.getByMaTL(MaTL);
            return View(theloai);
        }
    }
}