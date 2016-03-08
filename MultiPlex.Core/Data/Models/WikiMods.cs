using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlex.Core.Data.Models
{
    public class WikiMods
    {
        [Required]
       // [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
       
        public virtual Wiki Wiki { get; set; }
        [Required]
      //  [Key]
        public string Moderator { get; set; }
    }
}
