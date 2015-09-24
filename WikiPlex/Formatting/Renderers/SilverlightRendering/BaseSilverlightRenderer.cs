using System.Web.UI;

namespace WikiPlex.Formatting.Renderers
{
    internal abstract class BaseSilverlightRenderer : ISilverlightRenderer
    {
        public abstract string DataMimeType { get; }
        public abstract string ObjectType { get; }
        public abstract string DownloadUrl { get; }

        public void AddObjectTagAttributes(HtmlTextWriter writer)
        {
            writer.AddAttribute("data", DataMimeType);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, ObjectType);
        }

        public virtual void AddParameterTags(string url, bool gpuAcceleration, string[] initParams, HtmlTextWriter writer)
        {
            AddParameter("source", url, writer);
            AddParameter(gpuAcceleration ? "enableGPUAcceleration" : "windowless", "true", writer);

            if (initParams.Length > 0)
                AddParameter("initParams", string.Join(",", initParams), writer);
        }

        public void AddDownloadLink(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.P);

            writer.Write("You need to install Microsoft Silverlight to view this content. ");

            writer.AddAttribute(HtmlTextWriterAttribute.Href, DownloadUrl, false);
            writer.AddStyleAttribute(HtmlTextWriterStyle.TextDecoration, "none");
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            writer.Write("Get Silverlight!<br />");

            writer.AddAttribute(HtmlTextWriterAttribute.Src, "http://go.microsoft.com/fwlink/?LinkID=108181", false);
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, "Get Microsoft Silverlight");
            writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag(); // img

            writer.RenderEndTag(); // a
            writer.RenderEndTag(); // p
        }

        protected void AddParameter(string name, string value, HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Name, name);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();
        }
    }
}