using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.Repositories
{
    public class WikiRepository
    {
        //Context db = new Context();
        // Context db = new Context();
        Context db = CommonTools.db;
        #region Wiki
        //public void MarkWikiAsUpdated(string wikiname)
        //{
        //    try
        //    {
        //        if ( this.WikiExists(wikiname)==true)
        //        {
        //            Wiki wk = this.GetWiki(wikiname);
        //            wk.UpdatedAt = DateTime.Now;
        //         db.Entry(this.GetWiki(wikiname)).CurrentValues.SetValues(wk);
        //            db.Entry(wk).State = EntityState.Modified;
        //            this.db.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        CommonTools.ErrorReporting(ex);
        //    }
        //}
        public void MarkWikiAsUpdated(Wiki wk)
        {
            try
            {

                if (wk != null)
                {
                    wk.UpdatedAt = DateTime.Now;
                    db.Entry(this.GetWiki(wk.Name)).CurrentValues.SetValues(wk);
                   // db.Entry(wk).State = EntityState.Modified;
                   this.db.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
            }
        }
        public Wiki GetLatestUpdatedWiki()
        {

            try
            {
                Models.Wiki ap = null;
                List<Wiki> wks = this.ListWiki();
                List<Wiki> orwks;
                if (wks != null)
                {
                    orwks = wks.OrderByDescending(x => x.UpdatedAt).ToList();
                    if ( orwks !=null)
                    {
                        ap = orwks[0];
                    }

                    //ap = db.Wikis.FirstOrDefault(w => w.Name == wikiname);
                }
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public Wiki GetWiki(string wikiname)
        {

            try
            {
                Models.Wiki ap = null;
                if (wikiname != null)
                {
                   ap = db.Wikis.FirstOrDefault(w => w.Name == wikiname);
                  //  ap = db.Wikis.AsNoTracking().FirstOrDefault(w => w.Name == wikiname);
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


                return ap;

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
        public List<Models.Wiki> ListWikiByAdmUser(string username)
        {
            try
            {
                List<Models.Wiki> ap = null;
                if ( CommonTools.isEmpty(username)==false)
                {
                    ap=this.db.Wikis.Where(s => s.Administrator.UserName == username).ToList();

                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<Models.Wiki> ListWikiByModUser(string username)
        {
            try
            {
                List<Models.Wiki> ap = null;
                if (CommonTools.isEmpty(username) == false)
                {


                    ApplicationUser  usr =this.db.Users.FirstOrDefault(u => u.UserName==username);

                    List<Wiki> wks = this.ListWiki();

                    if ( wks!=null)
                    {
                        ap = new List<Wiki>();
                         foreach( Wiki w in wks )
                        {
                             if ( w.Moderators.Contains(usr))
                            {
                                ap.Add(w);
                            }
                        }
                    }
                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void CreateWiki(Wiki wk)
        {
            try
            {
                if ( wk!=null && this.WikiExists(wk.Name)==false)
                {
                    wk.UpdatedAt = DateTime.Now;
                     if (  CommonTools.isEmpty(wk.WikiTitle))
                    {
                        wk.WikiTitle = wk.Name;
                    }
                    this.db.Wikis.Add(wk);
                    this.db.SaveChanges();
                }
            }
            catch (ValidationException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return null;
            }

        }
        public void EditWikiBasicInfo(Wiki wk,string wikiname)
        {
            try
            {
                if (wk != null && CommonTools.isEmpty(wikiname)==false)
                {
                    //if (wk.Name == wikiname)
                    {


                        Wiki wk2 = this.GetWiki(wikiname);
                        wk.Administrator = wk2.Administrator;

                        // db.Entry(wk).State = EntityState.Modified;
                        this.MarkWikiAsUpdated(wk);
                        db.Entry(this.GetWiki(wikiname)).CurrentValues.SetValues(wk);
                        db.SaveChanges();
                       
                    }
                }
            }
            catch(ValidationException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return null;
            }
        }
        
        public void DeleteWiki(string wikiname)
        {
            try
            {
                if (wikiname != null)
                {
                    this.DeleteTitleByWiki(wikiname);
                    this.DeleteCategoryByWiki(wikiname);
                    Wiki wk = this.GetWiki(wikiname);
                    this.db.Wikis.Remove(wk);
                    this.db.SaveChanges();
                       
                }

            }
            catch (ValidationException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return null;
            }
        }
        public void DeleteWikiByAdm(string username)
        {
            try
            {
                if (username != null)
                {
                    List<Wiki> wks = this.ListWikiByAdmUser(username);
                    if ( wks !=null)
                    {
                         foreach( Wiki w in wks)
                        {
                            this.DeleteWiki(w.Name);
                        }
                    }

                }

            }
            catch (ValidationException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return null;
            }
        }

        #endregion
        #region Title
        public List<WikiTitle> GetTitlebyWiki(string wikiname)
        {
            try
            {
                List<WikiTitle> ap = null;
                if ( !CommonTools.isEmpty(wikiname ) && this.WikiExists(wikiname))
                {
                    ap = this.db.Title.Where(t => t.Wiki.Name == wikiname).ToList();
                }


                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
       public List<WikiTitle> GetTitleByCategory(string wikiname,int catid)
        {
            try
            {
                List<WikiTitle> ap = null;
                 if (CommonTools.isEmpty(wikiname)==false && catid>0 && this.WikiExists(wikiname))
                {
                    WikiCategory cat = this.GetCategorybyId(catid);
                    if (cat != null)
                    {
                        ap = this.GetTitlebyWiki(wikiname).Where(t => t.Categories.Contains(cat)).ToList();
                    }

                  
                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public WikiTitle Get(string wikiname, int titleid)
        {
            try
            {
                WikiTitle ap = null;
                if (wikiname != null && (titleid > 0))
                {
                    ap = db.Title.FirstOrDefault(t => t.Id == titleid && t.Wiki.Name == wikiname);
                }
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public WikiTitle Add(string wikiname, string name, string slug, ApplicationUser user)
        {
            try
            {
                WikiTitle ap = null;

                if (wikiname != null)// && slug != null)
                {
                    Models.Wiki wiki = this.GetWiki(wikiname);
                    ap = new WikiTitle();
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
        public void DeleteTitleById(string wikiname, int titleid)
        {
            try
            {
                if (wikiname != null && (titleid > 0) &&  this.WikiExists(wikiname))
                {
                    Wiki wk = this.GetWiki(wikiname);
                    this.DeleteByTitle(wikiname, titleid);
                    WikiTitle  title= this.Get(wikiname, titleid);
                    this.DeleteFileByTitle(wikiname, titleid);
                    title.Categories.Clear();
                    this.db.Title.Remove(title);
                    this.MarkWikiAsUpdated(wk);
                    this.db.SaveChanges();
                 
                }

                }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
               
            }
        }
        public void DeleteTitleByWiki(string wikiname)
        {
            try
            {
                if (wikiname != null && this.WikiExists(wikiname))
                {
                    List<WikiTitle> tls = this.GetTitlebyWiki(wikiname);
                     if ( tls !=null)
                    {
                        foreach( WikiTitle t in tls)
                        {
                            this.DeleteTitleById(wikiname, t.Id);
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public void EditTitle(string wikiname, int id, WikiTitle newvals)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) == false && this.WikiExists(wikiname) == true
                    && id > 0 && newvals != null)
                {
                    WikiTitle oldvals = this.Get(wikiname, id);
                    this.db.Entry(oldvals).CurrentValues.SetValues(newvals);
                  
                    this.db.SaveChanges();
                    this.MarkWikiAsUpdated(this.GetWiki(wikiname));
                }

            }
            catch(ValidationException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        #endregion
        #region Content
        public List<WikiContent> GetHistory(int titleid)
        {
            try
            {
                List<WikiContent> ap = null;
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
        public WikiContent GetContent(String wikiname, int id)
        {
            try
            {
                WikiContent ap = null;
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
        public WikiContent GetContent(String wikiname, string slug, string title)
        {
            try
            {
                WikiContent ap = null;
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
        public List<WikiContent> GetByTitle(String wikiname, int tid)
        {
            try
            {
                List<WikiContent> ap = null;
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
        [Obsolete("Is to be deleted")]
        public void SaveorAddContent(string wikiname, int tid, string source, ApplicationUser user)
        {
            try
            {
                if (wikiname != null && tid > 0 && source != null)
                {

                    WikiContent cont;
                    cont = new WikiContent();
                    WikiTitle title = Get(wikiname, tid);
                    if (title != null)
                    {
                        //  cont.Id = id;
                        cont.Title = title;
                        cont.Source = source;
                        cont.Wiki = db.Wikis.FirstOrDefault(w => w.Name == wikiname);
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

        public void AddContentTitle(string wikiname, WikiTitle title , WikiContent cont,WikiCategory cat,ApplicationUser usr)
        {
            try
            {
                if (wikiname != null && title !=null && cont != null && cat!=null)
                {
                  var  a=this.CategoryExistsinWiki(cat.Title, wikiname);
                    Wiki wk = this.GetWiki(wikiname);
                   // if (wk != null &&a ==true)
                    {
                        //  cont.Id = id;
                       

                        title.Categories = new List<WikiCategory>();
                        title.Categories.Add(cat);
                        title.Wiki = wk;
                        title.WrittenBy = usr;
                        title.Slug = title.Name.Replace(" ", "_");

                        cont.Title = title;
                        cont.Version = 1;
                        cont.Wiki = wk;
                        cont.WrittenBy = usr;
                        cont.VersionDate = DateTime.Now;
                        if (wk.Content == null)
                        {
                            wk.Content = new List<Core.Data.Models.WikiContent>();
                        }
                        wk.Content.Add(cont);
                        if (wk.Titles == null)
                        {
                            wk.Titles = new List<WikiTitle>();
                        }
                        wk.Titles.Add(title);
                        db.Content.Add(cont);
                        db.Title.Add(title);

                        this.MarkWikiAsUpdated(wk);
                        db.SaveChanges();
                      
                    }
                }



            }
            catch(ValidationException x)
            {
                throw (x);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return null;
            }

        }

        public void AddContent(string wikiname, int tid, WikiContent cont,  ApplicationUser usr)
        {
            try
            {
                if (wikiname != null && tid > 0 && cont != null)
                {
                    //  var a = this.CategoryExistsinWiki(cat.Name, wikiname);
                    Wiki wk = this.GetWiki(wikiname);
                    //Content cont1 = new Content();
                    WikiTitle title = this.Get(wikiname, tid);

                    if (wk != null && title != null)
                    {
                        //  cont.Id = id;
                        int i = cont.Id;
                        if (i > 0)
                        {
                            i++;
                        }
                        else
                        {
                            i = db.Content.Count() + 1;
                        }
                        cont.Id = i;

                        if (this.CountWithTitleId(wikiname, title.Id) > 0)
                        {
                            cont.Version = this.CountWithTitleId(wikiname, title.Id) + 1;
                        }
                        else
                        {
                            cont.Version = 1;
                        }


                        //cont1.Id = db.Content.Count() + 1;
                        cont.Title = title;
                        // cont1.Version = cont.Version+ 1;

                        //if (wk.Content == null)
                        //{
                        //    wk.Content = new List<Core.Data.Models.Content>();
                        //}
                        wk.Content.Add(cont);
                        cont.Wiki = wk;
                        cont.WrittenBy = usr;
                        cont.VersionDate = DateTime.Now;
                        //cont1.Source = cont.Source;


                        //if (wk.Titles == null)
                        //{
                        //    wk.Titles = new List<Title>();
                        //}


                        db.Content.Add(cont);

                        //  db.Title.Add(title);
                        this.MarkWikiAsUpdated(wk);
                        db.SaveChanges();

                    }
                }



            }
            catch (ValidationException x)
            {
                throw (x);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return null;
            }

        }

        public WikiContent GetByVersion(string wikiname, int id, int version)
        {
            try
            {
                WikiContent ap = null;
                if (wikiname != null && this.WikiExists(wikiname) && id > 0 && version > 0)
                {
                    ap = db.Content.First(s => s.Title.Id == id && s.Version == version && s.Wiki.Name == wikiname);
                }




                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;

            }

        }
       public void DeleteById(string wikiname, int id)
        {
            try
            { 
                if (wikiname != null && this.WikiExists(wikiname) && id > 0)
                {
                    Wiki wk = this.GetWiki(wikiname);
                    if ( wk.Content !=null && wk.Content.Count>0)
                    {
                        wk.Content.Remove(this.GetContent(wikiname, id));
                        db.Content.Remove(this.GetContent(wikiname, id));
                        this.MarkWikiAsUpdated(wk);

                        this.db.SaveChanges();
                       
                    }

                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                
            }
        }
        public void DeleteByTitle(string wikiname, int tid)
        {
            try
            {
                if (wikiname != null && this.WikiExists(wikiname) && tid > 0)
                {
                    Wiki wk = this.GetWiki(wikiname);
                    if (wk.Content != null && wk.Content.Count > 0)
                    {
                        List<WikiContent> conts = this.GetByTitle(wikiname, tid);
                        if (conts != null)
                        {
                            foreach ( WikiContent ct  in conts)
                            {
                                this.DeleteById(wikiname, ct.Id);
                            }                           
                        }
                        this.MarkWikiAsUpdated(wk);
                        this.db.SaveChanges();
                       
                    }

                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }


        #endregion

        #region Categories
        public List<WikiCategory> GetCategories()
        {
            try
            {
                return this.db.Categories.ToList();
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public Boolean CategoryExistsinWiki(string catname,string wikiname)
        {
            try
            {
                Boolean ap = false;
                if (!CommonTools.isEmpty(wikiname) && this.WikiExists(wikiname)
                    &&!CommonTools.isEmpty(catname))
                {
                    List<WikiCategory> cats = this.GetCategorybyWiki(wikiname);  
                    if ( cats==null )
                    {
                        return false;
                    }
                    else
                    {
                        ap=cats.Exists(s => s.Title==catname);
                    }
                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return false;
            }
        }

        public List<WikiCategory> GetCategorybyWiki(string wikiname)
        {
            try
            {
                List<WikiCategory> ap = null;
                if ( !CommonTools.isEmpty(wikiname) && this.WikiExists(wikiname))
                {
                    ap = this.db.Categories.Where(c => c.Wiki.Name == wikiname).ToList();
                }
                return ap;

            }
             catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public WikiCategory GetCategorybyId(int id)
        {
            try
            {
                WikiCategory ap = null;
                if (id > 0)
                {
                    ap = this.db.Categories.First(c => c.Id == id);
                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public WikiCategory GetCategorybyTitle(string catname, string wikiname)
        {
            try
            {
                WikiCategory ap = null;
                if (!CommonTools.isEmpty(wikiname) && this.WikiExists(wikiname)
                     && !CommonTools.isEmpty(catname))
                {
                    List<WikiCategory> cats = this.GetCategorybyWiki(wikiname);
                    ap = cats.FirstOrDefault(x => x.Title == catname);
                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void CreateCategory(WikiCategory cat)
        {
            try
            {
              
                if ( cat != null)
                {
                    if (this.CategoryExistsinWiki(cat.Title,cat.Wiki.Name) == false)
                    {    Wiki  wk= this.GetWiki(cat.Wiki.Name);
                        if (wk != null)
                        {  if ( wk.Categories == null )
                            {
                                wk.Categories = new List<WikiCategory>();
                            }
                            wk.Categories.Add(cat);
                            this.db.Categories.Add(cat);
                            this.MarkWikiAsUpdated(wk);
                            this.db.SaveChanges();
                           
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
              
            }
        }
        public void DeleteCategoryByWiki(string wikiname)
        {
            try
            {
                WikiCategory ap = null;
                if (!CommonTools.isEmpty(wikiname) && this.WikiExists(wikiname))
                {
                    List<WikiCategory> cats = this.GetCategorybyWiki(wikiname);
                     if (cats !=null)
                    {
                         foreach( WikiCategory c in cats)
                        {
                            this.DeleteCategoryById(c.Id);
                        }
                    }
                }
                

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
              
            }
        }
        public void DeleteCategoryById(int id)
        {
            try
            {
                WikiCategory ap = null;
                if (id > 0)
                {
                    ap = this.db.Categories.First(c => c.Id == id);
                    this.db.Categories.Remove(ap);
                    this.db.SaveChanges();
                }


            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public void EditCategory(int id ,string wikiname,WikiCategory cat)
        {
            try
            {

                if (!CommonTools.isEmpty(wikiname) && this.WikiExists(wikiname) &&
                    cat !=null)
                {
                    WikiCategory od = this.GetCategorybyId(id);
                    Wiki wk = this.GetWiki(wikiname);
                     if ( od !=null && wk !=null)
                    {
                        this.db.Entry(od).CurrentValues.SetValues(cat);
                        this.db.SaveChanges();
                        this.MarkWikiAsUpdated(wk);

                    }
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }

        }
        public void AddTitleToCategory(string wikiname, int catid, WikiTitle title)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) == false && catid > 0 && title != null
                     )
                {
                    WikiCategory cat = this.GetCategorybyId(catid);
                    if (cat != null)
                    {
                        if (title.Categories == null)
                        {
                            title.Categories = new List<WikiCategory>();
                        }
                        if (title.Categories.FirstOrDefault(x => x.Id == catid) == null)
                        {
                            title.Categories.Add(cat);
                            CommonTools.titlemngr.EditTitle(wikiname, title.Id, title);

                        }

                    }
                }
            }
            catch(ValidationException ex)
            {
                throw (ex);
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
                    WikiCategory cat = this.GetCategorybyId(catid);
                    if (cat != null)
                    {
                        if (title.Categories != null)
                        {
                            //title.Categories = new List<WikiCategory>();

                            if (title.Categories.FirstOrDefault(x => x.Id == catid) != null)
                            {
                                title.Categories.Remove(cat);
                                CommonTools.titlemngr.EditTitle(wikiname, title.Id, title);

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        #endregion
        #region Files
        public void AddFile(string wikiname, WikiFile tfile, int tid, ApplicationUser user)
        {
            try
            {
                if (wikiname != null && this.WikiExists(wikiname) != false
                    && tfile !=null && tid>0 && user!=null)
                {

                    Wiki wk = this.GetWiki(wikiname);
                    WikiTitle title = this.Get(wikiname, tid);
                    //if (this.CountWithTitleId(wikiname, title.Id) > 0)
                    //{
                    //    tfile.Version = this.CountWithTitleId(wikiname, title.Id) + 1;
                    //}
                    //else
                    //{
                    //    tfile.Version = 1;
                    //}
                   
                    tfile.Wiki = wk;
                    tfile.VersionDate = DateTime.Now;
                    tfile.Title = title;
                    tfile.Owner = user;
                    wk.Files.Add(tfile);

                    
                    

                    this.db.Files.Add(tfile);
                    this.MarkWikiAsUpdated(wk);
                    this.db.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return null;
            }
        }

        public WikiFile GetFilesById(string wikiname, int id)
        {

            try
            {
                WikiFile ap = null;
                if (wikiname != null && this.WikiExists(wikiname) != false && id > 0)
                {
                    ap = db.Files.First(x => x.Wiki.Name == wikiname && x.Id == id);
                }

                return ap;


            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }

        }
        public List<WikiFile> GetFiles()
        {
            try
            {
                List<WikiFile> ap = null;
                ap = this.db.Files.ToList();


                return ap;


            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<WikiFile> GetWikiFiles(string wikiname)
        {
            try
            {
                List<WikiFile> ap = null;
                if (wikiname != null && this.WikiExists(wikiname) != false)
                {
                    ap=this.db.Files.Where(x => x.Wiki.Name == wikiname).ToList();
                }

                    return ap;


            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public  List<WikiFile> GetFilesByTitle(string wikiname,int tid)
        {

            try
            {
                List<WikiFile> ap = null;
                if (wikiname != null && this.WikiExists(wikiname) != false  && tid>0)
                {
                    ap = this.db.Files.Where(x => x.Wiki.Name == wikiname 
                    && x.Title.Id==tid).ToList();
                }

                return ap;


            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }

        }
        public WikiFile GetFileByVersion(string wikiname, int id, int version)
        {
            try
            {
                WikiFile ap = null;
                if (wikiname != null && this.WikiExists(wikiname) && id > 0 && version > 0)
                {
                    ap = db.Files.First(s => s.Title.Id == id && s.Version == version 
                    && s.Wiki.Name == wikiname);
                }




                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;

            }

        }

        public void DeleteFileById(string wikiname, int id)
        {
            try
            {
                if (wikiname != null && this.WikiExists(wikiname) != false 
                    && id > 0)
                {
                    Wiki  wk=this.GetWiki(wikiname);
                    if (wk.Files.Count > 0)
                    {
                        WikiFile file = this.GetFilesById(wikiname, id);
                        wk.Files.Remove(file);
                        

                        this.db.Files.Remove(file);
                        this.MarkWikiAsUpdated(wk);
                        this.db.SaveChanges();
                    }
                    
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                // return null;
            }
        }
        public void DeleteFileByTitle(string wikiname,int tid)
        {
            try
            {
                if (wikiname != null && this.WikiExists(wikiname) != false
                    && tid > 0)
                {
                    List<WikiFile> files = this.GetFilesByTitle(wikiname, tid);
                     if (  files.Count>0)
                    {
                         foreach( WikiFile file in files)
                        {
;                            this.DeleteFileById(wikiname, file.Id);
                        }
                        
                        this.MarkWikiAsUpdated(this.GetWiki(wikiname));
                    }

                }

                }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
               // return null;
            }
        }

        #endregion

        #region WikiModInvites
        public List<WikiModInvitations> GetWikiModInvites()
        {
            try
            {
                List<WikiModInvitations> ap = null;


                ap = this.db.ModInvites.ToList();
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;

            }
        }
        public List<WikiModInvitations> GetWikiModInvitesbyWiki(string wikiname)
        {
            try
            {
                List<WikiModInvitations> ap = null, tap = null;

                 if ( CommonTools.isEmpty(wikiname) == false && this.WikiExists(wikiname)==true)
                {
                    tap = this.GetWikiModInvites();
                     if (tap !=null)
                    {
                        ap = tap.Where(s => s.Wiki.Name == wikiname).ToList();
                    }
                }
                
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;

            }
        }
        public WikiModInvitations GetWikiModInvitebyId(string wikiname,int id)
        {
            try
            {
                WikiModInvitations ap = null;
                List<WikiModInvitations> tap = null;

                if (CommonTools.isEmpty(wikiname) == false && this.WikiExists(wikiname) == true)
                {
                    tap = this.GetWikiModInvitesbyWiki(wikiname);
                    if (tap != null)
                    {
                        ap = tap.FirstOrDefault(s => s.Id == id);
                    }
                }

                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;

            }
        }
        public void CreateNewModInvite(string wikiname,string  mod, WikiModInvitations inv )
        {
            try
            {
                if ( CommonTools.isEmpty(wikiname)==false && this.WikiExists(wikiname)==true
                    && CommonTools.isEmpty(mod) == false)
                {
                    Wiki wk = this.GetWiki(wikiname);
                    if ( wk !=null )
                    {
                        inv.Wiki = wk;
                        this.db.ModInvites.Add(inv);
                        this.db.SaveChanges();

                    }
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
               

            }

        }
        public void DeleteModInviteById(string wikiname, int id)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) == false && this.WikiExists(wikiname) == true
                    && id>0)
                {
                    WikiModInvitations inv = this.GetWikiModInvitebyId(wikiname,id);
                    if (inv!= null)
                    {
                        this.db.ModInvites.Remove(inv);
                        this.db.SaveChanges();

                    }
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);


            }

        }
        #endregion

    }
}
