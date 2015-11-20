using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MultiPlex.Core;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.ViewModels;
using MultiPlex.Core.Managers;
using MultiPlex.Web.Models;

namespace MultiPlex.Web.Controllers
{
    [Export("WikiUser", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class WikiUserController : Controller
    {
        UserManager usremngr = new UserManager();
        WikiManager wkmngr = CommonTools.wkmngr;
        #region AdminPanel
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            var usrs = this.usremngr.GetUsers();
          
            return View(usrs.ToList());
        }
        [Authorize(Roles = "Administrators")]
        public ActionResult FullDetails(string username)
        {
            try
            {


                if (CommonTools.isEmpty(username) == true)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                ApplicationUser adm = this.usremngr.GetUser(username);
                if (adm == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                ViewFullUserDetails fulusr = new ViewFullUserDetails();
                fulusr.UserDetails = adm;
                fulusr.Roles = this.usremngr.GetRolesOfUser(username);
                fulusr.WikisAsAdmin= this.wkmngr.ListWikiByAdmUser(username);
                fulusr.WikisAsMod = this.wkmngr.ListWikiByModUser(username);

                return View(fulusr);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [Authorize(Roles = "Administrators")]
        public ActionResult EditUserDetails (string username)
        {
            try
            {
                if (CommonTools.isEmpty(username) == true)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                ApplicationUser adm = this.usremngr.GetUser(username);
                if (adm == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }
              
                return View(adm);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserDetails(ApplicationUser user)
        {
            try
            { if (ModelState.IsValid)
                {
                    this.usremngr.EditUser(user.UserName,user);
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(string username)
        {
            try
            {
                if (CommonTools.isEmpty(username) == true)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicationUser user = this.usremngr.GetUser(username);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewFullUserDetails fulusr = new ViewFullUserDetails();
                fulusr.UserDetails = user;
                fulusr.Roles = this.usremngr.GetRolesOfUser(username);
                fulusr.WikisAsAdmin = this.wkmngr.ListWikiByAdmUser(username);
                fulusr.WikisAsMod = this.wkmngr.ListWikiByModUser(username);


                return View(fulusr);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        // POST: ProjectNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string username)
        {

            try
            {
                int cat = 0;
                if (CommonTools.isEmpty(username) == true)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicationUser user = this.usremngr.GetUser(username);
                if (user != null)
                {
                    this.wkmngr.DeleteWikiByAdm(user.UserName);
                }
               
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        #endregion
        #region WikiUserEdit


        public ActionResult GetWikiUsers(string wikiname)
        {
            try
            {


                if (CommonTools.isEmpty(wikiname) == true)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                List<ApplicationUser> modusr = this.wkmngr.GetWikiModerators(wikiname);
                ApplicationUser adm = this.wkmngr.GetWikiAdministrator(wikiname);
                if (adm == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }
                ViewWikiUsers vwkus = new ViewWikiUsers();

                vwkus.Administrator = adm;
                vwkus.Moderators = modusr;

                return View(vwkus);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public ActionResult Details(string username)
        {
            try
            {


                if (CommonTools.isEmpty(username) == true)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                ApplicationUser adm = this.usremngr.GetUser(username);
                if (adm == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }


                return View(adm);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }
}