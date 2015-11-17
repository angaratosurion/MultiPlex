using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        [Required]
        public string WikiTitle { get; set; }
        public virtual List<WikiTitle> Titles { get; set; }
        public virtual List<WikiContent> Content { get; set; }
        [Required]
        public virtual ApplicationUser Administrator { get; set; }
        public virtual List<ApplicationUser> Moderators { get; set; }
        public virtual List<WikiCategory> Categories { get; set; }
        public virtual List<WikiFile> Files { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }
        [DataType(DataType.DateTime)]
        [Required,DatabaseGenerated(DatabaseGeneratedOption.Computed)]             
        public DateTime UpdatedAt { get; set; }
    }
}
