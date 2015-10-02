using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.Repositories;

namespace MultiPlex.Core.Managers
{
  public  class CategoryManager
    {
        Context db = new Context();
        WikiRepository rp = new WikiRepository();

        public void Add(Category cat)
        {
            try
            {

                if ( cat !=null)
                {
                    rp.CreateCategory(cat);
                    
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
