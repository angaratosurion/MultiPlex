using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MultiPlex.Core.Application;
using MultiPlex.Core.Data;
using MultiPlex.Core.Data.Models;
using BlackCogs.Data.Models;
using BlackCogs.Managers;

namespace MultiPlex.Core.Managers
{
    public class WikiUserManager: BlackCogsUserManager
    {

        // Context db = new Context();
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        //public WikiUserManager(ApplicationUserManager usrmngr, ApplicationSignInManager singmngr)
        //{
        //    this._userManager = usrmngr;
        //    this._signInManager = singmngr;
        //}
        //public WikiUserManager()
        //{

        //}
        Context db = CommonTools.db;
        WikiManager wkmngr = CommonTools.wkmngr;
        public Boolean UserHasAccessToWiki(ApplicationUser user, Wiki wk, Boolean isDelete)
        {
            try
            {
                Boolean ap = false;
                if (user != null && UserExists(user.UserName.ToString()) == true)
                {
                    if (isDelete == true)
                    {
                        IdentityRole role = this.GetRole(AdminRoles);
                        if (role != null)
                        {
                            if (role.Users.First(x => x.UserId == user.Id) != null && wk.Administrator.UserName == user.UserName)
                            {
                                ap = true;
                            }

                        }
                    }
                    else
                    {
                        if (wk.Administrator == user)
                        {
                            ap = true;
                        }
                    }
                }


                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return false;
            }
        }
    }

}

   
