using System.Web.Mvc;
using Munq.MVC3;
using Badmin.Models.Data;
using Munq.LifetimeManagers;
using Badmin.Models;

[assembly: WebActivator.PreApplicationStartMethod(
    typeof(Badmin.App_Start.MunqMvc3Startup), "PreStart")]

namespace Badmin.App_Start
{
    public static class MunqMvc3Startup
    {
        public static void PreStart()
        {

            DependencyResolver.SetResolver(new MunqDependencyResolver());

            var ioc = MunqDependencyResolver.Container;

            var badmin = new Badmin();

            badmin.Register<DatabaseContext, Forum>(x => x.Forums);

            ioc.Register<IBadmin>(x => badmin).WithLifetimeManager(new RequestLifetime());





        }
    }
}


