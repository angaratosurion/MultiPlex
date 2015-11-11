using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
    public class EditContent
    {

       
        public virtual WikiTitle Title { get; set; }
        public string Source { get; set; }
    }
       
}
