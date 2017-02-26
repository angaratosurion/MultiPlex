using System.Xml;

namespace WikiPlex.Syndication
{
    /// <summary>
    /// Handles reading RSS feeds.
    /// </summary>
    public class RssFeedReader : FeedReader
    {
        /// <summary>
        /// Initializes a new <see cref="RssFeedReader" /> class.
        /// </summary>
        /// <param name="xmlFeed">The xml feed to read.</param>
        public RssFeedReader(XmlDocument xmlFeed)
            : base(xmlFeed)
        {
        }

        /// <summary>
        /// Overidden. Gets the namespace prefix.
        /// </summary>
        protected override string NamespacePrefix
        {
            get { return null; }
        }

        /// <summary>
        /// Overidden. Gets the root of the feed.
        /// </summary>
        /// <returns>The root node.</returns>
        protected override XmlNode GetRoot()
        {
            return XmlFeed.DocumentElement.SelectSingleNode("./channel");
        }

        /// <summary>
        /// Overridden. Gets all of the items in the feed.
        /// </summary>
        /// <param name="root">The root node of the feed.</param>
        /// <returns>The list of items.</returns>
        protected override XmlNodeList GetItems(XmlNode root)
        {
            return root.SelectNodes("//item");
        }

        /// <summary>
        /// Overidden. Creates a new <see cref="SyndicationFeed"/> object.
        /// </summary>
        /// <param name="root">The root node of the feed.</param>
        /// <returns>The new <see cref="SyndicationFeed"/> object.</returns>
        protected override SyndicationFeed CreateFeed(XmlNode root)
        {
            return new SyndicationFeed { Title = GetValue(root, "./title") };
        }

        /// <summary>
        /// Overidden. Creates a new <see cref="SyndicationItem"/>.
        /// </summary>
        /// <param name="item">The xml node to create the item from.</param>
        /// <returns>The created <see cref="SyndicationItem"/>.</returns>
        protected override SyndicationItem CreateFeedItem(XmlNode item)
        {
            return new SyndicationItem
                       {
                           Title = GetValue(item, "./title"),
                           Description = GetValue(item, "./description"),
                           Link = GetValue(item, "./link"),
                           Date = new SyndicationDate(GetValue(item, "./pubDate"))
                       };
        }
    }
}