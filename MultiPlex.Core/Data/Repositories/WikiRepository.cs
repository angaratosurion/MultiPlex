using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.Repositories
{
    public class WikiRepository
    {
        //Context db = new Context();
        Context db = new Context();
        #region Wiki
        public Wiki GetWiki(string wikiname)
        {

            try
            {
                Models.Wiki ap = null;
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
        public bool WikiExists(string wikiname)
        {
            try
            {
                bool ap = false;
                if ( wikiname !=null)
                {
                     if (this.GetWiki(wikiname)!=null)
                    {
                        ap = true;
                    }
                }


                return false;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return false;
            }
        }
        public List<Models.Wiki> ListWiki()
        {
            try
            {
                return db.Wikis.ToList();

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void CreateWiki()
        {

        }

        #endregion
        #region Title
        public List<Content> GetHistory(int titleid)
        {
            try
            {
                List<Content> ap = null;
                if ((titleid > 0))
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
        public Title Get(string wikiname, int titleid)
        {
            try
            {
                Title ap = null;
                if (wikiname != null && (titleid > 0))
                {
                    ap = db.Title.FirstOrDefault(t => t.Id == titleid && t.Wiki.WikiName == wikiname);
                }
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public Title Add(string wikiname, string name, string slug, ApplicationUser user)
        {
            try
            {
                Title ap = null;

                if (wikiname != null && slug != null)
                {
                    Models.Wiki wiki = this.GetWiki(wikiname);
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

        #endregion
        #region Content

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
        public Content GetContent(String wikiname, int id)
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
        public Content GetContent(String wikiname, string slug, string title)
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
        public void SaveorAddContent(string wikiname, int tid, string source, ApplicationUser user)
        {
            try
            {
                if (wikiname != null && tid > 0 && source != null)
                {

                    Content cont;
                    cont = new Content();
                    Title title = Get(wikiname, tid);
                    if (title != null)
                    {
                        //  cont.Id = id;
                        cont.Title = title;
                        cont.Source = source;
                        cont.Wiki = db.Wikis.FirstOrDefault(w => w.WikiName == wikiname);
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
                        db.Content.Add(cont);


                        db.SaveChanges();
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
                if (wikiname != null && this.WikiExists(wikiname) && id > 0 && version > 0)
                {
                    ap = db.Content.First(s => s.Title.Id == id && s.Version == version && s.Wiki.WikiName == wikiname);
                }




                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;

            }

        }
        #endregion
    }
}
