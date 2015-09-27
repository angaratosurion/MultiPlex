using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlex.Core.Data.Models
{
   public class WikiModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        [Key]
        public string WikiName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Desrcition { get; set; }
        public virtual Title Titles { get; set; }
        public virtual List<Content> Content { get; set; }
        [Required]
        public virtual ApplicationUser Administrtor { get; set; }
        public virtual List<ApplicationUser> Moderators { get; set; }
        public virtual List<Category>Categories { get; set; }
    }
}
