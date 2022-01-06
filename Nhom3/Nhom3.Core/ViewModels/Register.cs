using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom3.Core.ViewModels
{
    public class Register
    {
        [Required(ErrorMessage ="Tên đăng nhập không được để trống")]
        [Display(Name ="Tên đăng nhập")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [Display(Name = "Mật khẩu")]
        [MinLength(8,ErrorMessage ="Mật khẩu không ít hơn 8 kí tự")]
        [MaxLength(20, ErrorMessage = "Mật khẩu không nhiều hơn 20 kí tự")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password",ErrorMessage ="Mật khẩu không khớp!")]
        public string RePassword { get; set; }
    }
}
