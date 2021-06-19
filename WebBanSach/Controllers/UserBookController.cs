using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class UserBookController : Controller
    {
        // GET: UserBook
        EBookModel db = new EBookModel();
        #region TacGia
        public ActionResult TacGia()
        {
            var lstTG = db.TacGias;
            return PartialView(lstTG);
        }
        public ActionResult CTTacGia(string Matg)
        {
            TacGia tacGia = db.TacGias.SingleOrDefault(n => n.MaTG == Matg);
            if (tacGia == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            return View(tacGia);
        }
        public ActionResult TGPartial(string Matg)
        {
            TacGia tg = db.TacGias.SingleOrDefault(n => n.MaTG == Matg);
            if (Matg == null)
            {
                return RedirectToAction("Index", "User");
            }
            List<Sach> lstSach = db.Saches.Where(n => n.MaTG == Matg).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.TB = "Không có quyển sách nào";
            }

            return View(lstSach);
        }
        #endregion
        public ViewResult XemChitiet(string MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaS == MaSach);
            //if (sach == null)
            //{

            //    return null; 


            //}
            return View(sach);
        }
        #region Theloai
        public ActionResult TheLoai()
        {
            var lstTL = db.TheLoais;
            return PartialView(lstTL.Take(6));
        }
        public ActionResult CTTheLoai1(string Matl)
        {
            TheLoai tacGia = db.TheLoais.SingleOrDefault(n => n.MaTL == Matl);
            if (tacGia == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            return View(tacGia);
        }
        public ActionResult CTTheLoai(string Matl)
        {
            TheLoai tg = db.TheLoais.SingleOrDefault(n => n.MaTL == Matl);
            if (Matl == null)
            {
                return RedirectToAction("Index", "User");
            }
            List<Sach> lstSach = db.Saches.Where(n => n.MaTG == Matl).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.TB = "Không có quyển sách nào";
            }

            return View(lstSach);
        }
        #endregion
        #region NXB
        public ActionResult NhaXuatBan()
        {
            var lstNXb = db.NhaXuatBans;
            return PartialView(lstNXb.Take(6));
        }
        public ActionResult CTNXB(string MaNxb)
        {
            NhaXuatBan tacGia = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNxb);
            if (tacGia == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            return View(tacGia);
        }
        public ActionResult NXBPartial(string MaNxb)
        {
            NhaXuatBan tacGia = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNxb);
            if (MaNxb == null)
            {
                return RedirectToAction("Index", "User");
            }
            List<Sach> lstSach = db.Saches.Where(n => n.MaNXB == MaNxb).Take(4).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.TB = "Không có quyển sách nào";
            }

            return View(lstSach);
        }
        #endregion

       

    }
}