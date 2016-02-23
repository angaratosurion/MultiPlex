using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.Repositories;
using BlackCogs.Data.Models;
namespace MultiPlex.Core.Managers
{
   public class WikiModeratorsInvitesManager
    {
        WikiRepository wrepo = new WikiRepository();
        WikiManager wkmngr = CommonTools.wkmngr;
        WikiUserManager usrmngr = CommonTools.usrmng;

        public List<WikiModInvitations> GetModeratorInvitesByWiki(string wikiname)
        {
            try
            {
                return  this.wrepo.GetWikiModInvitesbyWiki(wikiname);


               

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public WikiModInvitations GetModeratorInviteById(string wikiname,int id)
        {
            try
            {
                return this.wrepo.GetWikiModInvitebyId(wikiname,id);




            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void DeleteModeratorInvite(string wikiname, int invid, string adm)
        {
            try
            {
                if (!CommonTools.isEmpty(wikiname)
                     && !CommonTools.isEmpty(adm) && invid > 0)
                {
                    Wiki wk = wkmngr.GetWiki(wikiname);
                    ApplicationUser usr = this.usrmngr.GetUser(adm);
                    if (wk != null && usr != null && usrmngr.UserHasAccessToWiki(usr, wk, true))
                    {

                        this.wrepo.DeleteModInviteById(wikiname, invid);
                    }
                }


            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
               
            }
        }
        public List<WikiModInvitations> GetModeratorInvites()
        {
            try
            {
                return this.wrepo.GetWikiModInvites();




            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void CreateModRequest(string wikiname, WikiModInvitations inv )
        {
            try
            {
                if (!CommonTools.isEmpty(wikiname)
                     &&inv!=null )
                {
                    Wiki wk = wkmngr.GetWiki(wikiname);
                    ApplicationUser usr = this.usrmngr.GetUserbyID(inv.Moderator);
                    if (wk!=null && usr !=null)
                    {
                        this.wrepo.CreateNewModInvite(wikiname, usr.UserName, inv);
                    }

                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }

        }
        public void AcceptModRequest(string wikiname, int invid,string adm)
        {
            try
            {
                if (!CommonTools.isEmpty(wikiname)
                     && !CommonTools.isEmpty(adm) &&invid >0)
                {
                    Wiki wk = wkmngr.GetWiki(wikiname);
                    ApplicationUser usr = this.usrmngr.GetUser(adm);
                    if (wk != null && usr != null && usrmngr.UserHasAccessToWiki(usr, wk, true))
                    {
                        WikiModInvitations inv = this.GetModeratorInviteById(wikiname, invid);
                        if (inv != null)
                        {

                            if (wk.Moderators != null)
                            {
                                wk.Moderators.Add(inv.Moderator);
                            }
                            else
                            {
                                wk.Moderators = new List<string>();
                                wk.Moderators.Add(inv.Moderator);

                            }
                            wkmngr.EditBasicInfo(wk, wikiname);

                            this.DeleteModeratorInvite(wikiname, invid, adm);
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }

        }
        public void RejectModRequest(string wikiname, int invid, string adm)
        {
            try
            {
                if (!CommonTools.isEmpty(wikiname)
                     && !CommonTools.isEmpty(adm) && invid > 0)
                {
                    Wiki wk = wkmngr.GetWiki(wikiname);
                    ApplicationUser usr = this.usrmngr.GetUser(adm);
                    if (wk != null && usr != null && usrmngr.UserHasAccessToWiki(usr, wk, true))
                    {
                        WikiModInvitations inv = this.GetModeratorInviteById(wikiname, invid);
                        if (inv != null)
                        {
                            this.DeleteModeratorInvite(wikiname, invid, adm);
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }

        }

    }
}
