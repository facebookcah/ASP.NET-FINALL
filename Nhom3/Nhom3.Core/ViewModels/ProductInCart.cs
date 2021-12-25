using Nhom3.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom3.Core.ViewModels
{
    public class ProductInCart
    {
        public int CartCode { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int Quantity { get; set; }
        public ProductInCart(int cartCode,SanPham product,int quantity)
        {
            this.CartCode = cartCode;
            this.Code = (int)product.MaSP;
            this.Name = product.TenSP;
            this.Image = product.Anh;
            this.Price = product.Gia;
            this.SalePrice = (decimal)product.GiaKM;
            this.Quantity = quantity;
        }
    }
}
