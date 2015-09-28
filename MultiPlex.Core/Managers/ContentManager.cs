using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MultiPlex.Core.Data;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.Repositories;
using MultiPlex.Core.Data.ViewModels;
using MultiPlex.Core.Wiki;
using MultiPlex.Formatting.Renderers;


namespace MultiPlex.Core.Managers
{
    public class ContentManager
    {
        private readonly Context db = new Context();

        private readonly IWikiEngine wikiEngine;
        private readonly WikiRepository repository;
        UrlHelper url;
       // HtmlHelper html;
       
        string wikiname;
        

        public ContentManager( IWikiEngine wikiEngine,UrlHelper url,string wikiname)
        {
            this.url = url;
            this.wikiEngine = wikiEngine;
            this.wikiname = wikiname;

        }
        public ViewContent ViewWiki(string wikiname, int id, string slug)
        {
            try
            {
                var viewData = new ViewContent { Content = this.repository.GetContent(wikiname,id) };

                

                viewData.Content.RenderedSource = wikiEngine.Render(viewData.Content.Source, GetRenderers(url));
                viewData.History =this.repository.GetHistory(id);
                viewData.Editable = IsEditable();

                return viewData;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public ViewContent ViewWikiVersion(string wikiname, int id, string slug,
            int version)
        {
            try
            {
                var viewData = new ViewContent { Content = repository.GetByVersion(wikiname,id, version) };
              
                return viewData;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        
        public Content GetWikiforEditWiki(string wikiname, int id, string slug)
        {
            try
            {
                Content content = this.repository.GetContent(wikiname,id);
                if (content == null)
                    content = new Content { Title = new Title { Slug = slug } };
                return content;
               

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public int EditWikiPost(string wikiname, int titleid, string slug,
            string name,
            string source,ApplicationUser user)
        {
            try
            {

                
                Title title = this.repository.Get(wikiname, titleid);
                if (title == null)
                {

                    title = this.repository.Add(wikiname, name, slug, user);
                }

                    this.repository.SaveorAddContent(wikiname, title.Id, source, user);
                return titleid;

               
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return -1;
            }
        }
        public Content GetWikiSource(string wikiname,int id, string slug, int version)
        {

            try
            {
                Content content = repository.GetByVersion(wikiname,id, version);
                return content;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }


        }




        public string GetWikiPreview(int id, string slug, string source)
        {
            try
            {
                

                return wikiEngine.Render(source, GetRenderers(this.url));
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }


        public static bool IsEditable()
        {
            //return ConfigurationManager.AppSettings["Environment"] == "Debug";
            return true;
        }

        private IEnumerable<IRenderer> GetRenderers(UrlHelper url)
        {
            var siteRenderers = new IRenderer[] { new TitleLinkRenderer(url,
                repository,wikiname) };
            return Renderers.All.Union(siteRenderers);
        }
    }
}
