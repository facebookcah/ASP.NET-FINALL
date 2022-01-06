namespace Nhom3.Core.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        [Display(Name ="Mã hóa đơn")]
        public int MaHoaDon { get; set; }
        [Display(Name = "Ngày đặt")]

        public DateTime NgayDat { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="Tình trạng")]
        public string TinhTrang { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Phí vận chuyển")]

        public decimal PhiShip { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Ghi chú")]

        public string GhiChu { get; set; }
        [Required(ErrorMessage ="Địa chỉ nhận hàng không được để trống !")]
        [Column(TypeName = "ntext")]
        [Display(Name = "Địa chỉ nhận hàng")]

        public string DcNhanHang { get; set; }
        [Display(Name ="Mã giỏ hàng")]

        public int MaGioHang { get; set; }

        public virtual GioHang GioHang { get; set; }
    }
}
