using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Areas.Admin.Models.DAO;
using WebBanSach.Areas.Admin.Models.Entities;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class NhaXuatBanController : Controller
    {
        private BookModel db = new BookModel();
        // GET: Admin/NhaXuatBan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NhaXuatBan(string searchString, string searchSDT, string searchDC, int PageNum = 1, int PageSize = 1000)
        {
            //NhaXuatBanDAO dao = new NhaXuatBanDAO();
            //ViewBag.SearchString = searchString;
            //return View(dao.ListNhaXuatBanPage(searchString, searchSDT, searchDC, PageNum, PageSize));
            ViewBag.SearchString = searchString;
            ViewBag.SearchSDT = searchSDT;
            ViewBag.SearchDC = searchDC;
            NhaXuatBanDAO dao = new NhaXuatBanDAO();
            return View(dao.ListNhaXuatBanPage(searchString, searchSDT, searchDC, PageNum, PageSize));
        }
         public ActionResult Delete(int MaNXB)
        {
            NhaXuatBanDAO dao = new NhaXuatBanDAO();
            dao.Delete(MaNXB);
            return Redirect("~/Admin/NhaXuatBan/NhaXuatBan");
        }

        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Edit(int MaNXB)
        {
            List<NhaXuatBan> ls = new List<NhaXuatBan>();
            NhaXuatBanDAO nxbDao = new NhaXuatBanDAO();
            ViewBag.nxb = nxbDao.getByMaNXB(MaNXB);
            return View();
        }

        [HttpPost]
        public ActionResult Add(string TenNXB, string SDT, string DiaChi)
        {
            NhaXuatBanDAO dao = new NhaXuatBanDAO();
            NhaXuatBan nhaxuatban = new NhaXuatBan();
            nhaxuatban.TenNXB = TenNXB;
            nhaxuatban.SDT = SDT;
            nhaxuatban.DiaChi = DiaChi;
            if (ModelState.IsValid)
            {
                dao.Add(nhaxuatban);
                return RedirectToAction("NhaXuatBan");
            }
            else
            {
                return View(nhaxuatban);
            }
        }

        [HttpPost]
        public ActionResult Edit(int MaNXB, string TenNXB, string SDT, string DiaChi)
        {
            NhaXuatBanDAO dao = new NhaXuatBanDAO();
            NhaXuatBan nhaxuatban = dao.getByMaNXB(MaNXB);
            nhaxuatban.TenNXB = TenNXB;
            nhaxuatban.SDT = SDT;
            nhaxuatban.DiaChi = DiaChi;
            if (ModelState.IsValid)
            {
                dao.Edit(nhaxuatban);
                return RedirectToAction("NhaXuatBan");
            }
            else
            {
                return View(nhaxuatban);
            }
        }
        public ActionResult ChiTiet(int MaNXB)
        {
            List<NhaXuatBan> ls = new List<NhaXuatBan>();
            NhaXuatBanDAO nxbDao = new NhaXuatBanDAO();
            ViewBag.nxb = nxbDao.getByMaNXB(MaNXB);
            return View();
        }
        [HttpPost]
        public ActionResult ChiTiet(int MaNXB, string TenNXB, string SDT, string DiaChi)
        {
            NhaXuatBanDAO dao = new NhaXuatBanDAO();
            NhaXuatBan nhaxuatban = dao.getByMaNXB(MaNXB);
            return View(nhaxuatban);
        }
    }
}