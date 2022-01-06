using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using PagedList;
using Nhom3.Core.Domains;
using Nhom3.Core.ViewModels;

namespace Nhom3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        FlowerContext db = new FlowerContext();
        public ActionResult Index(int? madm, string searchString, int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var product = db.SanPhams.Select(p => p).ToList();
            /*db.SanPhams.Select(s => s).ToList();*/
            if (!string.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.TenSP.ToLower().Contains(searchString.ToLower())).ToList();
                return View(product.ToPagedList(pageNumber, pageSize));
            }
            if (madm > 0)
            {
                product = product.Where(p => p.MaDM == madm).ToList();
                return View(product.Where(s => s.MaDM == madm).ToPagedList(pageNumber, pageSize));
            }
            string categoryName = "";
            if (madm != null)
            {
                categoryName += db.DanhMucs.Find(madm).TenDM;
            }
            ViewBag.CategoryName = categoryName;
            return View(product.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AddToCart(int? productCode)
        {
            //kiểm tra đăng nhập
            if (Session["TenTaiKhoan"] == null)
            {
                List<ProductInCart> fakeData = new List<ProductInCart>();
                ViewBag.Message = "Vui lòng đăng nhập để tiếp tục mua sắm !!";
                return View("Cart", fakeData);
            }
            //pass case đăng nhập thì tìm giỏ hàng của tài khoản
            var code = Session["TenTaiKhoan"] as string;
            var carts = db.GioHangs.ToList();
            //kiểm tra tài khoản đã có giỏ hàng chưa
            var cartOfAccount = carts.Where(i => i.TenTaiKhoan.ToLower().Equals(code.ToLower())).FirstOrDefault();
            //nếu chưa thì tạo giỏ hàng mới cho tài khoản
            GioHang cart = new GioHang();
            if (cartOfAccount == null)
            {
                cart = new GioHang();
                cart.TenTaiKhoan = Session["TenTaiKhoan"] as string;
                db.GioHangs.Add(cart);
                db.SaveChanges();
            }
            // get product selected
            var product = db.SanPhams.Find(productCode);
            //check exist in chitietgiohang
            var productInOrderDetail = new ChiTietGioHang();
            if (cartOfAccount != null)
            {
                productInOrderDetail = db.ChiTietGioHangs.ToList().Where(i => i.MaGioHang == cartOfAccount.MaGioHang
            && i.MaSP == product.MaSP).ToList().FirstOrDefault();
            }
            else
            {
                productInOrderDetail = db.ChiTietGioHangs.ToList().Where(i => i.MaGioHang == cart.MaGioHang
                && i.MaSP == product.MaSP).ToList().FirstOrDefault();
            }
            //nếu tồn tại thì tăng số lượng ngược lại tạo mới và thêm vào db
            if (productInOrderDetail != null)
            {
                db.Entry(productInOrderDetail).State = EntityState.Modified;
                productInOrderDetail.SoLuongMua += 1;
                db.SaveChanges();
            }
            else
            {
                if (cartOfAccount == null)
                {


                    ChiTietGioHang chiTietGioHang = new ChiTietGioHang();
                    chiTietGioHang.MaGioHang = cart.MaGioHang;
                    chiTietGioHang.MaSP = product.MaSP;
                    chiTietGioHang.SoLuongMua = 1;
                    if (product.GiaKM == null) chiTietGioHang.Gia = product.Gia;
                    chiTietGioHang.Gia = (int)product.GiaKM;
                    db.ChiTietGioHangs.Add(chiTietGioHang);
                    db.SaveChanges();
                }
                else
                {
                    ChiTietGioHang chiTietGioHang = new ChiTietGioHang();
                    chiTietGioHang.MaGioHang = cartOfAccount.MaGioHang;
                    chiTietGioHang.MaSP = product.MaSP;
                    chiTietGioHang.SoLuongMua = 1;
                    if (product.GiaKM == null) chiTietGioHang.Gia = product.Gia;
                    chiTietGioHang.Gia = (int)product.GiaKM;
                    db.ChiTietGioHangs.Add(chiTietGioHang);
                    db.SaveChanges();
                }
            }
            var userName = Session["TenTaiKhoan"] as string;
            var cart1 = db.GioHangs.ToList().Where(i => i.TenTaiKhoan.ToLower().Equals(userName.ToLower())).FirstOrDefault();
            var products = db.ChiTietGioHangs.ToList().Where(i => i.MaGioHang == cart1.MaGioHang);
            List<ProductInCart> productInCarts = new List<ProductInCart>();
            foreach (var item in products)
            {
                var productInCart = db.SanPhams.Find(item.MaSP);
                productInCarts.Add(new ProductInCart(cart1.MaGioHang, productInCart, item.SoLuongMua));
            }
            return View("Cart", productInCarts);
        }
        public ActionResult Cart()
        {
            List<ProductInCart> fakeData = new List<ProductInCart>();
            if (Session["TenTaiKhoan"] == null)
            {
                ViewBag.Message = "Vui lòng đăng nhập để xem giỏ hàng !!";

                return View(fakeData);
            }
            //lấy tất cả sản phẩm trong giỏ hàng
            var userName = Session["TenTaiKhoan"] as string;
            var cart = db.GioHangs.ToList().Where(i => i.TenTaiKhoan.ToLower().Equals(userName.ToLower())).FirstOrDefault();
            if (cart == null)
            {
                ViewBag.Message = "Giỏ hàng chưa có sản phẩm nào \n Vui lòng chọn sản phẩm !!";
                return View(fakeData);
            }
            var products = db.ChiTietGioHangs.ToList().Where(i => i.MaGioHang == cart.MaGioHang);
            List<ProductInCart> productInCarts = new List<ProductInCart>();
            foreach (var item in products)
            {
                var product = db.SanPhams.Find(item.MaSP);
                productInCarts.Add(new ProductInCart(cart.MaGioHang, product, item.SoLuongMua));
            }
            return View(productInCarts);
        }


        public ActionResult Checkout(int? cartCode)
        {

            ViewBag.cartCode = cartCode;

            return View();
        }
        [HttpPost]
        public ActionResult Checkout(HoaDon hoaDon)
        {
            if (hoaDon.DcNhanHang == null)
            {
                return View(hoaDon);
            }
            var cart = db.GioHangs.ToList().Where(i => i.MaGioHang == hoaDon.MaGioHang).FirstOrDefault();
            if (cart == null)
            {
                string error = "Vui lòng đăng nhập hoặc thêm sản phẩm vào giỏ hàng để thanh toán";
                ViewBag.Succcess = error;
                return View(hoaDon);
            }
            var countProduct = db.ChiTietGioHangs.ToList().Where(i => i.MaGioHang == hoaDon.MaGioHang).ToList().Count;
            hoaDon.NgayDat = DateTime.Now;
            hoaDon.TinhTrang = "Đang giao";
            if (countProduct < 1)
                hoaDon.PhiShip = 15000;
            else
                hoaDon.PhiShip = 0;

            db.HoaDons.Add(hoaDon);
            db.SaveChanges();
            string mess = "Đặt hàng thành công";
            ViewBag.Succcess = mess;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditUser(string TenTK)
        {


            TaiKhoan taiKhoan = db.TaiKhoans.Find(TenTK);
            return View(taiKhoan);
        }
        [HttpPost]

        public ActionResult EditUser(TaiKhoan taiKhoan, string pass)
        {


            if (ModelState.IsValid)
            {
                if (taiKhoan.MatKhau == pass)
                {
                    db.Entry(taiKhoan).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.mess = "Thành công";
                    return RedirectToAction("Index");
                }
                ViewBag.mess = "Mật khẩu không đúng!!!";
                return View(taiKhoan);
            }
            else
            {
                return View(taiKhoan);
            }

        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Login(TaiKhoan user)
        {


            var userGet = db.TaiKhoans.Where(t => t.TenTaiKhoan.ToLower().Equals(user.TenTaiKhoan.ToLower()) && t.MatKhau.Equals(user.MatKhau)).ToList();
            if (user.TenTaiKhoan == null || user.MatKhau == null)
            {
                return View(userGet.FirstOrDefault());
            }
            if (userGet.Count() > 0)
            {

                if (userGet.FirstOrDefault().TinhTrang == false)
                {
                    // Hien thi thong bao loi
                    ViewBag.error = "Tài khoản bị khóa. Đăng nhập không thành công";
                    return View(userGet.FirstOrDefault());
                }
                else
                {
                    //Su dung session: add Session
                    Session["TaiKhoan"] = userGet.FirstOrDefault();
                    Session["TenKhachHang"] = userGet.FirstOrDefault().TenKhachHang;
                    Session["TenTaiKhoan"] = userGet.FirstOrDefault().TenTaiKhoan;
                    // Sang trang chu
                }
            }
            else
            {
                ViewBag.error = "Tên tài khoản hoặc mật khẩu không chính xác!";
                return View(userGet.FirstOrDefault());

            }

            return RedirectToAction("Index");
        }

        public ActionResult Users(string TenTK)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(TenTK);
            return View(taiKhoan);
        }
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        public PartialViewResult _Nav()
        {
            var danhmuc = db.DanhMucs.Select(p => p);
            return PartialView(danhmuc);
        }
        [HttpGet]
        public ActionResult Signin()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signin([Bind(Include = "TenTaiKhoan,MatKhau,Quyen,TinhTrang,TenKhachHang,Email,SoDienThoai,DiaChi")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                var taiKhoanFind = db.TaiKhoans.Find(taiKhoan.TenTaiKhoan);
                if (taiKhoanFind == null)
                {
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.ErrorSign = "Tên tài khoản trùng. Vui lòng nhập tên khác";
                }
            }
            //ViewBag.Infor = taiKhoan.ToString();
            return View(taiKhoan);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult ChangePassword(string TenTK)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(TenTK);
            return View(taiKhoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(TaiKhoan taiKhoan,
            string newPass, string rePass)
        {
            var acc = db.TaiKhoans.Find(taiKhoan.TenTaiKhoan);
            if (newPass.Equals(rePass))
            {

                if (newPass.Length < 8) ViewBag.mess = "Mật khẩu ít nhất 8 kí tự!";

                else
                {
                    db.Entry(acc).State = EntityState.Modified;
                    acc.MatKhau = newPass;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }



            }
            else
            {
                ViewBag.mess = "Mật khẩu không khớp!!!";
            }
            return View(taiKhoan);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Register register)
        {
            if (!ModelState.IsValid)
                return View();
            var exist = db.TaiKhoans.ToList().Any(i => i.TenTaiKhoan.ToLower().Equals(register.UserName.ToLower()));
            if (exist)
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại";
                return View();
            }
            TaiKhoan taiKhoan = new TaiKhoan();
            taiKhoan.TenTaiKhoan = register.UserName;
            taiKhoan.MatKhau = register.Password;
            taiKhoan.Quyen = 1;
            taiKhoan.TinhTrang = true;
            db.TaiKhoans.Add(taiKhoan);
            db.SaveChanges();
            return View("Login", taiKhoan);
        }
        public ActionResult Plus(int? cartCode, int? productCode)
        {
            var product = db.ChiTietGioHangs.ToList().Where(i => i.MaGioHang == cartCode && i.MaSP == productCode).FirstOrDefault();
            if (product != null)
            {
                db.Entry(product).State = EntityState.Modified;
                product.SoLuongMua += 1;
                db.SaveChanges();
            }
            return RedirectToAction("Cart");
        }

        public ActionResult Minius(int? cartCode, int? productCode)
        {
            var product = db.ChiTietGioHangs.ToList().Where(i => i.MaGioHang == cartCode && i.MaSP == productCode).FirstOrDefault();
            if (product != null)
            {
                if (product.SoLuongMua == 1)
                {
                    db.ChiTietGioHangs.Remove(product);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(product).State = EntityState.Modified;
                    product.SoLuongMua -= 1;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Cart");
        }
        public ActionResult RemoveFromCart(int? cartCode, int? productCode)
        {
            var product = db.ChiTietGioHangs.ToList().Where(i => i.MaGioHang == cartCode && i.MaSP == productCode).FirstOrDefault();
            if (product != null)
            {
                db.ChiTietGioHangs.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("Cart");
        }


    }


}

