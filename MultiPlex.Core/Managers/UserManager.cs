using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MultiPlex.Core.Data;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Managers
{
    public class UserManager
    {
        // Context db = new Context();
        Context db = CommonTools.db;
        public static string AdminRoles = "Administrators";
        #region User
        public ApplicationUser GetUser (string id)
        {
            try
            {
                ApplicationUser ap = null;
                if ( id !=null)
                {
                    ap = this.db.Users.First(u => u.UserName == id);
                }
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
         public Boolean UserExists(string id)
        {
            try
            {
                Boolean ap = false;
                if (id != null)
                {
                    ApplicationUser us = this.GetUser(id);
                    if ( us !=null)
                    {
                        ap = true;
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
    
        public Boolean UserHasAccessToWiki(ApplicationUser user,Wiki wk,Boolean isDelete)
        {
            try
            {
                Boolean ap = false;
                if ( user !=null && this.UserExists(user.Id)==true)
                {
                     if ( isDelete ==true)
                    {
                        IdentityRole role = this.GetRole(AdminRoles);
                         if ( role !=null)
                        {
                             if ( role.Users.First(x=>x.UserId== user.Id)!=null && wk.Administrtor == user)
                            {
                                ap = true;
                            }
                           
                        }
                    }
                    else
                    {
                        if ( wk.Administrtor==user)
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
        #endregion
        #region roles

        public IdentityRole GetRole(string Name)
        {
            try
            {
                IdentityRole ap = null;
                if (Name != null)
                {
                    ap = this.db.Roles.First(x => x.Name == Name);
                }
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }

        #endregion
    }
}
   
