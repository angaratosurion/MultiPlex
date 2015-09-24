using System;
using System.Xml;
using WikiPlex.Common;

namespace WikiPlex.Syndication
{
    /// <summary>
    /// This class is used for converting an <see cref="XmlDocument"/> into a <see cref="SyndicationFeed"/> based on the feed type.
    /// </summary>
    public class SyndicationReader : ISyndicationReader
    {
        /// <summary>
        /// Will read an <see cref="XmlDocument"/> and convert it into a <see cref="SyndicationFeed"/> based on the syndication type.
        /// </summary>
        /// <param name="xmlDocument">The xml document to read.</param>
        /// <returns>The created syndication feed.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the xmlDocument is null.</exception>
        public SyndicationFeed Read(XmlDocument xmlDocument)
        {
            return CreateReader(xmlDocument).Read();
        }

        private static IFeedReader CreateReader(XmlDocument xmlDocument)
        {
            Guard.NotNull(xmlDocument, "xmlDocument");

            string root = xmlDocument.DocumentElement.Name;
            string xmlns = xmlDocument.DocumentElement.GetAttribute("xmlns");

            if (string.Compare(root, "rss", StringComparison.OrdinalIgnoreCase) == 0)
                return new RssFeedReader(xmlDocument);

            if (string.Compare(root, "feed", StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (string.Compare(xmlns, "http://www.w3.org/2005/Atom", StringComparison.OrdinalIgnoreCase) == 0)
                    return new AtomFeedReader(xmlDocument);

                if (string.Compare(xmlns, "http://purl.org/atom/ns#", StringComparison.OrdinalIgnoreCase) == 0)
                    return new GoogleAtomFeedReader(xmlDocument);
            }

            throw new ArgumentException("Syndication Feed Not Supported", "url");
        }
    }
}