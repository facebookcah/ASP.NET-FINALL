using Nhom3.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom3.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        FlowerContext db = new FlowerContext();
        public IDictionary<string, int> getDashboard()
        {
            var countAccount = db.TaiKhoans.ToList().Count;
            var countCategory = db.DanhMucs.ToList().Count;
            var countProduct = db.SanPhams.ToList().Count;
            var countOrder = db.HoaDons.ToList().Count;

            IDictionary<string, int> list = new Dictionary<string, int>();
            list.Add("Danh mục", countCategory);
            list.Add("Tài khoản", countAccount);
            list.Add("Sản phẩm", countProduct);
            list.Add("Đơn hàng", countOrder);
            return list;
        }
        // GET: Admin/Home
        public ActionResult Index()

        {
            var list = getDashboard();
            ViewBag.list = list;
            return View();
        }
        public ActionResult LoginAdmin()
        {
            var list = getDashboard();
            ViewBag.list = list;
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(TaiKhoan account)
        {
            if (account.TenTaiKhoan != null && account.MatKhau != null)
            {
                var accounts = db.TaiKhoans.ToList();
                var exist = accounts.Any(i => i.TenTaiKhoan.ToLower().Equals(account.TenTaiKhoan.ToLower()) && i.MatKhau.Equals(account.MatKhau));
                if (exist)
                {
                    var thisAccount = accounts.Where(i => i.TenTaiKhoan.ToLower().Equals(account.TenTaiKhoan.ToLower()) && i.MatKhau.Equals(account.MatKhau)).FirstOrDefault();
                    if (!thisAccount.TinhTrang)
                    {
                        ViewBag.Error = "Tài khoản đã bị khóa!";
                        return View(account);
                    }
                    if (thisAccount.Quyen==1)
                    {
                        ViewBag.Error = "Tài khoản không có quyền truy cập!";
                        return View(account);
                    }
                    Session["FullName"] = thisAccount.TenTaiKhoan;
                    var list = getDashboard();
                    ViewBag.list = list;
                    return View("Index", account);
                }
                else
                {
                    ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không chính xác !";
                    return View(account);
                }
            }
            return View(account);
        }
        public ActionResult Logout()
        {
            Session["FullName"] = "";
            return View("LoginAdmin");
        }
    }
}