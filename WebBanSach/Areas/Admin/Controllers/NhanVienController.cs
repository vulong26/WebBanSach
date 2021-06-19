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
    public class NhanVienController : Controller
    {
        private BookModel db = new BookModel();
        // GET: Admin/NhanVien
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NhanVien(string searchString, string searchSDT, string searchDC, string searchGioiTinh, string searchTuNgaySinh,
            string searchDenNgaySinh, string searchTuLuong, string searchDenLuong, string searchBoPhan, int PageNum = 1, int PageSize = 1000)
        {
            ViewBag.BoPhan = db.BoPhans;

            ViewBag.SearchString = searchString;
            ViewBag.SearchSDT = searchSDT;
            ViewBag.SearchDC = searchDC;
            ViewBag.SearchGioiTinh = searchGioiTinh;
            ViewBag.SearchTuNgaySinh = searchTuNgaySinh;
            ViewBag.SearchDenNgaySinh = searchDenNgaySinh;
            ViewBag.SearchTuLuong = searchTuLuong;
            ViewBag.SearchDenLuong = searchDenLuong;
            ViewBag.SearchBoPhan = searchBoPhan;
            NhanVienDAO dao = new NhanVienDAO();
            return View(dao.ListNhanVienPage(searchString, searchSDT, searchDC, searchGioiTinh, searchTuNgaySinh, searchDenNgaySinh,
                searchTuLuong, searchDenLuong, searchBoPhan, PageNum, PageSize));
        }
        public ActionResult Delete(int MaNV)
        {
            NhanVienDAO dao = new NhanVienDAO();
            dao.Delete(MaNV);
            return Redirect("~/Admin/NhanVien/NhanVien");
        }

        public ActionResult Add()
        {
            ViewBag.BoPhan = db.BoPhans;
            return View();
        }

        public ActionResult Edit(int MaNV)
        {
            ViewBag.BoPhan = db.BoPhans;
            List<NhanVien> ls = new List<NhanVien>();
            BoPhanDAO dao = new BoPhanDAO();
            NhanVienDAO nvDao = new NhanVienDAO();
            ViewBag.bp = dao.ListBoPhan();
            ViewBag.nv = nvDao.getByMaNV(MaNV);
            return View();
        }
        [HttpPost]
        public ActionResult Add(string TenNV, string SDT, string DiaChi, string GioiTinh, string NgaySinh, string Luong, string MaBP, HttpPostedFileBase Anh)
        {
            ViewBag.BoPhan = db.BoPhans;
            
            NhanVien nhanvien = new NhanVien();
            nhanvien.TenNV = TenNV;
            nhanvien.SDT = SDT;
            nhanvien.DiaChi = DiaChi;
            nhanvien.GioiTinh = GioiTinh;
            string ns = nhanvien.NgaySinh.ToString();
            if (NgaySinh != null)
            {
                nhanvien.NgaySinh = DateTime.Parse(NgaySinh);
            }
            else if (NgaySinh == null)
            {
                nhanvien.NgaySinh = DateTime.Parse(ns);
            }
            if (Luong != "")
            {
                nhanvien.Luong = Int32.Parse(Luong);
            }
            else if (Luong == "")
            {
                nhanvien.Luong = 0;
            }
            nhanvien.MaBP = Int32.Parse(MaBP);
            if (ModelState.IsValid)
            {
                if (Anh != null && Anh.ContentLength > 0)
                {
                    var img = Path.GetFileName(Anh.FileName);
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photo/"),
                                            System.IO.Path.GetFileName(Anh.FileName));
                    Anh.SaveAs(path);

                    nhanvien.Anh = Anh.FileName;
                    NhanVienDAO dao = new NhanVienDAO();
                    dao.Add(nhanvien);
                }
                else if (Anh == null)
                {
                    var img = Path.GetFileName("icons8-no-user-alt-96.png");
                    nhanvien.Anh = "icons8-no-user-alt-96.png";
                    NhanVienDAO dao = new NhanVienDAO();
                    dao.Add(nhanvien);
                }
                return RedirectToAction("NhanVien");
            }
            else
            {
                return View(nhanvien);
            }
        }
        [HttpPost]
        public ActionResult Edit(int MaNV, string TenNV, string SDT, string DiaChi, string GioiTinh, string NgaySinh, string Luong, string MaBP, HttpPostedFileBase Anh)
        {
            ViewBag.BoPhan = db.BoPhans;
            
            NhanVienDAO dao = new NhanVienDAO();
            NhanVien nhanvien = dao.getByMaNV(MaNV);
            string photo = nhanvien.Anh;
            nhanvien.TenNV = TenNV;
            nhanvien.SDT = SDT;
            nhanvien.DiaChi = DiaChi;
            nhanvien.GioiTinh = GioiTinh;
            string str = nhanvien.NgaySinh.ToString("dd/MM/yyyy");
            string ns = nhanvien.NgaySinh.ToString();
            if(NgaySinh != null && NgaySinh != str)
            {
                nhanvien.NgaySinh = DateTime.Parse(NgaySinh);
            } 
            else if(NgaySinh != null && NgaySinh == str)
            {
                nhanvien.NgaySinh = DateTime.Parse(ns);
            }    
            else if (NgaySinh == null)
            {
                nhanvien.NgaySinh = DateTime.Parse(ns);
            }    
            if(Luong != "")
            {
                nhanvien.Luong = Int32.Parse(Luong);
            }    
            else if(Luong == "")
            {
                nhanvien.Luong = 0;
            }    
            nhanvien.MaBP = Int32.Parse(MaBP);
            if (ModelState.IsValid)
            {
                if (Anh != null && Anh.ContentLength > 0)
                {
                    var img = Path.GetFileName(Anh.FileName);
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photo/"),
                                            System.IO.Path.GetFileName(Anh.FileName));
                    Anh.SaveAs(path);

                    nhanvien.Anh = Anh.FileName;
                    dao.Edit(nhanvien);
                }
                else if (Anh == null)
                {
                    var img = Path.GetFileName("icons8-no-user-alt-96.png");
                    nhanvien.Anh = photo;
                    dao.Edit(nhanvien);
                }
                return RedirectToAction("NhanVien");
            }
            else
            {
                return View(nhanvien);
            }
        }

        public ActionResult ChiTiet(int MaNV)
        {
            BoPhanDAO dao = new BoPhanDAO();
            List<NhanVien> ls = new List<NhanVien>();
            NhanVienDAO nvDao = new NhanVienDAO();
            ViewBag.nv = nvDao.getByMaNV(MaNV);
            ViewBag.bp = dao.ListBoPhan();
            return View();
        }
        [HttpPost]
        public ActionResult ChiTiet(int MaNV, string TenNV, string SDT, string DiaChi, string GioiTinh, string NgaySinh, string Luong, string MaBP, HttpPostedFileBase Anh)
        {
            NhanVienDAO dao = new NhanVienDAO();
            NhanVien nhanvien = dao.getByMaNV(MaNV);
            return View(nhanvien);
        }
    }
}