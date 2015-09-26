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
        [Required]
        public ApplicationUser Administrtor { get; set; }
        public List<ApplicationUser> Moderators { get; set; }
    }
}
