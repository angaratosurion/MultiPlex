using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiPlex.Web.Controllers
{
    [HandleError]
    [Export("Home", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : Core.Controllers.HomeController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}