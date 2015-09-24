using System;
using System.Xml;
using WikiPlex.Common;

namespace WikiPlex.Syndication
{
    /// <summary>
    /// Encapsulates reading an xml document from a path.
    /// </summary>
    public class XmlDocumentReaderWrapper : IXmlDocumentReader
    {
        /// <summary>
        /// Will read an <see cref="XmlDocument"/> from the specified path.
        /// </summary>
        /// <param name="path">The path to read the document from.</param>
        /// <returns>The loaded <see cref="XmlDocument"/> or null if any exception occurs during loading.</returns>
        /// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
        /// <exception cref="ArgumentException">Thrown when path is empty.</exception>
        public XmlDocument Read(string path)
        {
            Guard.NotNullOrEmpty(path, "path");

            try
            {
                var xdoc = new XmlDocument();
                xdoc.Load(path);
                return xdoc;
            }
            catch
            {
                return null;
            }
        }
    }
}