using System.Web.UI;
using WikiPlex.Common;

namespace WikiPlex.Formatting.Renderers
{
    internal abstract class EmbeddedVideoRender : IVideoRenderer
    {
        private HtmlTextWriter writer;

        public Dimensions Dimensions { get; set; }

        public void Render(string url, HtmlTextWriter writer)
        {
            this.writer = writer;

            AddObjectTagAttributes(url);
            writer.RenderBeginTag(HtmlTextWriterTag.Object);

            AddParameterTags(url);

            AddEmbedTagAttributes(url);
            writer.RenderBeginTag(HtmlTextWriterTag.Embed);
            writer.RenderEndTag();

            writer.RenderEndTag(); // </object>
            this.writer = null;
        }

        protected abstract void AddObjectTagAttributes(string url);
        protected abstract void AddParameterTags(string url);
        protected abstract void AddEmbedTagAttributes(string url);

        protected void AddAttribute(string key, string value)
        {
            writer.AddAttribute(key, value);
        }

        protected void AddAttribute(HtmlTextWriterAttribute key, string value)
        {
            writer.AddAttribute(key, value);
        }

        protected void AddParameterTag(string name, string value)
        {
            AddAttribute(HtmlTextWriterAttribute.Name, name);
            AddAttribute(HtmlTextWriterAttribute.Value, value);
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();
        }
    }
}