using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom3.Core.Domains;
using Nhom3.Core.ViewModels;

namespace Nhom3.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private FlowerContext db = new FlowerContext();

        // GET: Admin/Order
        public ActionResult Index()
        {
            var hoaDons = db.HoaDons.Include(h => h.GioHang);
            return View(hoaDons.ToList());
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            var listProduct = db.ChiTietGioHangs.ToList().Where(i => i.MaGioHang == hoaDon.MaGioHang).ToList();

            List<OrderProduct> products = new List<OrderProduct>();
            foreach (var pro in listProduct)
            {
                var product = db.SanPhams.Find(pro.MaSP);
                if (product != null)
                {
                    var orderProduct = new OrderProduct(product, pro.SoLuongMua);
                    products.Add(orderProduct);
                }
            }
            OrderDetails orderDetails = new OrderDetails(hoaDon, products);
            if (listProduct.Count > 1)
                orderDetails.PhiShip = 0;
            if (listProduct.Count >=2) orderDetails.PhiShip = 15000;
            return View(orderDetails);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            ViewBag.MaGioHang = new SelectList(db.GioHangs, "MaGioHang", "TenTaiKhoan");
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHoaDon,NgayDat,TinhTrang,PhiShip,GhiChu,DcNhanHang,MaGioHang")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGioHang = new SelectList(db.GioHangs, "MaGioHang", "TenTaiKhoan", hoaDon.MaGioHang);
            return View(hoaDon);
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            var listProduct = db.ChiTietGioHangs.ToList().Where(i => i.MaGioHang == hoaDon.MaGioHang).ToList();
            List<OrderProduct> products = new List<OrderProduct>();
            foreach (var pro in listProduct)
            {
                var product = db.SanPhams.Find(pro.MaSP);
                if (product != null)
                {
                    var orderProduct = new OrderProduct(product, pro.SoLuongMua);
                    products.Add(orderProduct);
                }
            }
            OrderDetails orderDetails = new OrderDetails(hoaDon, products);
            ViewBag.MaGioHang = new SelectList(db.GioHangs, "MaGioHang", "TenTaiKhoan", hoaDon.MaGioHang);
            return View(orderDetails);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(int? orderCode, string TinhTrang)
        {
            var orderGet = db.HoaDons.Find(orderCode);

            try
            {

                db.Entry(orderGet).State = EntityState.Modified;
                orderGet.TinhTrang = TinhTrang;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }


        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
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
