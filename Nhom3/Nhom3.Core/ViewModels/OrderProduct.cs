using Nhom3.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom3.Core.ViewModels
{
    public class OrderProduct
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }

        public decimal Gia { get; set; }

        public decimal GiaKM { get; set; }

        public string Anh { get; set; }
        public int SoLuong { get; set; }

        public OrderProduct(SanPham pro, int quantity)
        {
            this.MaSP = pro.MaSP;
            this.TenSP = pro.TenSP;
            this.Gia = pro.Gia;
            this.GiaKM = (decimal)pro.GiaKM;
            this.Anh = pro.Anh;
            this.SoLuong = quantity;
        }
    }
}
