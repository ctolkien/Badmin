using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Badmin.Areas.Admin.Controllers
{
    public class ListController : BadminBaseController 
    {
        

        public ListController(IBadmin badmin) : base(badmin)
        {
        }

        public class ListThingy<T> where T : class
        {
            private readonly DbContext _ctx;
            public ListThingy(DbContext ctx)
            {
                _ctx = ctx;
            }

            public IPagedList Index(int page = 1, int pageSize = 25)
            {
                return _ctx.Set<T>()
                    .OrderBy(x => x)
                    .ToPagedList(page, pageSize);
            }
        }

        // GET: /Admin/List/
        public ActionResult Index(string type, int page = 1)
        {
         
            const int pageSize = 3;

            var dataContext = badmin.CreateDataContext(type);

            var config = badmin.GetDataConfiguration(type); //we get a data config, based on the 'type' string

            var t = typeof(ListThingy<>);
            var gt = t.MakeGenericType(new[] { config.ElementType });
            var ctor = gt.GetConstructor(new[] { config.DbContextType });
            dynamic obj = ctor.Invoke(new[] { dataContext });

            
            var dbSet = dataContext.Set(config.ElementType); //create a dbSet, would _really_ prefer DbSet<T>

            //because dbSet is IQueryable (non, generic),  I don't have access to skip/take, etc. ext. methods.

            //hence the below fails..
            //var list = obj.ToPagedList(page, pageSize);

            var list = obj.Index();

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
