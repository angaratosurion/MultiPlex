using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using MultiPlex.Formatting.Renderers;
using MultiPlex.Core.Data.Models;
using MultiPlex;
using MultiPlex.Core.Managers;

namespace MultiPlex.Core.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private WikiManager mng;
        UserManager usrmng = new UserManager();
        public HomeController()           
        { 
            this.mng = new WikiManager( new WikiEngine);
        }

       

        public ActionResult ViewWiki(string wikiname, int id, string slug)
        {

            var viewData = mng.ViewWiki(wikiname,  id, slug);
            if (viewData.Content == null)
                return RedirectToAction("EditWiki", new { id, slug });

            return View("View", viewData);
        }

        public ActionResult ViewWikiVersion(string wikiname, int id, string slug,
            int version)
        {
            var viewData = this.mng.ViewWikiVersion(wikiname, id, slug,version);

            if (viewData.Content == null)
                return RedirectToAction("ViewWiki", new {wikiname, id, slug });

           

            return View("View", viewData);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize]
        public ActionResult EditWiki(string wikiname, int id, string slug)
        {
            if (!WikiManager.IsEditable())
                return RedirectToAction("ViewWiki");
            Content content = this.mng.GetWikiforEditWiki(wikiname, id, slug);
           
            
            
            return View("Edit", content);
        }

        [AcceptVerbs(HttpVerbs.Post)]
       // [ValidateInput(false)]
       [Authorize]
        public ActionResult EditWiki(string wikiname, int id, string slug,
            string name,
            string source)
        {
            if (!WikiManager.IsEditable())
                return RedirectToAction("ViewWiki");
            
            id = this.mng.EditWikiPost(wikiname, id, slug, name, source,this.usrmng.GetUser(this.User.Identity.Name));
            return RedirectToAction("ViewWiki", new { wikiname,id, slug });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [OutputCache(Location = OutputCacheLocation.None)]
        public string GetWikiSource(int id, string slug, int version)
        {
            Content content = this.mng.GetWikiSource(id, slug, version);

            return content.Source;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [OutputCache(Location = OutputCacheLocation.None)]
        [ValidateInput(false)]
        public string GetWikiPreview(int id, string slug, string source)
        {
            return wikiEngine.Render(source, GetRenderers(this.Url));
        }

       
    }
}