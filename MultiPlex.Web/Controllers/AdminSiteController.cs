using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiPlex.Core.Managers;

namespace MultiPlex.Web.Controllers
{
    [Export("AdminSite", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AdminSiteController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            return View();
        }
    }
}