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
    public class SachController : Controller
    {
        private BookModel db = new BookModel();
        // GET: Admin/Sach
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sach(string searchString, string searchTuNgay,
            string searchDenNgay, string searchTuGia, string searchDenGia, string searchTuSL, string searchDenSL,
            string searchTheLoai, string searchTacGia, string searchNhaXuatBan, int PageNum = 1, int PageSize = 1000)
        {
            ViewBag.TheLoai = db.TheLoais;
            ViewBag.TacGia = db.TacGias;
            ViewBag.NhaXuatBan = db.NhaXuatBans;

            ViewBag.SearchString = searchString;
            ViewBag.SearchTuNgay = searchTuNgay;
            ViewBag.SearchDenNgay = searchDenNgay;
            ViewBag.SearchTuGia = searchTuGia;
            ViewBag.SearchDenGia = searchDenGia;
            ViewBag.SearchTuSL = searchTuSL;
            ViewBag.SearchDenSL = searchDenSL;
            ViewBag.SearchTheLoai = searchTheLoai;
            ViewBag.SearchTacGia = searchTacGia;
            ViewBag.SearchNhaXuatBan = searchNhaXuatBan;
            SachDAO dao = new SachDAO();
            return View(dao.ListSachPage(searchString, searchTuNgay, searchDenNgay, searchTuGia, searchDenGia, searchTuSL,
                searchDenSL, searchTheLoai, searchTacGia, searchNhaXuatBan,  PageNum, PageSize));
        }

        public ActionResult Delete(int MaS)
        {
            SachDAO dao = new SachDAO();
            dao.Delete(MaS);
            return Redirect("~/Admin/Sach/Sach");
        }

        public ActionResult Add()
        {
            ViewBag.TheLoai = db.TheLoais;
            ViewBag.TacGia = db.TacGias;
            ViewBag.NhaXuatBan = db.NhaXuatBans;
            return View();
        }
        public ActionResult Edit(int MaS)
        {
            ViewBag.TheLoai = db.TheLoais;
            ViewBag.TacGia = db.TacGias;
            ViewBag.NhaXuatBan = db.NhaXuatBans;

            List<Sach> ls = new List<Sach>();

            TheLoaiDAO tldao = new TheLoaiDAO();
            TacGiaDAO tgdao = new TacGiaDAO();
            NhaXuatBanDAO nxbdao = new NhaXuatBanDAO();

            SachDAO saDao = new SachDAO();

            ViewBag.tl = tldao.ListTheLoai();
            ViewBag.tg = tgdao.ListTacGia();
            ViewBag.nxb = nxbdao.ListNhaXuatBan();

            ViewBag.sa = saDao.getByMaS(MaS);

            return View();
        }

        public ActionResult ChiTiet(int MaS)
        {
            List<Sach> ls = new List<Sach>();

            TheLoaiDAO tldao = new TheLoaiDAO();
            TacGiaDAO tgdao = new TacGiaDAO();
            NhaXuatBanDAO nxbdao = new NhaXuatBanDAO();

            SachDAO saDao = new SachDAO();

            ViewBag.tl = tldao.ListTheLoai();
            ViewBag.tg = tgdao.ListTacGia();
            ViewBag.nxb = nxbdao.ListNhaXuatBan();

            ViewBag.sa = saDao.getByMaS(MaS);

            return View();
        }
        [HttpPost]
        public ActionResult Add(string TenS, string MoTa, string GiaBan, string NgayTaiLen, string TrangThai, 
            string SoLuong, string MaTL, string MaTG, string MaNXB, HttpPostedFileBase TenFileAnh)
        {
            ViewBag.TheLoai = db.TheLoais;
            ViewBag.TacGia = db.TacGias;
            ViewBag.NhaXuatBan = db.NhaXuatBans;

            Sach sach = new Sach();
            sach.TenS = TenS;
            sach.MoTa = MoTa;

            if (GiaBan != "")
            {
                sach.GiaBan = Int32.Parse(GiaBan);
            }
            else if (GiaBan == "")
            {
                sach.GiaBan = 0;
            }

            string ns = sach.NgayTaiLen.ToString();
            if (NgayTaiLen != null)
            {
                sach.NgayTaiLen = DateTime.Parse(NgayTaiLen);
            }
            else if (NgayTaiLen == null)
            {
                sach.NgayTaiLen = DateTime.Parse(ns);
            }

            sach.TrangThai = TrangThai;

            if (SoLuong != "")
            {
                sach.SoLuong = Int32.Parse(SoLuong);
            }
            else if (SoLuong == "")
            {
                sach.SoLuong = 0;
            }

            sach.MaTL = Int32.Parse(MaTL);
            sach.MaTG = Int32.Parse(MaTG);
            sach.MaNXB = Int32.Parse(MaNXB);

            if (ModelState.IsValid)
            {
                if (TenFileAnh != null && TenFileAnh.ContentLength > 0)
                {
                    var img = Path.GetFileName(TenFileAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photo/"),
                                            System.IO.Path.GetFileName(TenFileAnh.FileName));
                    TenFileAnh.SaveAs(path);

                    sach.TenFileAnh = TenFileAnh.FileName;
                    SachDAO dao = new SachDAO();
                    dao.Add(sach);
                }
                else if (TenFileAnh == null)
                {
                    var img = Path.GetFileName("icons8-open-book-100.png");
                    sach.TenFileAnh = "icons8-open-book-100.png";
                    SachDAO dao = new SachDAO();
                    dao.Add(sach);
                }
                return RedirectToAction("Sach");
            }
            else
            {
                return View(sach);
            }
        }
        [HttpPost]
        public ActionResult Edit(int MaS, string TenS, string MoTa, string GiaBan, string NgayTaiLen, string TrangThai,
            string SoLuong, string MaTL, string MaTG, string MaNXB, HttpPostedFileBase TenFileAnh)
        {
            ViewBag.TheLoai = db.TheLoais;
            ViewBag.TacGia = db.TacGias;
            ViewBag.NhaXuatBan = db.NhaXuatBans;

            SachDAO dao = new SachDAO();
            Sach sach = dao.getByMaS(MaS);
            string photo = sach.TenFileAnh;

            sach.TenS = TenS;
            sach.MoTa = MoTa;

            if (GiaBan != "")
            {
                sach.GiaBan = Int32.Parse(GiaBan);
            }
            else if (GiaBan == "")
            {
                sach.GiaBan = 0;
            }

            string str = sach.NgayTaiLen.ToString("dd/MM/yyyy");
            string ns = sach.NgayTaiLen.ToString();
            if (NgayTaiLen != null && NgayTaiLen != str)
            {
                sach.NgayTaiLen = DateTime.Parse(NgayTaiLen);
            }
            else if (NgayTaiLen != null && NgayTaiLen == str)
            {
                sach.NgayTaiLen = DateTime.Parse(ns);
            }
            else if (NgayTaiLen == null)
            {
                sach.NgayTaiLen = DateTime.Parse(ns);
            }

            sach.TrangThai = TrangThai;

            if (SoLuong != "")
            {
                sach.SoLuong = Int32.Parse(SoLuong);
            }
            else if (SoLuong == "")
            {
                sach.SoLuong = 0;
            }

            sach.MaTL = Int32.Parse(MaTL);
            sach.MaTG = Int32.Parse(MaTG);
            sach.MaNXB = Int32.Parse(MaNXB);

            if (ModelState.IsValid)
            {
                if (TenFileAnh != null && TenFileAnh.ContentLength > 0)
                {
                    var img = Path.GetFileName(TenFileAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photo/"),
                                            System.IO.Path.GetFileName(TenFileAnh.FileName));
                    TenFileAnh.SaveAs(path);

                    sach.TenFileAnh = TenFileAnh.FileName;
                    dao.Edit(sach);
                }
                else if (TenFileAnh == null)
                {
                    var img = Path.GetFileName("icons8-open-book-100.png");
                    sach.TenFileAnh = photo;
                    dao.Edit(sach);
                }
                return RedirectToAction("Sach");
            }
            else
            {
                return View(sach);
            }
        }
        [HttpPost]
        public ActionResult ChiTiet(int MaS, string TenS, string MoTa, string GiaBan, string NgayTaiLen, string TrangThai,
            string SoLuong, string MaTL, string MaTG, string MaNXB, HttpPostedFileBase TenFileAnh)
        {
            SachDAO dao = new SachDAO();
            Sach sach = dao.getByMaS(MaS);
            return View(sach);
        }
    }
}