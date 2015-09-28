using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlex.Core.Data.Models
{
   public class Wiki
    {
        [Required]
        public int id { get; set; }
        [Required]
        [Key]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public virtual List<Title> Titles { get; set; }
        public virtual List<Content> Content { get; set; }
        [Required]
        public virtual ApplicationUser Administrtor { get; set; }
        public virtual List<ApplicationUser> Moderators { get; set; }
        public virtual List<Category>Categories { get; set; }
        public virtual List<File> Files { get; set; }
    }
}
