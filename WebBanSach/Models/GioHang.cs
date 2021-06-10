using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebBanSach.Models
{
    public class GioHang
    {
        EBookModel db = new EBookModel();
        public string iMaS { get; set; }
        public string sTenS { get; set; }
        public string sTenFileAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien { get { return iSoLuong * dDonGia; } }
        
        public GioHang(string MaS)
        {
            iMaS = MaS;
            Sach sach = db.Saches.Single(n => n.MaS ==iMaS );
            sTenS = sach.TenS;
            sTenFileAnh = sach.TenFileAnh;
            dDonGia = double.Parse(sach.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}