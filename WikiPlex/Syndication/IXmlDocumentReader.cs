using System.Xml;

namespace WikiPlex.Syndication
{
    /// <summary>
    /// Defines the <see cref="XmlDocumentReaderWrapper"/> contract.
    /// </summary>
    public interface IXmlDocumentReader
    {
        /// <summary>
        /// Will read an <see cref="XmlDocument"/> from the specified path.
        /// </summary>
        /// <param name="path">The path to read the document from.</param>
        /// <returns>The loaded <see cref="XmlDocument"/> or null if any exception occurs during loading.</returns>
        XmlDocument Read(string path);
    }
}