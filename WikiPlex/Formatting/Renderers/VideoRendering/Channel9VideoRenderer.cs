using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WikiPlex.Common;

namespace WikiPlex.Formatting.Renderers
{
    internal class Channel9VideoRenderer : IVideoRenderer
    {
        private const string DimensionErrorText = "Cannot resolve video macro, invalid parameter '{0}'. Value can only be pixel based.";

        public Dimensions Dimensions { get; set; }

        public void Render(string url, HtmlTextWriter writer)
        {
            if (Dimensions.Height.Value.Type != UnitType.Pixel)
                throw new RenderException(string.Format(DimensionErrorText, "height"));
            if (Dimensions.Width.Value.Type != UnitType.Pixel)
                throw new RenderException(string.Format(DimensionErrorText, "width"));

            var actualUri = new Uri(url);
            url = actualUri.GetLeftPart(UriPartial.Path);

            if (url[url.Length - 1] != '/')
                url += "/";
            if (!url.EndsWith("/player/", StringComparison.OrdinalIgnoreCase))
                url += "player";

            writer.AddAttribute(HtmlTextWriterAttribute.Src, url + "?h=" + Dimensions.Height.Value.Value + "&w=" + Dimensions.Width.Value.Value, false);
            writer.AddAttribute(HtmlTextWriterAttribute.Width, Dimensions.Width.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Height, Dimensions.Height.ToString());
            writer.AddAttribute("scrolling", "no");
            writer.AddAttribute("frameborder", "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Iframe);
            writer.RenderEndTag();
        }
    }
}