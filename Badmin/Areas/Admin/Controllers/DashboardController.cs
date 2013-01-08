using Badmin.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Badmin.Areas.Admin.Controllers
{
    public class DashboardController : BadminBaseController<DatabaseContext>
    {

        public DashboardController() : base(new DatabaseContext())
        {

        }

        // GET: /Admin/Dashboard/

        public ActionResult Index()
        {

            var foo = base.GetListOfTables();

            return View(foo);
        }

    }
}
