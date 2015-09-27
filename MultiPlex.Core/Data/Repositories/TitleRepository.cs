using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.Repositories
{
    public class TitleRepository:ContentRepository
    {
        Context db = new Context();
        WikiRepository wrepo = new WikiRepository();
        public List<Content> GetHistory(int titleid)
        {
            try
            {
                List<Content> ap = null;
                if ((titleid> 0))
                {
                    ap = db.Content.Where(s => s.Title.Id == titleid).ToList();


                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public Title Get(string wikiname,int titleid )
        {
            try
            {
                Title ap = null;
                if ( wikiname !=null && (titleid>0))
                {
                   ap= db.Title.FirstOrDefault(t => t.Id == titleid && t.Wiki.WikiName == wikiname);
                }
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public Title Add(string wikiname,string name,string slug,ApplicationUser user)
        {
            try
            {
                Title ap = null;

                if (wikiname != null && slug!=null)
                {
                    WikiModel wiki = this.wrepo.GetWiki(wikiname);
                    ap = new Title();
                    ap.Name = name;
                    ap.Slug = slug;
                    ap.Wiki = wiki;
                    ap.WrittenBy = user;
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
