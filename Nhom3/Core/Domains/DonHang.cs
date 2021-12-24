namespace Core.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [Key]
        public int MaDonHang { get; set; }

        public DateTime NgayDat { get; set; }

        public int TinhTrang { get; set; }

        public int MaKH { get; set; }

        public int MaHTTT { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual HTTT HTTT { get; set; }
    }
}
