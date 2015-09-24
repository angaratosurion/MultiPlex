using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using WikiPlex.Formatting;
using WikiPlex.Formatting.Renderers;
using WikiPlex.Web.Sample.Models;
using WikiPlex.Web.Sample.Repositories;
using WikiPlex.Web.Sample.Wiki;

namespace WikiPlex.Web.Sample.WebForms
{
    /// <summary>
    /// Summary description for Wiki
    /// </summary>
    [WebService(Namespace = "http://wikiplex.codeplex.com/Sample/WebForms/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Wiki : WebService
    {
        [WebMethod]
        public string GetWikiSource(int id, string slug, int version)
        {
            var repository = new WikiRepository();
            Content content = repository.GetByVersion(id, version);

            return content.Source;
        }

        [WebMethod]
        public string GetWikiPreview(int id, string slug, string source)
        {
            var wikiEngine = new WikiEngine();
            return wikiEngine.Render(source, GetFormatter());
        }

        private static Formatter GetFormatter()
        {
            var siteRenderers = new IRenderer[] { new TitleLinkRenderer() };
            IEnumerable<IRenderer> allRenderers = Renderers.All.Union(siteRenderers);
            return new Formatter(allRenderers);
        }
    }
}