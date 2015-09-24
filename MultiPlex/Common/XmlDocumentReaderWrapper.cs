using System.Xml;

namespace MultiPlex.Common
{
    public class XmlDocumentReaderWrapper : IXmlDocumentReader
    {
        public XmlDocument Read(string path)
        {
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