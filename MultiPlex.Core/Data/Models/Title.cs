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
        [Required]
        public WikiModel Wiki { get; set; }
        [Required]
        public ApplicationUser WrittenBy { get; set; }
    }
}