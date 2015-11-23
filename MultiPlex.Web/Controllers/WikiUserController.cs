using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity.Owin;
using MultiPlex.Core;
using MultiPlex.Core.Application;
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
        WikiUserManager usremngr = new WikiUserManager();
        WikiManager wkmngr = CommonTools.wkmngr;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
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
      //  [AllowAnonymous]
        [Authorize(Roles = "Administrators")]
        public ActionResult CreateNewUser()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "Administrators")]
        //  [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewUser(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                      //  await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", " WikiUser");
                    }
                    //AddErrors(result);
                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }


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