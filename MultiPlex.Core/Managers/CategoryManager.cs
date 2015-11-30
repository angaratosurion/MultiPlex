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
        public List<WikiCategory> GetCategories()
        {
            try
            {
                List<WikiCategory> ap = null;
                ap = this.rp.GetCategories();
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;

            }
        }
        public void Add(WikiCategory cat)
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
        public List<WikiCategory> GetCategoryListByWiki(string wikiname)
        {

            try
            {
                List<WikiCategory> ap = null;

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
        public WikiCategory GetCategoryById(int id)
        {

            try
            {
                WikiCategory ap = null;

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
        public void EditCatrgory(string wikiname,int id , WikiCategory cat)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) == false && id >0 && cat !=null)
                {
                    this.rp.EditCategory(id, wikiname, cat);
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public void DeleteCatrgory( int id)
        {
            try
            {
                if ( id > 0)
                {
                    this.rp.DeleteCategoryById(id);
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public void AddTitleToCategory(string wikiname,int catid,WikiTitle title)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) == false && catid > 0 && title != null
                     )
                {
                    this.rp.AddTitleToCategory(wikiname, catid, title);
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
  
    public void RemoveTitleFromCategory(string wikiname, int catid, WikiTitle title)
    {
        try
        {
            if (CommonTools.isEmpty(wikiname) == false && catid > 0 && title != null
                 )
            {
                this.rp.RemoveTitleFromCategory (wikiname, catid, title);
            }//
        }
        catch (Exception ex)
        {

            CommonTools.ErrorReporting(ex);

        }
    }
}
}
