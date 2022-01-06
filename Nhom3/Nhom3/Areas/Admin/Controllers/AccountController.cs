using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom3.Core.Domains;

namespace Nhom3.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private FlowerContext db = new FlowerContext();

        // GET: Admin/Account
        public ActionResult Index(int? page)
        {

            return View(db.TaiKhoans.ToList());
        }

        // GET: Admin/Account/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: Admin/Account/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenTaiKhoan,MatKhau,Quyen,TinhTrang,TenKhachHang,Email,SoDienThoai,DiaChi")] TaiKhoan taiKhoan)
        {
            var accounts = db.TaiKhoans.ToList();
            if (ModelState.IsValid)
            {
                var exist = accounts.Any(i => i.TenTaiKhoan.ToLower().Equals(taiKhoan.TenTaiKhoan.ToLower()));
                if (!exist)
                {
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.duplicate = "no";
                    ViewBag.Error = "Tên tài khoản đã tồn tại";
                    return View("Create", taiKhoan);
                }
            }
            else
            {
                return View(taiKhoan);
            }
        }

        // GET: Admin/Account/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: Admin/Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taiKhoan);
        }

        // GET: Admin/Account/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: Admin/Account/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(string id)
        {
            //lấy ra tài khoản
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan.TenTaiKhoan.ToLower().Equals(Session["FullName"].ToString().ToLower())) 
            {
                ViewBag.Error = "Không thể xóa tài khoản bạn đang đăng nhập !!!";
                return View("Delete", taiKhoan);
            }
            //tìm kiếm giỏ hàng của tài khoản
            var carts = db.GioHangs.ToList();
            var cartOfAccount = carts.Where(i => i.TenTaiKhoan.Equals(id)).FirstOrDefault();
            //tồn tại giỏ hàng
            if (cartOfAccount != null)
            {
                //check tồn tại hóa đơn
                var order = db.HoaDons.ToList().Where(i => i.MaGioHang == cartOfAccount.MaGioHang).ToList();
                //nếu tồn tại trong hóa đơn thì báo lỗi
                if (order.Count > 0)
                {
                    ViewBag.Error = "Không xóa được tài khoản! Do tài khoản này đã có đơn hàng";
                    return View("Delete", taiKhoan);
                }
                //nếu không tồn tại hóa đơn thì xóa chi tiết giỏ hàng-xóa giỏ hàng-xóa tài khoản
                var productInCart = db.ChiTietGioHangs.Where(i => i.MaGioHang == cartOfAccount.MaGioHang).ToList();

                if (productInCart.Count == 0)
                {
                    //nếu giỏ hàng ko có hàng thì xóa giỏ hàng
                    db.GioHangs.Remove(cartOfAccount);
                    db.TaiKhoans.Remove(taiKhoan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in productInCart)
                    {
                        db.ChiTietGioHangs.Remove(item);
                    }
                    db.GioHangs.Remove(cartOfAccount);
                    db.TaiKhoans.Remove(taiKhoan);
                    db.SaveChanges();
                }
            }
            //nếu ko có giỏ hàng thì sẽ ko có hóa đơn và sẽ xóa đc
            else
            {
                db.TaiKhoans.Remove(taiKhoan);
                db.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
