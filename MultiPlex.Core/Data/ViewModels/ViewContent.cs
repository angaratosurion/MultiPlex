using System.Collections.Generic;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
    public class ViewContent
    {
        public WikiContent Content { get; set; }
        public List<WikiContent> History { get; set; }
        public bool Editable { get; set; }
    }
}