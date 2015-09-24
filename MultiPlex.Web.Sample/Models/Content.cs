using System;

namespace MultiPlex.Web.Sample.Models
{
    public class Content
    {
        public int Id { get; set; }
        public Title Title { get; set; }
        public string Source { get; set; }
        public string RenderedSource { get; set; }
        public int Version { get; set; }
        public DateTime VersionDate { get; set; }
    }
}