namespace Core.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [Key]
        public int MaSanPham { get; set; }

        [Required]
        [StringLength(100)]
        public string TenSanPham { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string MoTa { get; set; }

        [Required]
        [StringLength(100)]
        public string HoaChinh { get; set; }

        [Required]
        [StringLength(100)]
        public string HoaPhu { get; set; }

        public int ChieuNgang { get; set; }

        public int ChieuCao { get; set; }

        public decimal GiaBan { get; set; }

        public decimal? GiaKM { get; set; }

        [StringLength(100)]
        public string Anh { get; set; }

        public int MaDanhMuc { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
