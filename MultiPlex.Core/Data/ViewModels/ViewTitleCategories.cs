using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
    public class ViewTitleCategories
    {
        public List<WikiCategory> Categories { get; set; }
        public WikiTitle Title { get; set; }
        public WikiCategory CategoryToAddOrRemove { get; set; }
    }
}
