namespace Nhom3.Core.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            GioHangs = new HashSet<GioHang>();
        }

        [Key]
        [Display(Name = "Tên đăng nhập")]
        [StringLength(100, ErrorMessage = "Tên tài khoản không vượt quá 100 kí tự")]
        [Required(ErrorMessage = "Tên tài khoản là bắt buộc!")]
        public string TenTaiKhoan { get; set; }
        [Display(Name = "Mật khẩu")]
        
        [Required(ErrorMessage = "Mật khẩu khoản là bắt buộc!")]
        public string MatKhau { get; set; }
        [Display(Name = "Phân vùng")]
        public int Quyen { get; set; }
        [Display(Name = "Tình trạng")]

        public bool TinhTrang { get; set; }
        [Display(Name = "Tên khách hàng")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không vượt quá 100 kí tự!")]
        public string TenKhachHang { get; set; }
        [Display(Name = "Email")]
       
        [EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        [StringLength(12, ErrorMessage = "Số điện thoại không đúng định dạng!")]
        public string SoDienThoai { get; set; }
        [Display(Name = "Địa chỉ")]
        [Column(TypeName = "ntext")]
        public string DiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }
    }
}
