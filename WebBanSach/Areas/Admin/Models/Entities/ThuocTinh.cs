namespace WebBanSach.Areas.Admin.Models.Entities
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
        public int MaTT { get; set; }

        public int? SoLuotTaiVe { get; set; }

        public int? SoLuotYeuThich { get; set; }

        public int? SoluotTimKiem { get; set; }

        public int? MaS { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
