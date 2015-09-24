namespace WikiPlex.Syndication
{
    /// <summary>
    /// This holds a syndicated feed item's content.
    /// </summary>
    public class SyndicationItem
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public SyndicationDate Date { get; set; }
    }
}