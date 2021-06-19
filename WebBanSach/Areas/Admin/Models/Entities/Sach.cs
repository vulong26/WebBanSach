namespace WebBanSach.Areas.Admin.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ThuocTinhs = new HashSet<ThuocTinh>();
        }

        [Key]
        public int MaS { get; set; }

        [StringLength(50)]
        public string TenS { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        [StringLength(500)]
        public string TenFileAnh { get; set; }

        public int GiaBan { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayTaiLen { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        public int SoLuong { get; set; }

        public int MaTL { get; set; }

        public int MaTG { get; set; }

        public int MaNXB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual NhaXuatBan NhaXuatBan { get; set; }

        public virtual TacGia TacGia { get; set; }

        public virtual TheLoai TheLoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThuocTinh> ThuocTinhs { get; set; }
    }
}
