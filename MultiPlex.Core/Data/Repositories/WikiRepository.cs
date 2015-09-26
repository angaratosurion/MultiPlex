using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.Repositories
{
    public class WikiRepository:TitleRepository
    {
        Context db = new Context();
        public WikiModel Get(string wikiname)
        {

            try
            {
                WikiModel ap = null;
                if (wikiname != null)
                {
                    ap = db.Wikis.FirstOrDefault(w => w.WikiName == wikiname);
                }
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
    }
}
