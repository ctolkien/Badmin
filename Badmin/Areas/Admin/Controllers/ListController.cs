using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace Badmin.Areas.Admin.Controllers
{
    public class ListController : BadminBaseController
    {

        

        public ListController(IBadmin badmin) : base(badmin)
        {
        }

        //
        // GET: /Admin/List/

        public ActionResult Index(string type, int page = 1)
        {
         
            const int PageSize = 2;

            var config = badmin.GetDataConfigurationForType<dynamic>(type);

            var dataContext = Badmin.CreateDataCotext(config);

            var set = dataContext.Set(config.Data.ElementType);
            
            //kill me now..
            //this pulls all the data back, is not paging appropriately...
            var objectlist = new List<object>();
            foreach (var item in set)
            {
                objectlist.Add(item);
            }

            var list = objectlist.AsQueryable().ToPagedList(page, PageSize);


            return View(list);
        }

        public ActionResult Edit(string type, int id)
        {

            //ugly, fix trhis
            var data = badmin.Configurations.Single(x => x.Name.ToUpper() == type.ToUpper());

            var dataContext = Badmin.CreateDataCotext(data);

            var item = dataContext.Set(data.Data.ElementType).Find(id);
            
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(string type, int id, FormCollection forms)
        {
            if (!ModelState.IsValid)
                return View();

            //blargh
            var data = badmin.Configurations.Single(x => x.Name.ToUpper() == type.ToUpper());

            var dataContext = Badmin.CreateDataCotext(data);
            
            var item = dataContext.Set(data.Data.ElementType).Find(id) as dynamic;
            
            UpdateModel(item);

            dataContext.SaveChanges();


            return RedirectToAction("index", new { type = type });

        }

    }
}
