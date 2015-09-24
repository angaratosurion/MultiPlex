using System.Xml;

namespace WikiPlex.Common
{
    public interface IXmlDocumentReader
    {
        XmlDocument Read(string path);
    }
}