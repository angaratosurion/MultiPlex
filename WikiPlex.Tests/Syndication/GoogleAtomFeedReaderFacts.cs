using System;
using System.Xml;
using Should;
using WikiPlex.Syndication;
using Xunit;

namespace WikiPlex.Tests.Syndication
{
    public class GoogleAtomFeedReaderFacts
    {
        private const string contentBasedXml = @"<?xml version=""1.0"" encoding=""ISO-8859-1""?>
<feed xmlns=""http://purl.org/atom/ns#"">
	<title>AtomSample</title> 
	<entry>
		<title type=""text"">Item 1 Title</title> 
		<link rel=""alternate"" href=""http://item1.com"" />
        <content type=""html"">The html content</content>
		<summary type=""text"">Item 1 Description</summary>
        <modified>2003-12-13T18:30:02Z</modified> 
    </entry>
	<entry>
		<title type=""text"">Item 2 Title</title> 
		<link rel=""alternate"" href=""http://item2.com"" />
		<summary type=""html"">Item 2 Description</summary> 
        <modified>2003-12-14T18:30:02Z</modified>
	</entry>
</feed>";
        private const string encodedXml = @"<?xml version=""1.0"" encoding=""ISO-8859-1""?>
<feed xmlns=""http://purl.org/atom/ns#"">
	<title>AtomSample</title> 
	<entry>
		<title type=""text"">Item 1 Title</title> 
		<link rel=""alternate"" href=""http://item1.com"" />
		<summary type=""text"">&lt;strong&gt;Hello&lt;/strong&gt;</summary>
        <modified>2003-12-13T18:30:02Z</modified> 
    </entry>
	<entry>
		<title type=""text"">Item 2 Title</title> 
		<link rel=""alternate"" href=""http://item2.com"" />
		<summary type=""html""><![CDATA[<strong>Hello</strong>]]></summary> 
        <modified>2003-12-14T18:30:02Z</modified>
	</entry>
</feed>";
        private const string xml = @"<?xml version=""1.0"" encoding=""ISO-8859-1""?>
<feed xmlns=""http://purl.org/atom/ns#"">
	<title>AtomSample</title> 
	<entry>
		<title type=""text"">Item 1 Title</title> 
		<link rel=""alternate"" href=""http://item1.com"" />
		<summary type=""text"">Item 1 Description</summary>
        <modified>2003-12-13T18:30:02Z</modified> 
    </entry>
	<entry>
		<title type=""text"">Item 2 Title</title> 
		<link rel=""alternate"" href=""http://item2.com"" />
		<summary type=""html"">Item 2 Description</summary> 
        <modified>2003-12-14T18:30:02Z</modified>
	</entry>
</feed>";

        [Fact]
        public void Should_throw_ArgumentNullException_when_xml_document_is_null()
        {
            var reader = new GoogleAtomFeedReader(null);

            var ex = Record.Exception(() => reader.Read()) as ArgumentNullException;

            ex.ShouldNotBeNull();
        }

        [Fact]
        public void Should_read_the_feed_info_correctly()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var reader = new GoogleAtomFeedReader(xmlDoc);

            SyndicationFeed feed = reader.Read();

            feed.ShouldNotBeNull();
            feed.Title.ShouldEqual("AtomSample");
        }

        [Fact]
        public void Should_read_the_items_correctly()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var reader = new GoogleAtomFeedReader(xmlDoc);

            SyndicationFeed feed = reader.Read();

            feed.Items.Count.ShouldEqual(2);
            feed.Items[0].Title.ShouldEqual("Item 1 Title");
            feed.Items[0].Description.ShouldEqual("Item 1 Description");
            feed.Items[0].Link.ShouldEqual("http://item1.com");
            feed.Items[0].Date.Value.ShouldEqual("2003-12-13T18:30:02Z");
            feed.Items[1].Title.ShouldEqual("Item 2 Title");
            feed.Items[1].Description.ShouldEqual("Item 2 Description");
            feed.Items[1].Link.ShouldEqual("http://item2.com");
            feed.Items[1].Date.Value.ShouldEqual("2003-12-14T18:30:02Z");
        }

        [Fact]
        public void Should_read_the_encoded_content_correctly()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(encodedXml);
            var reader = new GoogleAtomFeedReader(xmlDoc);

            SyndicationFeed feed = reader.Read();

            feed.Items.Count.ShouldEqual(2);
            feed.Items[0].Description.ShouldEqual("<strong>Hello</strong>");
            feed.Items[1].Description.ShouldEqual("<strong>Hello</strong>");
        }

        [Fact]
        public void Should_read_the_content_element_over_summary()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(contentBasedXml);
            var reader = new GoogleAtomFeedReader(xmlDoc);

            SyndicationFeed feed = reader.Read();

            feed.Items.Count.ShouldEqual(2);
            feed.Items[0].Description.ShouldEqual("The html content");
        }
    }
}