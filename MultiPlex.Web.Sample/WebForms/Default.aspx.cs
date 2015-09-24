using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MultiPlex.Formatting.Renderers;
using MultiPlex.Web.Sample.Repositories;
using MultiPlex.Web.Sample.Wiki;
using Content = MultiPlex.Web.Sample.Models.Content;

namespace MultiPlex.Web.Sample.WebForms
{
    public partial class Default : Page
    {
        private readonly IWikiRepository repository = new WikiRepository();
        private readonly IWikiEngine wikiEngine = new WikiEngine();
        private Content wikiContent;

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = GetId();
            string slug = GetSlug();

            int version;
            if (!string.IsNullOrEmpty(Request.QueryString["v"]) && int.TryParse(Request.QueryString["v"], out version))
            {
                wikiContent = repository.GetByVersion(id, version);
                if (wikiContent == null)
                    Response.Redirect(ResolveClientUrl("~/WebForms/?i=" + id + "&p=" + HttpUtility.UrlEncode(slug)));
            }
            else
                wikiContent = repository.Get(id);

            if (wikiContent == null)
                Response.Redirect(ResolveClientUrl("~/WebForms/Edit.aspx?i=" + id + "&p=" + HttpUtility.UrlEncode(slug)));

            title.Text = "MultiPlex Sample - " + HttpUtility.HtmlEncode(wikiContent.Title.Name);
            sourceId.Text = previewId.Text = wikiContent.Title.Id.ToString();
            sourceSlug.Text = previewSlug.Text = wikiContent.Title.Slug;
            sourceVersion.Text = wikiContent.Version.ToString();
            renderedSource.Text = wikiEngine.Render(wikiContent.Source, GetRenderers());
            Name.Value = wikiContent.Title.Name;
            NotLatestPlaceHolder.Visible = wikiContent.Version != wikiContent.Title.MaxVersion;
            editWiki.Visible = editWikiForm.Visible = IsEditable();

            pageHistory.DataSource = repository.GetHistory(id);
            pageHistory.DataBind();
        }

        private int GetId()
        {
            string idParam = Request.QueryString["i"];
            if (string.IsNullOrEmpty(idParam))
                return 1;
            return int.Parse(idParam);
        }

        private string GetSlug()
        {
            string slug = Request.QueryString["p"];
            if (string.IsNullOrEmpty(slug))
                slug = "home";
            return slug;
        }

        protected void BindPageHistoryItem(object sender, RepeaterItemEventArgs e)
        {
            var date = e.Item.FindControl("date") as Literal;
            var versionLink = e.Item.FindControl("versionLink") as HyperLink;
            var historyItem = e.Item.DataItem as Content;

            date.Visible = versionLink.Visible = false;

            if (wikiContent.Version == historyItem.Version)
            {
                date.Visible = true;
                date.Text = wikiContent.VersionDate.ToString();
            }
            else
            {
                versionLink.Visible = true;
                versionLink.NavigateUrl = ResolveClientUrl("~/WebForms/?i=" + wikiContent.Title.Id + "&p=" + HttpUtility.UrlEncode(wikiContent.Title.Slug) + "&v=" + historyItem.Version);
                versionLink.Text = historyItem.VersionDate.ToString();
            }
        }

        private static IEnumerable<IRenderer> GetRenderers()
        {
            var siteRenderers = new IRenderer[] { new TitleLinkRenderer() };
            return Renderers.All.Union(siteRenderers);
        }

        private static bool IsEditable()
        {
            return ConfigurationManager.AppSettings["Environment"] == "Debug";
        }

        protected void SaveWikiContent(object sender, EventArgs e)
        {
            int id = GetId();
            string slug = GetSlug();
            repository.Save(id, slug, Name.Value, Source.Text);
            Response.Redirect("~/WebForms/?p=" + HttpUtility.UrlEncode(slug));
        }
    }
}