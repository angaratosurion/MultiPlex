using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.Repositories;

namespace MultiPlex.Core.Managers
{
   public class TitleManager
    {
        WikiRepository wrepo = new WikiRepository();
        //UserManager usrmngr = new UserManager();

        public List<WikiTitle> GetTitlesbyCategory(string wikiname,int catid)
        {
            try
            {
                return wrepo.GetTitleByCategory(wikiname, catid);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public WikiTitle GetTitlebyId(string wikiname, int id)
        {
            try
            {
                return wrepo.Get(wikiname, id);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
       
        public void Delete(WikiTitle title,ApplicationUser user)
        {

            try
            {
                if (title != null && user != null)
                {
                    Wiki wk = CommonTools.wkmngr.GetWiki(title.Wiki.Name);
                    if ( wk !=null  && CommonTools.usrmng.UserHasAccessToWiki(user,wk,true))
                    {
                        wrepo.DeleteTitleById(wk.Name, title.Id);
                    }
                    
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                // return null;
            }
        }
    }
}
