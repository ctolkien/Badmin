using System;
using System.Collections.Generic;
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

        public ActionResult Index(string type, int page =1)
        {
            const int PageSize = 2;

            var dataConfig = badmin.Configurations.SingleOrDefault(x => x.Name.ToUpper() == type.ToUpper());

            var property = dataConfig.Data.ToPagedList(page, PageSize);



            return View(property);
        }

        public ActionResult Edit(string type, int id)
        {

            //ugly, fix trhis
            var data = badmin.Configurations.Single(x => x.Name.ToUpper() == type.ToUpper());

            var dataContext = Badmin.CreateDataCotext(data);

            var item = dataContext.Set(data.DataType).Find(id);
            
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(string type, int id, FormCollection forms)
        {
            if (!ModelState.IsValid)
                return View();

            var data = badmin.Configurations.Single(x => x.Name.ToUpper() == type.ToUpper());

            var dataContext = Badmin.CreateDataCotext(data);
            
            var item = dataContext.Set(data.DataType).Find(id) as dynamic;
            
            UpdateModel(item);

            dataContext.SaveChanges();


            return RedirectToAction("index", new { type = type });

        }

    }
}
