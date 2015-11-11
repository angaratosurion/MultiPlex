using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MultiPlex.Core.Data.Models
{
    

    public class WikiTitle
    {  
            [Required]                
        public int Id { get; set; }
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
        public virtual ApplicationUser WrittenBy { get; set; }
        public virtual List<WikiFile> Files { get; set; }
        public virtual List<WikiCategory> Categories { get; set; }
      
        public virtual WikiContent Content { get; set; }
    }
}