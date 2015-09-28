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
        public List<Title> GetTitlesbyCategory(string wikiname,int catid)
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
    }
}
