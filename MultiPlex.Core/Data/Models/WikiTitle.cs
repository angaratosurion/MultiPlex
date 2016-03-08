using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Data.Models;

namespace MultiPlex.Core.Data.Models
{
    

    public class WikiTitle
    {  
        [Required] 
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
       // [Key]          
        public int TitleId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Slug { get; set; }
        public int MaxVersion { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }
        public Boolean isLocked { get; set; }
        [Required]
        public virtual Wiki Wiki { get; set; }
        [Required]
        public string WrittenBy { get; set; }
        public virtual List<WikiFile> Files { get; set; }
        public virtual List<WikiCategory> Categories { get; set; }
      
        public virtual WikiContent Content { get; set; }
    }
}