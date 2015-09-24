using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using WikiPlex.Syndication;
using Xunit;
using Xunit.Extensions;

namespace WikiPlex.IntegrationTests
{
    public class XmlLoaderFacts
    {
        public class Load
        {
            [Theory]
            [InlineData("Atom")]
            [InlineData("GoogleAtom")]
            [InlineData("Rss")]
            public void Will_return_the_xml_document_with_the_xml_from_the_path_specified(string xmlFeed)
            {
                string path = Path.GetTempFileName();
                try
                {
                    var loader = new XmlDocumentReaderWrapper();
                    string expectedContent = InputDataAttribute.ReadContent("WikiPlex.IntegrationTests.Data.SyndicatedFeedFormatting.", xmlFeed + ".xml");
                    File.WriteAllText(path, expectedContent);
                    var expected = new XmlDocument();
                    expected.LoadXml(expectedContent);

                    XmlDocument xdoc = loader.Read(path);

                    Assert.NotNull(xdoc);
                    Assert.Equal(expected.OuterXml, xdoc.OuterXml);
                }
                finally
                {
                    File.Delete(path);
                }
            }

            [Theory]
            [InlineData("does not exist")]
            [InlineData("http://doesnotexist")]
            public void Will_return_null_if_feed_does_not_exist_at_path(string path)
            {
                var loader = new XmlDocumentReaderWrapper();

                XmlDocument xdoc = loader.Read(path);

                Assert.Null(xdoc);
            }
        }
    }
}