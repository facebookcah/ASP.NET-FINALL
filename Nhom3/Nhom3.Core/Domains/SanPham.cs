namespace Nhom3.Core.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
        }

        [Key]
        [Display(Name = "Mã sản phẩm")]

        public int MaSP { get; set; }
        [Display(Name = "Danh mục")]

        public int MaDM { get; set; }
        [Display(Name = "Tên sản phẩm")]

        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không vượt quá 100 kí tự!")]
        public string TenSP { get; set; }
        [Display(Name = "Hoa chính")]

        [Required(ErrorMessage = "Hoa chính không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên hoa chính không vượt quá 100 kí tự!")]
        public string HoaChinh { get; set; }
        [Display(Name = "Hoa phụ")]
        [Required(ErrorMessage = "Hoa phụ không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên hoa phụ không vượt quá 100 kí tự!")]
        public string HoaPhu { get; set; }
        [Display(Name = "Chiều ngang")]
        [Required(ErrorMessage = "Chiều ngang không được để trống")]

        public int ChieuNgang { get; set; }
        [Display(Name = "Chiều cao")]
        [Required(ErrorMessage = "Chiều cao không được để trống")]

        public int ChieuCao { get; set; }
        [Display(Name = "Trọng lượng")]
        public double? TrongLuong { get; set; }
        [Display(Name = "Số lượng tồn")]
        public int? SoLuongTon { get; set; }
        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Giá không được để trống")]
        public decimal Gia { get; set; }
        [Display(Name = "Giá khuyến mãi")]
        public decimal? GiaKM { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Ảnh không được để trống!")]
        [Display(Name = "Ảnh")]
        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
