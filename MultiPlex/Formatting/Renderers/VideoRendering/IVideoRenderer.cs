using System.Web.UI;
using MultiPlex.Common;

namespace MultiPlex.Formatting.Renderers
{
    internal interface IVideoRenderer
    {
        Dimensions Dimensions { get; set; }

        void Render(string url, HtmlTextWriter writer);
    }
}