using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
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

            var config = badmin.GetDataConfiguration(type); //we get a data config, based on the 'type' string

            var dataContext = badmin.CreateDataContext(type); // create a datacontext for this particlar type.

            var dbSet = dataContext.Set(config.ElementType); //create a dbSet, would _really_ prefer DbSet<T>

            //because dbSet is IQueryable (non, generic),  I don't have access to skip/take, etc. ext. methods.
            
            //hence the below fails..
            var list = dbSet.ToPagedList(page, pageSize);


            return View(list);
        }

      
        public ActionResult Edit(string type, int id)
        {

            //ugly, fix trhis
            var data = badmin.GetDataConfiguration(type);

            var dataContext = Badmin.CreateDataContext(data);

            var item = dataContext.Set(data.ElementType).Find(id);
            
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
            
            var item = dataContext.Set(data.ElementType).Find(id) as dynamic;
            
            UpdateModel(item);

            dataContext.SaveChanges();


            return RedirectToAction("index", new { type = type });

        }

        public ActionResult Details(string type, int id)
        {
            var data = badmin.GetDataConfiguration(type);

            var dataContext = Badmin.CreateDataContext(data);

            var item = dataContext.Set(data.ElementType).Find(id) as dynamic;

            return View(item);
        }

    }
}
