namespace WebBanSach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DauSach")]
    public partial class DauSach
    {
        [Key]
        [StringLength(10)]
        public string MaDS { get; set; }

        [StringLength(50)]
        public string TenDS { get; set; }

        [StringLength(10)]
        public string MaNXB { get; set; }

        [StringLength(10)]
        public string MaTL { get; set; }

        [StringLength(10)]
        public string MaTG { get; set; }
    }
}
