using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlackCogs.Data.Models;

namespace MultiPlex.Core.Data.Models
{
    public class WikiContent
    {
        public WikiContent()
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
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Column(Order =1)]
        public int ContentId { get; set; } 

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
       // [ ForeignKey("Title")]
        public int TitleId { get; set; }
        [Required]
       // [ ForeignKey("Title")]
        public virtual WikiTitle Title { get; set; }
        [Required]
        public virtual Wiki Wiki { get; set; }
        [Required]        
        public string WrittenBy { get; set; }

    }
}