using System.Text.RegularExpressions;
using System.Web.UI;
using WikiPlex.Common;

namespace WikiPlex.Formatting.Renderers
{
    internal class VimeoVideoRenderer : EmbeddedVideoRender
    {
        private static readonly Regex VideoIdRegex = new Regex(@"^http://(?:www\.)?vimeo\.com/(.+)$");
        const string WModeAttributeString = "transparent";
        const string SrcSttributeFormatString = "http://vimeo.com/moogaloop.swf?clip_id={0}&server=vimeo.com&show_title=1&show_byline=1&show_portrait=1&color=&fullscreen=1&autoplay=0&loop=0";

        protected override void AddObjectTagAttributes(string url)
        {
            AddAttribute(HtmlTextWriterAttribute.Height, Dimensions.Height.ToString());
            AddAttribute(HtmlTextWriterAttribute.Width, Dimensions.Width.ToString());
        }

        protected override void AddParameterTags(string url)
        {
            AddParameterTag("movie", string.Format(SrcSttributeFormatString, Utility.ExtractFragment(VideoIdRegex, url)));
            AddParameterTag("wmode", WModeAttributeString);
            AddParameterTag("allowfullscreen", "true");
            AddParameterTag("allowscriptaccess", "always");
        }

        protected override void AddEmbedTagAttributes(string url)
        {
            AddAttribute(HtmlTextWriterAttribute.Height, Dimensions.Height.ToString());
            AddAttribute(HtmlTextWriterAttribute.Width, Dimensions.Width.ToString());
            AddAttribute(HtmlTextWriterAttribute.Type, "application/x-shockwave-flash");
            AddAttribute("wmode", WModeAttributeString);
            AddAttribute("allowfullscreen", "true");
            AddAttribute("allowscriptaccess", "always");

            AddAttribute(HtmlTextWriterAttribute.Src, string.Format(SrcSttributeFormatString, Utility.ExtractFragment(VideoIdRegex, url)));
        }
    }
}
