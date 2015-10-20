using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MultiPlex.Core.Data.Models
{
    

    public class Title
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
        [Required]
        public virtual Wiki Wiki { get; set; }
        [Required]
        public virtual ApplicationUser WrittenBy { get; set; }
        public virtual List<File> Files { get; set; }
        public virtual List<Category> Categories { get; set; }
      
        public virtual Content Content { get; set; }
    }
}