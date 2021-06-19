using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Areas.Admin.Models.DAO;
using WebBanSach.Areas.Admin.Models.Entities;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class TacGiaController : Controller
    {
        // GET: Admin/TacGia
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TacGia(string searchString, string searchGioiTinh, string searchTuNgaySinh,
            string searchDenNgaySinh, string searchTuSoTacPham, string searchDenSoTacPham, int PageNum = 1, int PageSize = 1000)
        {
            ViewBag.SearchString = searchString;
            ViewBag.SearchGioiTinh = searchGioiTinh;
            ViewBag.SearchTuNgaySinh = searchTuNgaySinh;
            ViewBag.SearchDenNgaySinh = searchDenNgaySinh;
            ViewBag.SearchTuSoTacPham = searchTuSoTacPham;
            ViewBag.SearchDenSoTacPham = searchDenSoTacPham;
            TacGiaDAO dao = new TacGiaDAO();
            return View(dao.ListTacGiaPage(searchString, searchGioiTinh, searchTuNgaySinh, searchDenNgaySinh,
                searchTuSoTacPham, searchDenSoTacPham, PageNum, PageSize));
        }

        public ActionResult Delete(int MaTG)
        {
            TacGiaDAO dao = new TacGiaDAO();
            dao.Delete(MaTG);
            return Redirect("~/Admin/TacGia/TacGia");
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int MaTG)
        {
            List<TacGia> ls = new List<TacGia>();
            TacGiaDAO tgDao = new TacGiaDAO();
            ViewBag.tg = tgDao.getByMaTG(MaTG);
            return View();
        }
        public ActionResult ChiTiet(int MaTG)
        {
            List<TacGia> ls = new List<TacGia>();
            TacGiaDAO tgDao = new TacGiaDAO();
            ViewBag.tg = tgDao.getByMaTG(MaTG);
            return View();
        }
        [HttpPost]
        public ActionResult Add(string TenTG, string MoTa, string GioiTinh, string NgaySinh, string SoTacPham, HttpPostedFileBase Image)
        {            
            TacGia tacgia = new TacGia();
            tacgia.TenTG = TenTG;
            tacgia.MoTa = MoTa;
            tacgia.GioiTinh = GioiTinh;
            string str = tacgia.NgaySinh.ToString("dd/MM/yyyy");
            string ns = tacgia.NgaySinh.ToString();
            if (NgaySinh != null && NgaySinh != str)
            {
                tacgia.NgaySinh = DateTime.Parse(NgaySinh);
            }
            else if (NgaySinh != null && NgaySinh == str)
            {
                tacgia.NgaySinh = DateTime.Parse(ns);
            }
            else if (NgaySinh == null)
            {
                tacgia.NgaySinh = DateTime.Parse(ns);
            }
            if (SoTacPham != "")
            {
                tacgia.SoTacPham = Int32.Parse(SoTacPham);
            }    
            else if(SoTacPham == "")
            {
                tacgia.SoTacPham = 0;
            }    
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    var img = Path.GetFileName(Image.FileName);
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photo/"),
                                            System.IO.Path.GetFileName(Image.FileName));
                    Image.SaveAs(path);

                    tacgia.Image = Image.FileName;
                    TacGiaDAO dao = new TacGiaDAO();
                    dao.Add(tacgia);
                }
                else if (Image == null)
                {
                    var img = Path.GetFileName("icons8-no-user-alt-96.png");                   
                    tacgia.Image = "icons8-no-user-alt-96.png";
                    TacGiaDAO dao = new TacGiaDAO();
                    dao.Add(tacgia);
                }    
                return RedirectToAction("TacGia");
            }
            else
            {
                return View(tacgia);
            }
        }
        [HttpPost]
        public ActionResult Edit(int MaTG, string TenTG, string MoTa, string GioiTinh, string NgaySinh, string SoTacPham, HttpPostedFileBase Image)
        {           
            TacGiaDAO dao = new TacGiaDAO();
            TacGia tacgia = dao.getByMaTG(MaTG);
            string photo = tacgia.Image;
            tacgia.TenTG = TenTG;
            tacgia.MoTa = MoTa;
            tacgia.GioiTinh = GioiTinh;
            string str = tacgia.NgaySinh.ToString("dd/MM/yyyy");
            string ns = tacgia.NgaySinh.ToString();
            if (NgaySinh != null && NgaySinh != str)
            {
                tacgia.NgaySinh = DateTime.Parse(NgaySinh);
            }
            else if (NgaySinh != null && NgaySinh == str)
            {
                tacgia.NgaySinh = DateTime.Parse(ns);
            }
            else if (NgaySinh == null)
            {
                tacgia.NgaySinh = DateTime.Parse(ns);
            }
            if (SoTacPham != "")
            {
                tacgia.SoTacPham = Int32.Parse(SoTacPham);
            }
            else if (SoTacPham == "")
            {
                tacgia.SoTacPham = 0;
            }
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    var img = Path.GetFileName(Image.FileName);
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photo/"),
                                            System.IO.Path.GetFileName(Image.FileName));
                    Image.SaveAs(path);

                    tacgia.Image = Image.FileName;
                    dao.Edit(tacgia);
                }
                else if (Image == null)
                {
                    var img = Path.GetFileName("icons8-no-user-alt-96.png");
                    tacgia.Image = photo;
                    dao.Edit(tacgia);
                }
                return RedirectToAction("TacGia");
            }
            else
            {
                return View(tacgia);
            }
        }
        [HttpPost]
        public ActionResult ChiTiet(int MaTG, string TenTG, string MoTa, string GioiTinh, string NgaySinh, string SoTacPham, HttpPostedFileBase Image)
        {
            TacGiaDAO dao = new TacGiaDAO();
            TacGia tacgia = dao.getByMaTG(MaTG);
            return View(tacgia);
        }
    }
}