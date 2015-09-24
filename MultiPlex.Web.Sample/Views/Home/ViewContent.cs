using System.Collections.Generic;
using MultiPlex.Web.Sample.Models;

namespace MultiPlex.Web.Sample.Views.Home
{
    public class ViewContent
    {
        public Content Content { get; set; }
        public ICollection<Content> History { get; set; }
        public bool Editable { get; set; }
    }
}