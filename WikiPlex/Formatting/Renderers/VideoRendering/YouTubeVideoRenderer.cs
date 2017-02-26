using System.Text.RegularExpressions;
using System.Web.UI;
using WikiPlex.Common;

namespace WikiPlex.Formatting.Renderers
{
    internal class YouTubeVideoRenderer : EmbeddedVideoRender
    {
        private static readonly Regex VideoIdRegex = new Regex(@"^http://www\.youtube\.com/watch\?v=(.+)$");
        const string WModeAttributeString = "transparent";
        const string SrcSttributeFormatString = "http://www.youtube.com/v/{0}";

        protected override void AddObjectTagAttributes(string url)
        {
            AddAttribute(HtmlTextWriterAttribute.Height, Dimensions.Height.ToString());
            AddAttribute(HtmlTextWriterAttribute.Width, Dimensions.Width.ToString());
        }

        protected override void AddParameterTags(string url)
        {
            AddParameterTag("movie", string.Format(SrcSttributeFormatString, Utility.ExtractFragment(VideoIdRegex, url)));
            AddParameterTag("wmode", WModeAttributeString);
        }

        protected override void AddEmbedTagAttributes(string url)
        {
            AddAttribute(HtmlTextWriterAttribute.Height, Dimensions.Height.ToString());
            AddAttribute(HtmlTextWriterAttribute.Width, Dimensions.Width.ToString());
            AddAttribute(HtmlTextWriterAttribute.Type, "application/x-shockwave-flash");
            AddAttribute("wmode", WModeAttributeString);

            AddAttribute(HtmlTextWriterAttribute.Src, string.Format(SrcSttributeFormatString, Utility.ExtractFragment(VideoIdRegex, url)));
        }
    }
}