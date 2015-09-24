using System;
using System.Xml;
using WikiPlex.Common;

namespace WikiPlex.Syndication
{
    public class FeedReaderFactory : IFeedReaderFactory
    {
        public IFeedReader CreateReader(XmlDocument xmlDocument)
        {
            Guard.NotNull(xmlDocument, "xmlDocument");

            string root = xmlDocument.DocumentElement.Name;
            string xmlns = xmlDocument.DocumentElement.GetAttribute("xmlns");

            if (string.Compare(root, "rss", StringComparison.OrdinalIgnoreCase) == 0)
                return new RssFeedReader();

            if (string.Compare(root, "feed", StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (string.Compare(xmlns, "http://www.w3.org/2005/Atom", StringComparison.OrdinalIgnoreCase) == 0
                    || string.Compare(xmlns, "http://purl.org/atom/ns#", StringComparison.OrdinalIgnoreCase) == 0)
                    return new AtomFeedReader();
            }

            throw new ArgumentException("Syndication Feed Not Supported", "xmlDocument");
        }
    }
}