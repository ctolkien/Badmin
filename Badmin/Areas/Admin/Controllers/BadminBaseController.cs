using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Badmin.Areas.Admin.Controllers
{
    public abstract class BadminBaseController<T> : Controller 
    {
        protected readonly DbContext dataContext;

        public BadminBaseController(DbContext dataContext)
        {
            this.dataContext = dataContext;
        }


        //we want to get all the public IDBSets


        public IEnumerable<string> GetListOfTables()
        {

            //var properties = typeof(T).GetProperties().Where(x => x.PropertyType == typeof(DbSet<Badmin.Models.Forum>));


            //return properties.Select(x => x.Name);

            return new[] { "asdasd" };
            
        }



    }
}