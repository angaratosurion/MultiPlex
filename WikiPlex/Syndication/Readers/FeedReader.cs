using System;
using System.Xml;
using WikiPlex.Common;

namespace WikiPlex.Syndication
{
    /// <summary>
    /// Defines the abstract base class for all feed readers.
    /// </summary>
    public abstract class FeedReader : IFeedReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedReader"/> class.
        /// </summary>
        /// <param name="xmlFeed"></param>
        protected FeedReader(XmlDocument xmlFeed)
        {
            XmlFeed = xmlFeed;
        }

        /// <summary>
        /// Gets the xml feed.
        /// </summary>
        protected XmlDocument XmlFeed { get; private set; }

        /// <summary>
        /// Gets the xml namespaces.
        /// </summary>
        protected XmlNamespaceManager Namespaces { get; private set; }

        /// <summary>
        /// Gets the namespace prefix.
        /// </summary>
        protected abstract string NamespacePrefix { get; }

        /// <summary>
        /// Will read the feed.
        /// </summary>
        /// <returns>The read syndicated feed.</returns>
        public SyndicationFeed Read()
        {
            Guard.NotNull(XmlFeed, "xmlFeed");

            XmlNode root = GetRoot();
            if (root == null)
                throw new ArgumentException("Feed Root Not Found.");

            SetupNamespaces(root);
            SyndicationFeed feed = CreateFeed(root);

            XmlNodeList items = GetItems(root);
            foreach (XmlNode item in items)
                feed.Items.Add(CreateFeedItem(item));

            return feed;
        }

        /// <summary>
        /// Gets the root of the feed.
        /// </summary>
        /// <returns>The root node.</returns>
        protected abstract XmlNode GetRoot();

        /// <summary>
        /// Gets all of the items in the feed.
        /// </summary>
        /// <param name="root">The root node of the feed.</param>
        /// <returns>The list of items.</returns>
        protected abstract XmlNodeList GetItems(XmlNode root);

        /// <summary>
        /// Creates a new <see cref="SyndicationFeed"/> object.
        /// </summary>
        /// <param name="root">The root node of the feed.</param>
        /// <returns>The new <see cref="SyndicationFeed"/> object.</returns>
        protected abstract SyndicationFeed CreateFeed(XmlNode root);

        /// <summary>
        /// Creates a new <see cref="SyndicationItem"/>.
        /// </summary>
        /// <param name="item">The xml node to create the item from.</param>
        /// <returns>The created <see cref="SyndicationItem"/>.</returns>
        protected abstract SyndicationItem CreateFeedItem(XmlNode item);

        /// <summary>
        /// Will get a value of an xml node based on an xpath query.
        /// </summary>
        /// <param name="parent">The parent xml node to search from.</param>
        /// <param name="xpath">The xpath query.</param>
        /// <returns>The value if found, null otherwise.</returns>
        protected string GetValue(XmlNode parent, string xpath)
        {
            XmlNode child = parent.SelectSingleNode(xpath, Namespaces);
            return child == null ? string.Empty : child.InnerText;
        }

        private void SetupNamespaces(XmlNode root)
        {
            if (!string.IsNullOrEmpty(root.NamespaceURI))
            {
                Namespaces = new XmlNamespaceManager(XmlFeed.NameTable);
                Namespaces.AddNamespace(NamespacePrefix, root.NamespaceURI);
            }
        }
    }
}