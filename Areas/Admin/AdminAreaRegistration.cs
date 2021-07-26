using System.Web.Mvc;

namespace ShopPhone.Areas.Admin
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
                new { action = "Index", Controller = "Phones", id = UrlParameter.Optional },
                new[] { "ShopPhone.Areas.Admin.Controllers" }
            );
        }
    }
}