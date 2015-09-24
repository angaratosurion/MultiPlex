using System;
using System.Xml;
using WikiPlex.Syndication;
using Xunit;

namespace WikiPlex.Tests.Syndication
{
    public class FeedReaderFactoryFacts
    {
        public class CreateReader
        {
            [Fact]
            public void Will_throw_ArgumentNullException_when_xml_document_is_null()
            {
                var factory = new FeedReaderFactory();

                Exception ex = Record.Exception(() => factory.CreateReader(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void Will_return_RssFeedReader_when_document_element_is_rss()
            {
                var factory = new FeedReaderFactory();
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(@"<?xml version=""1.0""?><rss version=""0.91"" />");

                var reader = factory.CreateReader(xmlDoc);

                Assert.IsType<RssFeedReader>(reader);
            }

            [Fact]
            public void Will_return_AtomFeedReader_when_document_element_is_feed_and_correct_xmlns()
            {
                var factory = new FeedReaderFactory();
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(@"<?xml version=""1.0""?><feed xmlns=""http://www.w3.org/2005/Atom"" />");

                var reader = factory.CreateReader(xmlDoc);

                Assert.IsType<AtomFeedReader>(reader);
            }

            [Fact]
            public void Will_return_AtomFeedReader_when_document_element_is_feed_and_correct_xmlns_for_google_atom()
            {
                var factory = new FeedReaderFactory();
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(@"<?xml version=""1.0""?><feed xmlns=""http://purl.org/atom/ns#"" />");

                var reader = factory.CreateReader(xmlDoc);

                Assert.IsType<AtomFeedReader>(reader);
            }

            [Fact]
            public void Will_throw_ArgumentException_when_document_element_is_unsupported()
            {
                var factory = new FeedReaderFactory();
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(@"<?xml version=""1.0""?><something />");

                var ex = Record.Exception(() => factory.CreateReader(xmlDoc));

                Assert.IsType<ArgumentException>(ex);
            }
        }
    }
}