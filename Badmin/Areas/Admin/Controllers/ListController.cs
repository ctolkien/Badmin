using Badmin;
using Badmin.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Badmin.Areas.Admin.Controllers
{
    public class ListController : BadminBaseController
    {

        public ListController(IBadmin badmin) : base(badmin)
        {
        }

        //
        // GET: /Admin/List/

        public ActionResult Index(string id)
        {

            var dataConfig = badmin.DataConfigurations.SingleOrDefault(x => x.Name == id);

            var property = dataConfig.Data;


            
            
            //var property = badmin.DataConfigurations.SingleOrDefault(x => x.Name == id).Data.Cast<IEnumerable;

            //var property = typeof(DatabaseContext).GetProperties().Where(x => x.Name == property).First();

            //r things = base.dataContext


            return View(property);
        }

    }
}
