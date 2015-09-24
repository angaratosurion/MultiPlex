using System.Xml;

namespace WikiPlex.Syndication
{
    public interface IFeedReaderFactory
    {
        IFeedReader CreateReader(XmlDocument xmlDocument);
    }
}