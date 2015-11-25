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

namespace MultiPlex.Core.Managers
{
    public class WikiUserManager
    {
        // Context db = new Context();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public WikiUserManager(ApplicationUserManager usrmngr, ApplicationSignInManager singmngr)
        {
            this._userManager = usrmngr;
            this._signInManager = singmngr;
        }
        public WikiUserManager()
        {

        }
        Context db = CommonTools.db;
        WikiManager wkmngr = CommonTools.wkmngr;
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
        public void EditUser(string username,ApplicationUser user)
        {
            try
            {
                if ( CommonTools.isEmpty(username)==false && user != null && 
                    this.UserExists(user.UserName)  )
                {
                    db.Entry(this.GetUser(username)).CurrentValues.SetValues(user);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
              
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
       public List<ApplicationUser> GetUsers()
        {
            try
            {
                return this.db.Users.ToList();

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }

        
        public Boolean UserHasAccessToWiki(ApplicationUser user,Wiki wk,Boolean isDelete)
        {
            try
            {
                Boolean ap = false;
                if ( user !=null && UserExists(user.UserName.ToString())==true)
                {
                     if ( isDelete ==true)
                    {
                        IdentityRole role = this.GetRole(AdminRoles);
                         if ( role !=null)
                        {
                             if ( role.Users.First(x=>x.UserId== user.Id)!=null && wk.Administrator.UserName == user.UserName)
                            {
                                ap = true;
                            }
                           
                        }
                    }
                    else
                    {
                        if ( wk.Administrator==user)
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
        public  Boolean RoleExists(string Name)
        {
            try
            {
                Boolean ap = false;
                if (Name != null)
                {
                    IdentityRole rol = this.GetRole(Name);
                   if (rol!=null)
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
        public IdentityRole GetRole(string Name)
        {
            try
            {
                IdentityRole ap = null;
                if (Name != null)
                {
                    List<IdentityRole> rols = this.GetRoles();

                    if (rols != null)
                    {
                        ap = rols.FirstOrDefault(r => r.Name == Name);
                    }
                }
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<IdentityRole> GetRolesOfUser(string UserName)
        {
            try
            {
                List<IdentityRole> ap = null;
                if (UserName != null && this.UserExists(UserName))
                {
                    ApplicationUser usr = this.GetUser(UserName);
                    if ( usr !=null && usr.Roles!=null)
                    {
                        ap = new List<IdentityRole>();
                        foreach(IdentityUserRole rl in usr.Roles)
                        {
                            IdentityRole r = this.db.Roles.FirstOrDefault(x => x.Id == rl.RoleId);
                            ap.Add(r);
                        }
                        
                    }
                }



                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<IdentityRole> GetRoles()
        {
            try
            {
                List < IdentityRole > ap= this.db.Roles.ToList();
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void CreateNewRole(IdentityRole role)
        {
            try
            {
                if ( role !=null &&  this.RoleExists(role.Name)==false)
                {
                    this.db.Roles.Add(role);
                    this.db.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
               // return null;
            }
        }
        public void EditRole(string rolename ,IdentityRole role)
        {
            try
            {
                if ( CommonTools.isEmpty( rolename) ==false && 
                    role != null && this.RoleExists(role.Name))
                {
                    IdentityRole or = this.GetRole(rolename);
                    if (or != null && or.Name!= "Administrators")
                    {
                        this.db.Entry(or).CurrentValues.SetValues(role);
                        this.db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                // return null;
            }
        }
        public void DeleteRole(string rolename)
        {
            try
            {
                if (CommonTools.isEmpty(rolename) == false)
                {
                    IdentityRole or = this.GetRole(rolename);
                    if (or != null && rolename!= "Administrators")
                    {
                        this.db.Roles.Remove(or);
                        this.db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                // return null;
            }
        }
        public List<ApplicationUser> GetUsersofRole(string Name)
        {
            try
            {
                List<ApplicationUser> ap = null;
                if (Name != null && this.RoleExists(Name))
                {
                    IdentityRole rol = this.GetRole(Name);
                     if ( rol !=null && rol.Users !=null && rol.Users.Count>0)
                    {
                        ap = new List<ApplicationUser>();
                        foreach( var u in rol.Users)
                        {
                            ApplicationUser t=this.db.Users.FirstOrDefault(x => x.Id == u.UserId);
                            ap.Add(t);
                        }
                    }
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
   
