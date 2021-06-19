namespace WebBanSach.Areas.Admin.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [Key]
        public int MaNV { get; set; }

        [StringLength(50)]
        public string TenNV { get; set; }

        [StringLength(10)]
        public string SDT { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        [StringLength(3)]
        public string GioiTinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }

        public decimal Luong { get; set; }

        public int? MaBP { get; set; }

        [StringLength(50)]
        public string Anh { get; set; }
        public virtual BoPhan BoPhan { get; set; }
    }
}
