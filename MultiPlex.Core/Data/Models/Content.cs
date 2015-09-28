using System;
using System.ComponentModel.DataAnnotations;

namespace MultiPlex.Core.Data.Models
{
    public class Content
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public virtual Title Title { get; set; }
        public string Source { get; set; }
        public string RenderedSource { get; set; }       
        public int Version { get; set; }
        [DataType(DataType.DateTime)]
        
        public DateTime VersionDate { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }
        [Required]
        public virtual Wiki Wiki { get; set; }
        [Required]        
        public  ApplicationUser WrittenBy { get; set; }

    }
}