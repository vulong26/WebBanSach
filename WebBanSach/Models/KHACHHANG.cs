namespace WebBanSach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHACHHANG")]
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            DonHangs = new HashSet<DonHang>();
        }

        [Key]
        [Display(Name ="Mã Khách hàng")]
        public int MaKH { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "{0} không thể để trống")]
        public string HoTen { get; set; }

        [Required(ErrorMessage ="{0} không thể để trống")]
        [StringLength(50)]
        [Display(Name = "Mật khẩu")]
        
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "{0} không thể để trống")]

        [StringLength(100)]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} không thể để trống")]

        [StringLength(200)]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "{0} không thể để trống")]
        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        public string DienThoai { get; set; }
        

        [StringLength(3)]
        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgaySinh { get; set; }

        [Required(ErrorMessage = "{0} không thể để trống")]
        [StringLength(50)]
        [Display(Name = "Tài khoản ")]

        public string TaiKhoan { get; set; }
       

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
