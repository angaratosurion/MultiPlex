using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
    public class ViewTitleFile
    {
        public WikiTitle Title { get; set; }
        public WikiFile File { get; set; }
        public  Boolean ToBeAdded { get; set; }
    }
}
