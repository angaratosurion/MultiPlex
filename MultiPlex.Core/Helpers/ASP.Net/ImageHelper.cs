using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MultiPlex.Core.Helpers.ASP.Net
{
    public static class ImageHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, string height)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("height", height);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, string height = null, string width = null, string maxheight = null, string maxwidth = null, string cssClass = null)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);

            if (!string.IsNullOrWhiteSpace(height))
            {
                builder.MergeAttribute("height", height);
            }
            if (!string.IsNullOrWhiteSpace(width))
            {
                builder.MergeAttribute("width", width);
            }
            if (!string.IsNullOrWhiteSpace(maxheight))
            {
                builder.MergeAttribute("max-height", maxheight);
            }
            if (!string.IsNullOrWhiteSpace(maxwidth))
            {
                builder.MergeAttribute("max-width", maxwidth);
            }
            if (!string.IsNullOrWhiteSpace(cssClass))
            {
                builder.MergeAttribute("class", cssClass);
            }
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}
