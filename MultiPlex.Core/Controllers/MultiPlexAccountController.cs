using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
        }

        public MultiPlexAccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
    }
}
