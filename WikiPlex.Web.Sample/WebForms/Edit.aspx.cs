using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using WikiPlex.Web.Sample.Models;
using WikiPlex.Web.Sample.Repositories;

namespace WikiPlex.Web.Sample.WebForms
{
    public partial class Edit : Page
    {
        private readonly IWikiRepository repository = new WikiRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsEditable())
                Response.Redirect("/WebForms");

            if (Page.IsPostBack)
                return;

            int id = GetId();
            string slug = GetSlug();
            Content content = repository.Get(id);

            if (content != null)
            {
                Name.Text = content.Title.Name;
                Source.Text = content.Source;
                CancelPlaceHolder.Visible = true;
                Cancel.OnClientClick = "window.location.href='" + ResolveClientUrl("~/WebForms/?i=" + id + "&p=" + HttpUtility.UrlEncode(slug) + "'");
            }
        }

        private int GetId()
        {
            string idParam = Request.QueryString["i"];
            if (string.IsNullOrEmpty(idParam))
                return 0;
            return int.Parse(idParam);
        }

        private string GetSlug()
        {
            string slug = Request.QueryString["p"];
            if (string.IsNullOrEmpty(slug))
                return string.Empty;
            return slug;
        }

        private static bool IsEditable()
        {
            return ConfigurationManager.AppSettings["Environment"] == "Debug";
        }

        protected void SaveSource(object sender, EventArgs e)
        {
            int id = GetId();
            string slug = GetSlug();
            id = repository.Save(id, slug, Name.Text, Source.Text);
            Response.Redirect("~/WebForms/?i=" + id + "&p=" + HttpUtility.UrlEncode(slug));
        }
    }
}