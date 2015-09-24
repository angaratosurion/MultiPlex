using System.Xml;

namespace WikiPlex.Syndication
{
    /// <summary>
    /// Handles reading the Google specific ATOM feeds.
    /// </summary>
    public class GoogleAtomFeedReader : AtomFeedReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleAtomFeedReader"/> class.
        /// </summary>
        /// <param name="xmlFeed">The xml feed to read.</param>
        public GoogleAtomFeedReader(XmlDocument xmlFeed)
            : base(xmlFeed)
        {
        }

        /// <summary>
        /// Overidden. Gets the item date.
        /// </summary>
        /// <param name="item">The xml node to get the date from.</param>
        /// <returns>The read date.</returns>
        protected override string GetItemDate(XmlNode item)
        {
            return GetValue(item, "./atom:modified");
        }
    }
}