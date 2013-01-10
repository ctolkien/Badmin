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

        public ActionResult Index(string id, int page =1)
        {

            var dataConfig = badmin.DataConfigurations.SingleOrDefault(x => x.Name == id);

            const int pageSize = 2;

            int skip = (int)((page - 1) * pageSize);

            
            var orderedData = dataConfig.Data.ToList();

            var property = orderedData
                            .Skip(skip)
                            .Take(pageSize);


            return View(property);
        }

    }
}
