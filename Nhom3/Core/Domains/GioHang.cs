namespace Core.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GioHang")]
    public partial class GioHang
    {
        [Key]
        public int MaGioHang { get; set; }

        public int? MaTaiKHoan { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
