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
    public class CategoryController : Controller
    {
        private FlowerContext db = new FlowerContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(db.DanhMucs.ToList());
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDM,TenDM")] DanhMuc danhMuc)
        {
            
            if (ModelState.IsValid)
            {
                var isExist = db.DanhMucs.ToList().Any(i => i.TenDM.ToLower().Trim().Equals(danhMuc.TenDM.ToLower().Trim()));
                if (isExist)
                {
                    ViewBag.Message = "Danh mục đã tồn tại! Vui lòng chọn tên danh mục khác";
                    return View(danhMuc);
                }
                db.DanhMucs.Add(danhMuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danhMuc);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDM,TenDM")] DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                var isExist = db.DanhMucs.ToList().Any(i => i.TenDM.ToLower().Trim().Equals(danhMuc.TenDM.ToLower().Trim()));
                if (isExist)
                {
                    ViewBag.Message = "Danh mục đã tồn tại! Vui lòng chọn tên danh mục khác";
                    return View(danhMuc);
                }
                db.Entry(danhMuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhMuc);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            var products = db.SanPhams.ToList();
            var hasProduct=products.Any(i => i.MaDM == id);

            if (hasProduct)
            {
                ViewBag.Error = "Không xóa được danh mục! Do tồn tại sản phẩm đang bán";
                return View("Delete", danhMuc);
            }
            
           
            db.DanhMucs.Remove(danhMuc);
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
