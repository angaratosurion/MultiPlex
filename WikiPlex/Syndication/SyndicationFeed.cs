using System.Collections.Generic;

namespace WikiPlex.Syndication
{
    /// <summary>
    /// This holds the syndicated feeds information.
    /// </summary>
    public class SyndicationFeed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyndicationFeed"/> class.
        /// </summary>
        public SyndicationFeed()
        {
            Items = new List<SyndicationItem>();
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets the list of <see cref="SyndicationItem"/>s.
        /// </summary>
        public IList<SyndicationItem> Items { get; private set; }
    }
}