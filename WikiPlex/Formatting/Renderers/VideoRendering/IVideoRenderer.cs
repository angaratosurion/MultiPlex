using System.Web.UI;
using WikiPlex.Common;

namespace WikiPlex.Formatting.Renderers
{
    internal interface IVideoRenderer
    {
        Dimensions Dimensions { get; set; }

        void Render(string url, HtmlTextWriter writer);
    }
}