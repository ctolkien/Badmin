using System.Linq;
using System.Web.Mvc;

namespace Badmin.Areas.Admin.Controllers
{
    public class DashboardController : BadminBaseController
    {

        public DashboardController(IBadmin badmin) : base(badmin)
        {
            
        }
        
        // GET: /Admin/Dashboard/

        public ActionResult Index()
        {

            var model = badmin.Configurations.Where(x => x.VisibleInMenu);

            return View(model.Select(x => x.Name));
        }

    }
}
