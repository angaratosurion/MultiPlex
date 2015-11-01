using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiPlex.Core.Managers;

namespace MultiPlex.Web.Controllers
{
    [Export("Admin", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            return View();
        }
    }
}