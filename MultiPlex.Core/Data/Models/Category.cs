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
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name{ get; set; }
      
        [Required]
        public virtual Wiki Wiki { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }
    }
}
