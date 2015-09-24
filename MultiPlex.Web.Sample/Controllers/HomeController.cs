using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using MultiPlex.Formatting.Renderers;
using MultiPlex.Web.Sample.Models;
using MultiPlex.Web.Sample.Repositories;
using MultiPlex.Web.Sample.Views.Home;
using MultiPlex.Web.Sample.Wiki;

namespace MultiPlex.Web.Sample.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IWikiRepository repository;
        private readonly IWikiEngine wikiEngine;

        public HomeController()
            : this(new WikiRepository(), new WikiEngine())
        {
        }

        public HomeController(IWikiRepository repository, IWikiEngine wikiEngine)
        {
            this.repository = repository;
            this.wikiEngine = wikiEngine;
        }

        public ActionResult ViewWiki(int id, string slug)
        {
            var viewData = new ViewContent { Content = repository.Get(id) };

            if (viewData.Content == null)
                return RedirectToAction("EditWiki", new { id, slug });

            viewData.Content.RenderedSource = wikiEngine.Render(viewData.Content.Source, GetRenderers());
            viewData.History = repository.GetHistory(id);
            viewData.Editable = IsEditable();

            return View("View", viewData);
        }

        public ActionResult ViewWikiVersion(int id, string slug, int version)
        {
            var viewData = new ViewContent { Content = repository.GetByVersion(id, version) };

            if (viewData.Content == null)
                return RedirectToAction("ViewWiki", new { id, slug });

            viewData.Content.RenderedSource = wikiEngine.Render(viewData.Content.Source, GetRenderers());
            viewData.History = repository.GetHistory(id);
            viewData.Editable = IsEditable();

            return View("View", viewData);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditWiki(int id, string slug)
        {
            if (!IsEditable())
                return RedirectToAction("ViewWiki");

            Content content = repository.Get(id);

            if (content == null)
                content = new Content { Title = new Title { Slug = slug } };

            return View("Edit", content);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult EditWiki(int id, string slug, string name, string source)
        {
            if (!IsEditable())
                return RedirectToAction("ViewWiki");

            id = repository.Save(id, slug, name, source);
            return RedirectToAction("ViewWiki", new { id, slug });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [OutputCache(Location = OutputCacheLocation.None)]
        public string GetWikiSource(int id, string slug, int version)
        {
            Content content = repository.GetByVersion(id, version);

            return content.Source;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [OutputCache(Location = OutputCacheLocation.None)]
        [ValidateInput(false)]
        public string GetWikiPreview(int id, string slug, string source)
        {
            return wikiEngine.Render(source, GetRenderers());
        }

        private static bool IsEditable()
        {
            return ConfigurationManager.AppSettings["Environment"] == "Debug";
        }

        private IEnumerable<IRenderer> GetRenderers()
        {
            var siteRenderers = new IRenderer[] { new TitleLinkRenderer(Url, repository) };
            return Renderers.All.Union(siteRenderers);
        }
    }
}