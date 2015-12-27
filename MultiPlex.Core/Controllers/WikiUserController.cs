using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MultiPlex.Core;
using MultiPlex.Core.Application;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.ViewModels;
using MultiPlex.Core.Managers;
using MultiPlex.Core.Data.ViewModels.Identity;
using BlackCogs.Data.ViewModels.Identity;
using BlackCogs.Data.Models;
using BlackCogs.Data.ViewModels;

namespace MultiPlex.Core.Controllers
{
    [Export("WikiUser", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class WikiUserController : BlackCogs.Controllers.UserController
    {
        WikiUserManager usremngr= new WikiUserManager();
        WikiManager wkmngr = CommonTools.wkmngr;
        
        #region AdminPanel
     
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
        public ActionResult DeleteUser(string username)
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
        [HttpPost, ActionName("DeleteUser")]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(string username)
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
      //  [AllowAnonymous]
     
       
        #endregion
        #region WikiUserEdit
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