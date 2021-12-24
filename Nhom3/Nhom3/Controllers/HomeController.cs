using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using PagedList;
using Nhom3.Core.Domains;

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

        public ActionResult Cart()
        {


            return View();
        }

       
        public ActionResult Checkout()
        {


            return View();
        }
        [HttpGet]
        public ActionResult EditUser(string TenTK)
        {


            TaiKhoan taiKhoan = db.TaiKhoans.Find(TenTK);
            return View(taiKhoan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "TenTaiKhoan,MatKhau,Quyen,TinhTrang," +
            "TenKhachHang,Email,SoDienThoai,DiaChi")] TaiKhoan taiKhoan, string PassValidate)
        {

            string Pass = db.TaiKhoans.AsNoTracking().
                Where(t => t.TenTaiKhoan.Equals(taiKhoan.TenTaiKhoan)).
                FirstOrDefault().MatKhau;
            if (Pass.Equals(PassValidate))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(taiKhoan).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message1"] = "Thành công";
                    return RedirectToAction("Index");
                }
                ViewBag.Message2 = "Msg2";
                return View(taiKhoan);
            }
            else
            {
                ViewBag.Message3 = "Msg3";
                return View(taiKhoan);
            }

        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string TenTaiKhoan, string MatKhau)
        {
            if (string.IsNullOrEmpty(TenTaiKhoan))
            {
                ViewBag.ErrorTenTaiKhoan = "Tên tài khoản không được để trống";
            }
            if (string.IsNullOrEmpty(TenTaiKhoan))
            {
                ViewBag.ErrorMatKhau = "Mật khẩu không được để trống";
            }
            if (ModelState.IsValid)
            {
                var user = db.TaiKhoans.Where(t => t.TenTaiKhoan.Equals(TenTaiKhoan) && t.MatKhau.Equals(MatKhau) && t.Quyen == 0).ToList();
                if (user.Count() > 0)
                {

                    if (user.FirstOrDefault().TinhTrang == false)
                    {
                        // Hien thi thong bao loi
                        ViewBag.error = "Tài khoản bị khóa. Đăng nhập không thành công";
                    }
                    else
                    {
                        //Su dung session: add Session
                        Session["TaiKhoan"] = user.FirstOrDefault();
                        Session["TenKhachHang"] = user.FirstOrDefault().TenKhachHang;
                        Session["TenTaiKhoan"] = user.FirstOrDefault().TenTaiKhoan;
                        // Sang trang chu
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.error = "Đăng nhập không thành công";
                }
            }
            return View();
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
            return RedirectToAction("Login");
        }

        public ActionResult ChangePassword(string TenTK)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(TenTK);
            return View(taiKhoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "TenTaiKhoan,MatKhau,Quyen,TinhTrang,TenKhachHang,Email,SoDienThoai,DiaChi")] TaiKhoan taiKhoan,
            string OldPassword)
        {
            string old_pass = db.TaiKhoans.AsNoTracking().
                Where(t => t.TenTaiKhoan.Equals(taiKhoan.TenTaiKhoan)).
                FirstOrDefault().MatKhau;
            if (old_pass.Equals(OldPassword))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(taiKhoan).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = "Thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Lỗi nhập dữ liệu!!!";
                }
            }
            else
            {
                ViewBag.Message = "Mật khẩu cũ không chính xác!!!";
            }
            return View(taiKhoan);
        }

    }


}

