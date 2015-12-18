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

namespace MultiPlex.Core.Controllers
{
    [Export("WikiUser", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class WikiUserController : Controller
    {
        WikiUserManager usremngr= new WikiUserManager();
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
        [Authorize(Roles = "Administrators")]
        public ActionResult GetRoles()
        {
            var usrs = this.usremngr.GetRoles();

            return View(usrs.ToList());
        }
        [Authorize(Roles = "Administrators")]
        public ActionResult CreateNewRole()
        {
            return View();
        }
        [Authorize(Roles = "Administrators")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult CreateNewRole(IdentityRole model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                 
                    usremngr.CreateNewRole(model);
                 // if (result.Succeeded)
                    {
                        
                        return RedirectToAction("GetRoles");
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
        [Authorize(Roles = "Administrators")]
        public ActionResult RoleDetails(string rolename)
        {
            try
            {


                if (CommonTools.isEmpty(rolename) == true)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                IdentityRole adm = this.usremngr.GetRole(rolename);
                 if (adm==null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                ViewIdentityRoleDetails mod = new ViewIdentityRoleDetails();
                mod.Role = adm;
                mod.Members = this.usremngr.GetUsersofRole(rolename);
               
                return View(mod);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [Authorize(Roles = "Administrators")]
        public ActionResult EditRoleDetails(string rolename)
        {
            try
            {
                if (CommonTools.isEmpty(rolename) == true)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                IdentityRole adm = this.usremngr.GetRole(rolename);
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
        [Authorize(Roles = "Administrators")]
        public ActionResult EditRoleDetails(IdentityRole user, string rolename)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.usremngr.EditRole(rolename, user);
                    return RedirectToAction("GetRoles");
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
        public ActionResult DeleteRole(string rolename)
        {
            try
            {
                if (CommonTools.isEmpty(rolename) == true)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                IdentityRole adm = this.usremngr.GetRole(rolename);
                if (adm == null)
                {
                    return HttpNotFound();
                }
                ViewIdentityRoleDetails rolddetails = new ViewIdentityRoleDetails();
                rolddetails.Role = adm;
                List<ApplicationUser> membs = this.usremngr.GetUsersofRole(rolename);
                 if ( membs == null)
                {
                    membs = new List<ApplicationUser>();
                }
                rolddetails.Members = membs;


                return View(rolddetails);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        // POST: ProjectNews/Delete/5
        [HttpPost, ActionName("DeleteRole")]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleConfirmed(string rolename)
        {

            try
            {
                int cat = 0;
                if (CommonTools.isEmpty(rolename) == true)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                IdentityRole adm = this.usremngr.GetRole(rolename);
                if (adm != null)
                {
                    this.usremngr.DeleteRole(rolename);
                }

                return RedirectToAction("GetRoles");
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [Authorize(Roles = "Administrators")]
        public ActionResult AddUserToRole(string rolename)
        {
            ViewAddUserToRole model = new ViewAddUserToRole();
            List<ApplicationUser> usr = this.usremngr.GetUsers();
            IdentityRole rol = this.usremngr.GetRole(rolename);
            model.Role = rol;
            model.Users = usr;
            ViewBag.Users = new SelectList(usr, "UserName");
            return View(model);
        }
        [Authorize(Roles = "Administrators")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserToRole(ViewAddUserToRole model,string rolename)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string UserToAdd;
                  if ( rolename != null  && model !=null && model.UserToAdd!=null)
                        //&& CommonTools.isEmpty( con["UserToAdd"])==false)
                    {

                        UserToAdd = model.UserToAdd.UserName;
                        this.usremngr.AddUserToRole( rolename, UserToAdd);

                        RouteValueDictionary vals = new RouteValueDictionary();
                        vals.Add("rolename", rolename);
                        return RedirectToAction("RoleDetails", vals);
                    }
                    //AddErrors(result);
                }

                // If we got this far, something failed, redisplay form
                return View();
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [Authorize(Roles = "Administrators")]
        public ActionResult RemoveUserFromRole(string rolename,string username)
        {
            try
            {
                if (CommonTools.isEmpty(rolename) == true)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (CommonTools.isEmpty(username) == true)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                IdentityRole adm = this.usremngr.GetRole(rolename);
                ApplicationUser user = this.usremngr.GetUser(username);
                if (adm == null)
                {
                    return HttpNotFound();
                }
               
                ViewDeleteUserFromRole rolddetails = new ViewDeleteUserFromRole();
                rolddetails.Role = adm;
                rolddetails.UserToDelete=user;
                


                return View(rolddetails);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        // POST: ProjectNews/Delete/5
        [HttpPost, ActionName("RemoveUserFromRole")]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUserFromRoleConfirmed(string rolename, string username)
        {

            try
            {
                int cat = 0;
                if (CommonTools.isEmpty(rolename) == true)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                IdentityRole adm = this.usremngr.GetRole(rolename);
                ApplicationUser user = this.usremngr.GetUser(username);
               
                if (CommonTools.isEmpty(username) == true)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (adm != null && user != null)
                {
                    this.usremngr.RemoveUserFromRole(rolename, username);
                }

                return RedirectToAction("GetRoles");
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