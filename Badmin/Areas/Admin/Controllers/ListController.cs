using System.Collections.Generic;
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

        public ActionResult Index(string type, int page = 1)
        {
         
            const int pageSize = 3;

            var config = badmin.GetDataConfiguration(type);

            var dataContext = badmin.CreateDataContext(type);

            var set = dataContext.Set(config.Data.ElementType);
            
            //kill me now..
            //this pulls all the data back, is not paging appropriately...
            //need some way to convert a DbSet, to a DbSet<T>
            //note can use reflection and 'make generic type'
            
            var objectlist = new List<object>();
            foreach (var item in set)
            {
                objectlist.Add(item);
            }

            var list = objectlist.AsQueryable().ToPagedList(page, pageSize);


            return View(list);
        }

        public ActionResult Edit(string type, int id)
        {

            //ugly, fix trhis
            var data = badmin.GetDataConfiguration(type);

            var dataContext = Badmin.CreateDataContext(data);

            var item = dataContext.Set(data.Data.ElementType).Find(id);
            
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string type, int id, FormCollection forms)
        {
            if (!ModelState.IsValid)
                return View();

            //blargh
            var data = badmin.GetDataConfiguration(type);

            var dataContext = Badmin.CreateDataContext(data);
            
            var item = dataContext.Set(data.Data.ElementType).Find(id) as dynamic;
            
            UpdateModel(item);

            dataContext.SaveChanges();


            return RedirectToAction("index", new { type = type });

        }

        public ActionResult Details(string type, int id)
        {
            var data = badmin.GetDataConfiguration(type);

            var dataContext = Badmin.CreateDataContext(data);

            var item = dataContext.Set(data.Data.ElementType).Find(id) as dynamic;

            return View(item);
        }

    }
}
