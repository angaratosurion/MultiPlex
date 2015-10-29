using System;
using System.ComponentModel.DataAnnotations;

namespace MultiPlex.Core.Data.Models
{
    public class Content
    {
        public Content()
        {

        }
        //public Content(int id,Title title, string source, int version
        //   ,DateTime versionDate, Wiki wiki, ApplicationUser writtenBy)
        //{
        //    this.Id = id;
        //    this.Title = title;
        //    this.Source = source;
        //    this.Version = version;
        //    this.VersionDate = versionDate;
        //    this.Wiki = wiki;
        //    this.WrittenBy = writtenBy;
        //}
        [Required]
        public int Id { get; set; }
        [Required]
        public virtual Title Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Source { get; set; }
        public string RenderedSource { get; set; } 
        [Required]      
        public int Version { get; set; }
        [Required]
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