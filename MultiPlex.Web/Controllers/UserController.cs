using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiPlex.Core;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Managers;

namespace MultiPlex.Web.Controllers
{
    [Export("WikiUser", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class WikiUserController : Controller
    {
        UserManager usremngr = new UserManager();
        WikiManager wkmngr = CommonTools.wkmngr;
        [Authorize(Roles ="Adminsitrator")]
        public ActionResult Index()
        {
            var usrs = this.usremngr.GetUsers();
            return View();
        }
        public ActionResult GetWikiUsers(string wikiname)
        {
            try
            {


                 if (CommonTools.isEmpty(wikiname)==true)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                List<ApplicationUser> modusr = this.wkmngr.GetWikiModerators(wikiname);
                ApplicationUser adm = this.wkmngr.GetWikiAdministrator(wikiname);
                if ( adm == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }


                
                return View();
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}