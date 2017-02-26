using System.Xml;

namespace MultiPlex.Common
{
    public interface IXmlDocumentReader
    {
        XmlDocument Read(string path);
    }
}