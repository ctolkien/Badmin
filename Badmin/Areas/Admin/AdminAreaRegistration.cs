using System.Web.Mvc;

namespace Badmin.Areas.admin
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
                "Admin_List",
                "admin/{type}/{controller}/{action}/{id}",
                new { action="index", id = UrlParameter.Optional  }
                );

            context.MapRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", controller = "Dashboard",  id = UrlParameter.Optional }
            );
        }
    }
}
