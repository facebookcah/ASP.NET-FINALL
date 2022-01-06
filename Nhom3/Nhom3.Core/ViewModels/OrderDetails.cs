using Nhom3.Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom3.Core.ViewModels
{
    public class OrderDetails
    {
        [Display(Name ="Mã hóa đơn")]
        public int MaHoaDon { get; set; }
        [Display(Name = "Ngày đặt")]

        public DateTime NgayDat { get; set; }

        [Display(Name = "Tình trạng")]

        public string TinhTrang { get; set; }

        [Display(Name = "Phí ship")]

        public decimal PhiShip { get; set; }
        [Display(Name = "Ghi chú")]

        public string GhiChu { get; set; }
        [Display(Name = "Địa chỉ nhận hàng")]

        public string DcNhanHang { get; set; }
        [Display(Name = "Tài khoản")]

        public string taiKhoan { get; set; }

        public List<OrderProduct> sanPhams { get; set; }

        public OrderDetails(HoaDon hoaDon, List<OrderProduct> sanPhams)
        {
            this.MaHoaDon = hoaDon.MaHoaDon;
            this.NgayDat = hoaDon.NgayDat;
            this.TinhTrang = hoaDon.TinhTrang;
            
            this.GhiChu = hoaDon.GhiChu;
            this.DcNhanHang = hoaDon.DcNhanHang;
            this.taiKhoan = hoaDon.GioHang.TenTaiKhoan;
            this.sanPhams =sanPhams;

        }

    }
}
