using System;
using System.ComponentModel.DataAnnotations;

namespace MultiPlex.Core.Data.Models
{
    public class Content
    {
        [Required]
        public int Id { get; set; }
        public Title Title { get; set; }
        public string Source { get; set; }
        public string RenderedSource { get; set; }
        public int Version { get; set; }
        public DateTime VersionDate { get; set; }
        [Required]
        public WikiModel Wiki { get; set; }
        [Required]
        public ApplicationUser WrittenBy { get; set; }

    }
}