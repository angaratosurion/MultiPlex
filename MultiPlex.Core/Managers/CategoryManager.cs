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
        public List<Category> GetCategoryListByWiki(string wikiname)
        {

            try
            {
                List<Category> ap = null;

                if (CommonTools.isEmpty(wikiname)== false)
                {
                    ap = rp.GetCategorybyWiki(wikiname); 


                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
               return null;
            }

        }
        public Category GetCategoryListById(int id)
        {

            try
            {
                Category ap = null;

                if (id>0)
                {
                    ap = rp.GetCategorybyId(id);


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
