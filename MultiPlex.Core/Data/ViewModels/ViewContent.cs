using System.Collections.Generic;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
    public class ViewContent
    {
        public Content Content { get; set; }
        public List<Content> History { get; set; }
        public bool Editable { get; set; }
    }
}