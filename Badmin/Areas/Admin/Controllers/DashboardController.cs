using Badmin;
using Badmin.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
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

            var model = badmin.DataConfigurations.Where(x => x.VisibleInMenu);

            return View(model.Select(x => x.Name));
        }

    }
}
