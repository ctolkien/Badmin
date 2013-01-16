using System;
using System.Data;
using System.Linq;
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

            const int PageSize = 2;

            int skip = ((page - 1) * PageSize);

            
            //hack: code is broken here...

            IQueryable<object> orderedData = dataConfig.Data;

            //can't cast it to IHasId
            //it's all goen to hell...
            var property = orderedData
                .OrderBy(x => x)
                .Skip(skip).Take(PageSize)
                .ToList();
    

            return View(property);
        }

    }
}
