using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Areas.Admin.Models.DAO;
using WebBanSach.Areas.Admin.Models.Entities;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class BoPhanController : Controller
    {
        private BookModel db = new BookModel();
        // GET: Admin/BoPhan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BoPhan(string searchString, int PageNum = 1, int PageSize = 1000)
        {
            ViewBag.SearchString = searchString;
            //ViewBag.SearchSDT = searchSDT;
            //ViewBag.SearchDC = searchDC;
            BoPhanDAO dao = new BoPhanDAO();
            return View(dao.ListBoPhanPage(searchString, PageNum, PageSize));
        }

        public ActionResult Delete(int MaBP)
        {
            BoPhanDAO dao = new BoPhanDAO();
            dao.Delete(MaBP);
            return Redirect("~/Admin/BoPhan/BoPhan");
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int MaBP)
        {
            List<BoPhan> ls = new List<BoPhan>();
            BoPhanDAO bpDao = new BoPhanDAO();
            ViewBag.bp = bpDao.getByMaBP(MaBP);
            return View();
        }
        [HttpPost]
        public ActionResult Add(string TenBP)
        {
            BoPhanDAO dao = new BoPhanDAO();
            BoPhan bophan = new BoPhan();
            bophan.TenBP = TenBP;
            //nhaxuatban.SDT = SDT;
            //nhaxuatban.DiaChi = DiaChi;
            if (ModelState.IsValid)
            {
                dao.Add(bophan);
                return RedirectToAction("BoPhan");
            }
            else
            {
                return View(bophan);
            }
        }
        [HttpPost]
        public ActionResult Edit(int MaBP, string TenBP)
        {
            BoPhanDAO dao = new BoPhanDAO();
            BoPhan bophan = dao.getByMaBP(MaBP);
            bophan.TenBP = TenBP;
            //nhaxuatban.SDT = SDT;
            //nhaxuatban.DiaChi = DiaChi;
            if (ModelState.IsValid)
            {
                dao.Edit(bophan);
                return RedirectToAction("BoPhan");
            }
            else
            {
                return View(bophan);
            }
        }

        public ActionResult ChiTiet(int MaBP)
        {
            List<BoPhan> ls = new List<BoPhan>();
            BoPhanDAO bpDao = new BoPhanDAO();
            ViewBag.bp = bpDao.getByMaBP(MaBP);
            return View();
        }
        
        [HttpPost]
        public ActionResult ChiTiet(int MaBP, string TenBP)
        {
            BoPhanDAO dao = new BoPhanDAO();
            BoPhan bophan = dao.getByMaBP(MaBP);
            return View(bophan);
        }
    }
}