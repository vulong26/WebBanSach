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
        public ActionResult TacGia()
        {
            var lstTG = db.TacGias;
            return PartialView(lstTG);
        }
        public ViewResult XemChitiet(string MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaS == MaSach);
            //if (sach == null)
            //{

            //    return null; 


            //}
            return View(sach);
        }
        public ActionResult TheLoai()
        {
            var lstTL = db.TheLoais;
            return PartialView(lstTL);
        }
        public ActionResult NhaXuatBan()
        {
            var lstNXb = db.NhaXuatBans;
            return PartialView(lstNXb);
        }

    }
}