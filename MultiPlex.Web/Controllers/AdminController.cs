using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiPlex.Web.Controllers
{
    [Export("Admin", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}