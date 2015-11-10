﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Wiki GetWiki(string wikiname)
        {

            try
            {
                Models.Wiki ap = null;
                if (wikiname != null)
                {
                    ap = db.Wikis.FirstOrDefault(w => w.Name == wikiname);
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
        public void CreateWiki(Wiki wk)
        {
            try
            {
                if ( wk!=null && this.WikiExists(wk.Name)==false)
                {
                    this.db.Wikis.Add(wk);
                    this.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return null;
            }

        }

        #endregion
        #region Title
        public List<Title> GetTitlebyWiki(string wikiname)
        {
            try
            {
                List<Title> ap = null;
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
       public List<Title> GetTitleByCategory(string wikiname,int catid)
        {
            try
            {
                List<Title> ap = null;
                 if (CommonTools.isEmpty(wikiname)==false && catid>0 && this.WikiExists(wikiname))
                {
                    Category cat = this.GetCategorybyId(catid);
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
        public Title Get(string wikiname, int titleid)
        {
            try
            {
                Title ap = null;
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
        public Title Add(string wikiname, string name, string slug, ApplicationUser user)
        {
            try
            {
                Title ap = null;

                if (wikiname != null)// && slug != null)
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
        public void DeleteTitleById(string wikiname, int titleid)
        {
            try
            {
                if (wikiname != null && (titleid > 0) &&  this.WikiExists(wikiname))
                {
                    Wiki wk = this.GetWiki(wikiname);
                    this.DeleteByTitle(wikiname, titleid);
                    Title  title= this.Get(wikiname, titleid);
                    title.Categories.Clear();
                    this.db.Title.Remove(title);
                    this.db.SaveChanges();

                }

                }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
               
            }
        }
        #endregion
        #region Content
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

        public void AddContentTitle(string wikiname, Title title , Content cont,Category cat,ApplicationUser usr)
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
                       

                        title.Categories = new List<Category>();
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
                            wk.Content = new List<Core.Data.Models.Content>();
                        }
                        wk.Content.Add(cont);
                        if (wk.Titles == null)
                        {
                            wk.Titles = new List<Title>();
                        }
                        wk.Titles.Add(title);
                        db.Content.Add(cont);
                        db.Title.Add(title);
                       

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

        public void AddContent(string wikiname, int tid, Content cont,  ApplicationUser usr)
        {
            try
            {
                if (wikiname != null && tid>0 && cont != null )
                {
                  //  var a = this.CategoryExistsinWiki(cat.Name, wikiname);
                    Wiki wk = this.GetWiki(wikiname);
                    //Content cont1 = new Content();
                    Title title = this.Get(wikiname, tid);
                    
                   if (wk != null   && title !=null)
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

        public Content GetByVersion(string wikiname, int id, int version)
        {
            try
            {
                Content ap = null;
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
                        List<Content> conts = this.GetByTitle(wikiname, tid);
                        if (conts != null)
                        {
                            foreach ( Content ct  in conts)
                            {
                                this.DeleteById(wikiname, ct.Id);
                            }                           
                        }
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
        public List<Category> GetCategories()
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
                    List<Category> cats = this.GetCategorybyWiki(wikiname);  
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

        public List<Category> GetCategorybyWiki(string wikiname)
        {
            try
            {
                List<Category> ap = null;
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
        public Category GetCategorybyId(int id)
        {
            try
            {
                Category ap = null;
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
        public void CreateCategory(Category cat)
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
                                wk.Categories = new List<Category>();
                            }
                            wk.Categories.Add(cat);
                            this.db.Categories.Add(cat);
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


        #endregion
        #region Files
        public void AddFile(string wikiname, File tfile, int tid, ApplicationUser user)
        {
            try
            {
                if (wikiname != null && this.WikiExists(wikiname) != false
                    && tfile !=null && tid>0 && user!=null)
                {

                    Wiki wk = this.GetWiki(wikiname);
                    Title title = this.Get(wikiname, tid);
                    if (this.CountWithTitleId(wikiname, title.Id) > 0)
                    {
                        tfile.Version = this.CountWithTitleId(wikiname, title.Id) + 1;
                    }
                    else
                    {
                        tfile.Version = 1;
                    }
                   
                    tfile.Wiki = wk;
                    tfile.VersionDate = DateTime.Now;
                    tfile.Title = title;
                    tfile.Owner = user;
                    wk.Files.Add(tfile);

                    
                    

                    this.db.Files.Add(tfile);
                    
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return null;
            }
        }

        public File GetFilesById(string wikiname, int id)
        {

            try
            {
                File ap = null;
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
        public List<File> GetFiles()
        {
            try
            {
                List<File> ap = null;
                ap = this.db.Files.ToList();


                return ap;


            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<File> GetWikiFiles(string wikiname)
        {
            try
            {
                List<File> ap = null;
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
        public  List<File> GetFilesByTitle(string wikiname,int tid)
        {

            try
            {
                List<File> ap = null;
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
        public File GetFileByVersion(string wikiname, int id, int version)
        {
            try
            {
                File ap = null;
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
                        File file = this.GetFilesById(wikiname, id);
                        wk.Files.Remove(file);
                        

                        this.db.Files.Remove(file);
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
                    List<File> files = this.GetFilesByTitle(wikiname, tid);
                     if (  files.Count>0)
                    {
                         foreach( File file in files)
                        {
;                            this.DeleteFileById(wikiname, file.Id);
                        }
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

    }
}
