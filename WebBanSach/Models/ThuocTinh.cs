namespace WebBanSach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThuocTinh")]
    public partial class ThuocTinh
    {
        [Key]
        [StringLength(10)]
        public string MaTT { get; set; }

        public int? SoLuotTaiVe { get; set; }

        public int? SoLuotYeuThich { get; set; }

        public int? SoluotTimKiem { get; set; }

        [StringLength(10)]
        public string MaS { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
