using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Badmin.Areas.Admin.Controllers
{
    public abstract class BadminBaseController : Controller
    {
        protected readonly IBadmin badmin;

        public BadminBaseController(IBadmin badmin)
        {
            this.badmin = badmin;
        }


    }
}