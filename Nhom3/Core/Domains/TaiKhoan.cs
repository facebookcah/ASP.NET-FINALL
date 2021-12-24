namespace Core.Domains
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
        public int MaTaiKHoan { get; set; }

        [Required]
        [StringLength(20)]
        public string TenTaiKhoan { get; set; }

        [Required]
        [StringLength(25)]
        public string MatKhau { get; set; }

        public bool TrangThai { get; set; }

        public int MaKH { get; set; }

        public int MaQuyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual Quyen Quyen { get; set; }
    }
}
