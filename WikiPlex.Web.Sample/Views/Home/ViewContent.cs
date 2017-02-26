using System.Collections.Generic;
using WikiPlex.Web.Sample.Models;

namespace WikiPlex.Web.Sample.Views.HomeWiki
{
    public class ViewContent
    {
        public Content Content { get; set; }
        public ICollection<Content> History { get; set; }
        public bool Editable { get; set; }
    }
}