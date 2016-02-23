using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Data.Models;

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
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        [Required]
        public string WikiTitle { get; set; }
        public virtual List<WikiTitle> Titles { get; set; }
        public virtual List<WikiContent> Content { get; set; }
      
      //  public string AdministratorId { get; set; }
        [Required]
      //  [ForeignKey("AdministratorId")]
        public string Administrator { get; set; }
        public virtual List<WikiMods> Moderators { get; set; }
        public virtual List<WikiCategory> Categories { get; set; }
        public virtual List<WikiFile> Files { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }
          
        public DateTime UpdatedAt { get; set; }
    }
}
