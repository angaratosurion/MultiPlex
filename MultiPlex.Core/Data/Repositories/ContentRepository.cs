using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.Repositories
{
    public class ContentRepository
    {
        Context db = new Context();
        WikiRepository wikrerpo = new WikiRepository();
        TitleRepository titlerp = new TitleRepository();
        public int CountWithTitleId(string wikiname, int tid)
        {
            try
            {
                int ap = 0;
                if ((wikiname != null) && (tid > 0))
                {
                    ap = this.GetByTitle(wikiname, tid).ToList().Count;


                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return -1;
            }
        }
        public Content Get(String wikiname, int id)
        {
            try
            {
                Content ap = null;
                if ((wikiname != null) && (id > 0))
                {
                    ap = db.Content.First(s => s.Id == id);


                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public Content Get(String wikiname, string slug, string title)
        {
            try
            {
                Content ap = null;
                if ((wikiname != null) && (title != null) && slug != null)
                {
                    ap = db.Content.First(s => s.Title.Name == title && s.Title.Slug == slug);


                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<Content> GetByTitle(String wikiname, int tid)
        {
            try
            {
                List<Content> ap = null;
                if ((wikiname != null) && (tid > 0))
                {
                    ap = db.Content.Where(s => s.Title.Id == tid).ToList();


                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void SaveorAdd(string wikiname, int tid, string source, ApplicationUser user)
        {
            try
            {
                if (wikiname != null && tid > 0 && source != null)
                {

                    Content cont;
                    cont = new Content();
                    Title title = this.titlerp.Get(wikiname, tid);
                    if (title != null)
                    {
                        //  cont.Id = id;
                        cont.Title = title;
                        cont.Source = source;
                        cont.Wiki = this.db.Wikis.FirstOrDefault(w => w.WikiName == wikiname);
                        cont.WrittenBy = user;
                        if (this.CountWithTitleId(wikiname, tid) > 0)
                        {
                            cont.Version = this.CountWithTitleId(wikiname, tid) + 1;
                        }
                        else
                        {
                            cont.Version = 1;
                        }
                        cont.VersionDate = DateTime.Now;
                        this.db.Content.Add(cont);


                        this.db.SaveChanges();
                    }
                }



            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return null;
            }

        }
        public Content GetByVersion(string wikiname, int id, int version)
        {
            try
            {
                Content ap = null;
                if(wikiname !=null && this.wikrerpo.WikiExists(wikiname) && id >0  && version>0)
                {
                    ap = this.db.Content.First(s => s.Title.Id == id && s.Version == version && s.Wiki.WikiName == wikiname);
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
