using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Badmin
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.Data.Entity.Database.SetInitializer(new DatabaseInit());

        }
    }

    public class DatabaseInit : System.Data.Entity.DropCreateDatabaseIfModelChanges<Models.Data.DatabaseContext>
    {
        protected override void Seed(Models.Data.DatabaseContext context)
        {
            context.Forums.Add(new Models.Forum
            {
                Name = "Sample Forum 1"
            });
            context.Forums.Add(new Models.Forum
            {
                Name = "Sample Forum 2"
            });
            context.Forums.Add(new Models.Forum
            {
                Name = "Sample Forum 3"
            });
            context.Forums.Add(new Models.Forum
            {
                Name = "Sample Forum 4"
            });
            context.Forums.Add(new Models.Forum
            {
                Name = "Sample Forum 5"
            });

            context.SaveChanges();


            base.Seed(context);
        }
    
	


    }

}