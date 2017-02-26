using System.Xml;

namespace MultiPlex.Syndication
{
    public interface IFeedReaderFactory
    {
        IFeedReader CreateReader(XmlDocument xmlDocument);
    }
}