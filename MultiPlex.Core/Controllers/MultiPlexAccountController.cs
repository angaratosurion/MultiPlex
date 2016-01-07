using System;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MultiPlex.Core.Application;

namespace MultiPlex.Core.Controllers
{
    [Export("Account", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class MultiPlexAccountController:BlackCogs.Controllers.AccountController
    {



        public MultiPlexAccountController()
        {
            //    this.UserManager = (BlackCogs.Application.ApplicationUserManager)this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //    this.SignInManager=(BlackCogs.Application.ApplicationSignInManager)HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            //
           
        }

       
    }
}
