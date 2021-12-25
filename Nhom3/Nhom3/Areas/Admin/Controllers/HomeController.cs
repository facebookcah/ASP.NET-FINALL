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
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(TaiKhoan account)
        {
            if (account.TenTaiKhoan != null && account.MatKhau != null)
            {
                var accounts = db.TaiKhoans.ToList();
                var exist = accounts.Any(i => i.TenTaiKhoan.ToLower().Equals(account.TenTaiKhoan.ToLower()) && i.MatKhau.Equals(account.MatKhau));
                if (exist) {
                    var thisAccount= accounts.Where(i => i.TenTaiKhoan.ToLower().Equals(account.TenTaiKhoan.ToLower()) && i.MatKhau.Equals(account.MatKhau)).FirstOrDefault();
                    Session["FullName"] = thisAccount.TenTaiKhoan;
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