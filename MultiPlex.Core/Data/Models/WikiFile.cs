using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Data.Models;

namespace MultiPlex.Core.Data.Models
{
    
   public class WikiFile
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
       // [Required]
        public string AbsolutePath { get; set; }
        [Required]
        public string RelativePath { get; set; }
        [Required]
        public string FileType { get; set; }
        public Boolean isImage { get; set; }
        public int Version { get; set; }
        [DataType(DataType.DateTime)]

        public DateTime VersionDate { get; set; }

        [Timestamp]
        public Byte[] RowVersion { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        public virtual Wiki Wiki { get; set; }
        [Required]
        public virtual WikiTitle Title { get; set; }

    }
}
