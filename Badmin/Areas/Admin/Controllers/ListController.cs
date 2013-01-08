using Badmin.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Badmin.Areas.Admin.Controllers
{
    public class ListController : BadminBaseController<DatabaseContext>
    {

        public ListController() : base (new DatabaseContext())
        {

        }

        //
        // GET: /Admin/List/

        public ActionResult Index(string property)
        {

            //var property = typeof(DatabaseContext).GetProperties().Where(x => x.Name == property).First();

            //r things = base.dataContext


            return View();
        }

    }
}
