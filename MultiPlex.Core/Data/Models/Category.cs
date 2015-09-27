using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlex.Core.Data.Models
{
   public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name{ get; set; }
        public virtual List<Context> Content { get; set; }
        [Required]
        public virtual WikiModel Wiki { get; set; }
    }
}
