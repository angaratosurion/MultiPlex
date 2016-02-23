using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlex.Core.Data.Models
{
    public class WikiMods
    {
        public int Id { get; set; }
        [Required]
        [Key]
        public virtual Wiki Wiki { get; set; }
        [Required]
        [Key]
        public string Moderator { get; set; }
    }
}
