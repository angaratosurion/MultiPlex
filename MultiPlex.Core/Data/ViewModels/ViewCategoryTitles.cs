using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
    public class ViewCategoryTitles
    {
        public WikiCategory Category { get; set; }
        public List<WikiTitle> Titles { get; set; }
    }
}
