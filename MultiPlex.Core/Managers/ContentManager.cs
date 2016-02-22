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
using MultiPlex.Core.WikiMacro;
using MultiPlex.Formatting.Renderers;

using BlackCogs.Data.Models;
namespace MultiPlex.Core.Managers
{
    public class ContentManager
    {
       

        private readonly IWikiEngine wikiEngine;
        private readonly WikiRepository repository = new WikiRepository();

        

        UrlHelper url;
       // HtmlHelper html;
       
        string wikiname;
        

        public ContentManager( IWikiEngine wikiEngine,UrlHelper url,string wikiname)
        {
            this.url = url;
            this.wikiEngine = wikiEngine;
            this.wikiname = wikiname;

        }
        public WikiTitle AddContent(String wikiname , WikiTitle title, WikiContent cont,int cid,ApplicationUser usr )
        {
            try
            {
                WikiTitle ap= null;
                if (CommonTools.isEmpty(wikiname) == false && (cid > 0)
                  && title != null && cont != null)
                {
                    WikiCategory cat = CommonTools.catmngr.GetCategoryById(cid);
                    Wiki wk = CommonTools.wkmngr.GetWiki(wikiname);

                    if (wk != null && cat != null && usr != null)
                    {
                        
                       ap= repository.AddContentTitle(wikiname, title, cont,cat,usr);

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
        public ViewContent ViewContent(string wikiname, int id, string slug)
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
        public WikiContent GetContent(string wikiname, int id)
        {
            try
            {
                var viewData =  this.repository.GetContent(wikiname, id) ;
                return viewData;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public ViewContent ViewContentVersion(string wikiname, int id, string slug,
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
       
        public void EditContentPost(string wikiname, WikiContent cont, ApplicationUser user,int id)
        {
            try
            {
                if (cont != null)
                {
                    WikiTitle title =this.repository.Get(wikiname, id);
                 //   Title title = cont.Title;
                    if (CommonTools.isEmpty(wikiname) == false 
                 && id >0 && cont != null)
                    {


                        this.repository.AddContent(wikiname, id, cont,  user);

                    }
                }


            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return -1;
            }
        }
        public WikiContent GetWikiSource(string wikiname,int id, string slug, int version)
        {

            try
            {
                WikiContent content = repository.GetByVersion(wikiname,id, version);
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
