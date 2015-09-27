using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.Repositories;

namespace MultiPlex.Core.Managers
{
 public   class WikiManager
    {
        WikiRepository wrepo = new WikiRepository();

        public List<WikiModel> ListWiki()
        {
            try
            {
                return this.wrepo.ListWiki();

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
    }
}
