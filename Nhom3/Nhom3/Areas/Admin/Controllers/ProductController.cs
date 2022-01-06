using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom3.Core.Domains;
using PagedList;
namespace Nhom3.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private FlowerContext db = new FlowerContext();

        // GET: Admin/Product
        public ActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
        
            var sanPhams = db.SanPhams;
            return View(sanPhams.ToList().ToPagedList(pageNumber,pageSize));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {

            var categories = db.DanhMucs.ToList();
            IDictionary<int, String> dropList = new Dictionary<int, String>();
            foreach (var item in categories)
            {
                dropList.Add(item.MaDM, item.TenDM);
            }
            ViewBag.drop = dropList;
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham sanPham)
        {
            try
            {
                var categories = db.DanhMucs.ToList();
                IDictionary<int, String> dropList = new Dictionary<int, String>();
                foreach (var item in categories)
                {
                    dropList.Add(item.MaDM, item.TenDM);
                }
                ViewBag.drop = dropList;
                if (ModelState.IsValid)
                {
                    sanPham.Anh = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        //Use Namespace called : System.IO
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        //Lấy tên file upload
                        string UploadPath = Server.MapPath("~/wwwroot/Content/images/" + FileName);
                        //Copy và lưu file vào server
                        f.SaveAs(UploadPath);
                        //Lưu tên file vào trường Image
                        sanPham.Anh = FileName;
                    }

                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                }
                else
                {
                
                    return View(sanPham);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! ";

                return View(sanPham);
            }
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            var categories = db.DanhMucs.ToList();
            IDictionary<int, String> dropList = new Dictionary<int, String>();
            foreach (var item in categories)
            {
                dropList.Add(item.MaDM, item.TenDM);
            }
            ViewBag.drop = dropList;
            return View(sanPham);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SanPham sanPham)
        {
            string tenSP = sanPham.TenSP;
            var hoaChinh = sanPham.HoaChinh;
            var hoaPhu = sanPham.HoaPhu;
            var ChieuNgang = sanPham.ChieuNgang;
            var ChieuCao = sanPham.ChieuCao;
            var TrongLuong = sanPham.TrongLuong;
            var SoLuongTon = sanPham.SoLuongTon;
            var Gia = sanPham.Gia;
            var GiaKM = sanPham.GiaKM;
            var MaDM = sanPham.MaDM;
            var MoTa = sanPham.MoTa;

            try
            {
                if (ModelState.IsValid)
                {

                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        //Use Namespace called : System.IO
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        //Lấy tên file upload
                        string UploadPath = Server.MapPath("~/wwwroot/Content/images/" + FileName);
                        //Copy và lưu file vào server
                        f.SaveAs(UploadPath);
                        //Lưu tên file vào trường Image
                        db.Entry(sanPham).State = EntityState.Modified;
                        sanPham.Anh = FileName;
                        db.SaveChanges();

                    }
                    db.Entry(sanPham).State = EntityState.Modified;
                    sanPham.TenSP = tenSP;
                    sanPham.HoaChinh = hoaChinh;
                    sanPham.HoaPhu = hoaPhu;
                    sanPham.ChieuNgang = ChieuNgang;
                    sanPham.ChieuCao = ChieuCao;
                    sanPham.TrongLuong = TrongLuong;
                    sanPham.SoLuongTon = SoLuongTon;
                    sanPham.Gia = Gia;
                    sanPham.GiaKM = GiaKM;
                    sanPham.MaDM = MaDM;
                    sanPham.MoTa = MoTa;
                    db.SaveChanges();
                }
                else
                {
                    var categories = db.DanhMucs.ToList();
                    IDictionary<int, String> dropList = new Dictionary<int, String>();
                    foreach (var item in categories)
                    {
                        dropList.Add(item.MaDM, item.TenDM);
                    }
                    ViewBag.drop = dropList;
                    return View("Edit", sanPham);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! ";

                return View(sanPham);
            }
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            var cartDetails = db.ChiTietGioHangs.ToList();
            var isInCart = cartDetails.Any(i => i.MaSP == id);
            if (isInCart)
            {
                ViewBag.Error = "Không thể xóa sản phẩm do sản phẩm đang tồn tại trong giỏ hàng";
                return View("Delete", sanPham);
            }
            db.SanPhams.Remove(sanPham);
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
