using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MultiPlex.Formatting.Renderers;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Helpers;
using MultiPlex.Core.Data.Repositories;

namespace MultiPlex.Core.Wiki
{
    public class TitleLinkRenderer : Renderer
    {
        private const string LinkFormat = "<a href=\"{0}\">{1}</a>";
        private readonly UrlHelper urlHelper;
        private readonly WikiRepository wikiRepository;

        public TitleLinkRenderer() : this(null, new WikiRepository ())
        {
        }

        public TitleLinkRenderer(UrlHelper urlHelper, WikiRepository wikiRepository)
        {
            this.urlHelper = urlHelper;
            this.wikiRepository = wikiRepository;
        }

        protected override ICollection<string> ScopeNames
        {
            get { return new[] {WikiScopeName.WikiLink}; }
        }

        protected override string PerformExpand(string scopeName, string input, Func<string, string> htmlEncode,
            Func<string, string> attributeEncode)
        {
            string slug = SlugHelper.Generate(input);
            Content content = wikiRepository.Get(slug, input);
            int id = content != null ? content.Title.Id : 0;
            string url;

            if (urlHelper != null)
                url = urlHelper.RouteUrl("Default", new { id, slug });
            else
                url = "/WebForms/?i=" + id + "&p=" + slug;

            return string.Format(LinkFormat, attributeEncode(url), htmlEncode(input));
        }
    }
}