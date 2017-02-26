using System.Xml;

namespace WikiPlex.Syndication
{
    /// <summary>
    /// Defines the <see cref="SyndicationReader"/> contract.
    /// </summary>
    public interface ISyndicationReader
    {
        /// <summary>
        /// Will read an <see cref="XmlDocument"/> and convert it into a <see cref="SyndicationFeed"/> based on the syndication type.
        /// </summary>
        /// <param name="xmlDocument">The xml document to read.</param>
        /// <returns>The created syndication feed.</returns>
        SyndicationFeed Read(XmlDocument xmlDocument);
    }
}