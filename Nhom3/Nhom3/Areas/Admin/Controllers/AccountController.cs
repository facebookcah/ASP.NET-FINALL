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
        public ActionResult Index()
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
            var exist=accounts.Any(i => i.TenTaiKhoan.ToLower().Equals(taiKhoan.TenTaiKhoan.ToLower()));
         
            if (ModelState.IsValid)
            {
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
                    return View("Create",taiKhoan);
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
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            var cart = db.GioHangs.ToList();
            var existInCart = cart.Any(i => i.TenTaiKhoan.Equals(id));
            if (existInCart)
            {
                var cartOfAccount = cart.Where(i => i.TenTaiKhoan.Equals(id)).FirstOrDefault();
                var cartCode = cartOfAccount.MaGioHang;
                var countProductInCart = db.ChiTietGioHangs.Where(i=>i.MaGioHang==cartCode).ToList();
                if (countProductInCart.Count == 0)
                {
                    var hoadon = db.HoaDons.ToList().Find(i => i.MaGioHang == cartCode);
                    db.HoaDons.Remove(hoadon);
                    db.GioHangs.Remove(cartOfAccount);
                    db.TaiKhoans.Remove(taiKhoan);
                   
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Error = "Không xóa được tài khoản! Do tài khoản này đã có đơn hàng";
                    return View("Delete",taiKhoan);
                }
            }
            
            db.TaiKhoans.Remove(taiKhoan);
            db.SaveChanges();
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
