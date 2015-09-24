using System.Xml;
using WikiPlex.Syndication;

namespace WikiPlex.IntegrationTests
{
    public class LocalXmlReader : IXmlDocumentReader
    {
        private const string LOCAL_PATH_PREFIX = "http://local/Data/";

        public XmlDocument Read(string path)
        {
            if (path.StartsWith(LOCAL_PATH_PREFIX))
                path = path.Substring(LOCAL_PATH_PREFIX.Length);

            string[] parts = path.Split('/');
            string ns = parts[0];
            string name = parts[1];

            var xdoc = new XmlDocument();
            xdoc.LoadXml(InputDataAttribute.ReadContent("WikiPlex.IntegrationTests.Data." + ns + ".", name));
            return xdoc;
        }
    }
}