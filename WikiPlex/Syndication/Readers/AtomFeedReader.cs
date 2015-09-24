using System.Xml;

namespace WikiPlex.Syndication
{
    /// <summary>
    /// Handles reading ATOM feeds.
    /// </summary>
    public class AtomFeedReader : FeedReader
    {
        /// <summary>
        /// Initializes a new <see cref="AtomFeedReader"/> class.
        /// </summary>
        /// <param name="xmlFeed">The xml feed to read.</param>
        public AtomFeedReader(XmlDocument xmlFeed)
            : base(xmlFeed)
        {
        }

        /// <summary>
        /// Overidden. Gets the namespace prefix.
        /// </summary>
        protected override string NamespacePrefix
        {
            get { return "atom"; }
        }

        /// <summary>
        /// Overidden. Gets the root of the feed.
        /// </summary>
        /// <returns>The root node.</returns>
        protected override XmlNode GetRoot()
        {
            return XmlFeed.DocumentElement;
        }

        /// <summary>
        /// Overridden. Gets all of the items in the feed.
        /// </summary>
        /// <param name="root">The root node of the feed.</param>
        /// <returns>The list of items.</returns>
        protected override XmlNodeList GetItems(XmlNode root)
        {
            return root.SelectNodes("//atom:entry", Namespaces);
        }

        /// <summary>
        /// Overidden. Will create a new <see cref="SyndicationFeed"/> from the xml feed.
        /// </summary>
        /// <param name="root">The root element.</param>
        /// <returns>The created <see cref="SyndicationFeed"/>.</returns>
        protected override SyndicationFeed CreateFeed(XmlNode root)
        {
            return new SyndicationFeed { Title = GetValue(root, "./atom:title") };
        }

        /// <summary>
        /// Overidden. Creates a new <see cref="SyndicationFeed"/> object.
        /// </summary>
        /// <param name="item">The root node of the feed.</param>
        /// <returns>The new <see cref="SyndicationFeed"/> object.</returns>
        protected override SyndicationItem CreateFeedItem(XmlNode item)
        {
            return new SyndicationItem
                       {
                           Title = GetValue(item, "./atom:title"),
                           Description = GetDescriptionValue(item),
                           Link = GetItemLink(item),
                           Date = new SyndicationDate(GetItemDate(item))
                       };
        }

        /// <summary>
        /// Overidden. Creates a new <see cref="SyndicationItem"/>.
        /// </summary>
        /// <param name="item">The xml node to create the item from.</param>
        /// <returns>The created <see cref="SyndicationItem"/>.</returns>
        private string GetItemLink(XmlNode item)
        {
            XmlNode link = item.SelectSingleNode("./atom:link[@rel='alternate']", Namespaces);
            if (link == null)
                return string.Empty;

            return link.Attributes.GetNamedItem("href").InnerText;
        }

        /// <summary>
        /// Will get the date for a feed node.
        /// </summary>
        /// <param name="item">The item to search.</param>
        /// <returns>The item date value.</returns>
        protected virtual string GetItemDate(XmlNode item)
        {
            return GetValue(item, "./atom:updated");
        }

        private string GetDescriptionValue(XmlNode parent)
        {
            string content = GetValue(parent, "./atom:content");
            if (!string.IsNullOrEmpty(content))
                return content;

            return GetValue(parent, "./atom:summary");
        }
    }
}