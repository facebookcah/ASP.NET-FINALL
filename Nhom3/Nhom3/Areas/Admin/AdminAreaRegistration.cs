using System.Web.Mvc;

namespace Nhom3.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller="Home",action = "LoginAdmin", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin_category",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Category", action = "IndexCategory", id = UrlParameter.Optional }
            );
        }
    }
}