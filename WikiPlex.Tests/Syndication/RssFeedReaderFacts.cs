using System;
using System.Xml;
using Should;
using WikiPlex.Syndication;
using Xunit;

namespace WikiPlex.Tests.Syndication
{
    public class RssFeedReaderFacts
    {
        private const string encodedXml = @"<?xml version=""1.0"" encoding=""ISO-8859-1""?>
<rss version=""0.91"">
	<channel>
		<item>
			<title>Item 1 Title</title> 
			<link>http://item1.com</link> 
			<description>&lt;strong&gt;Hello&lt;/strong&gt;</description>
            <pubDate>Sun, 19 May 2002 15:21:36 GMT</pubDate> 
        </item>
		<item>
			<title>Item 2 Title</title> 
			<link>http://item2.com</link> 
			<description><![CDATA[<strong>Hello</strong>]]></description> 
            <pubDate>Mon, 20 May 2002 15:21:36 GMT</pubDate>
		</item>
	</channel>
</rss>";
        private const string xml = @"<?xml version=""1.0"" encoding=""ISO-8859-1""?>
<rss version=""0.91"">
	<channel>
		<title>RssSample</title> 
		<link>http://rsssample.com</link> 
		<description>Rss Description</description> 
		<language>en-us</language> 
		<copyright>Copyright 2009, Microsoft.</copyright> 
		<managingEditor>test@user.com</managingEditor> 
		<webMaster>test@user.com</webMaster> 
		<image>
			<title>RssSample</title> 
			<url>http://rsssample/sample.gif</url> 
			<link>http://rsssample.com</link> 
			<width>88</width> 
			<height>31</height> 
			<description>Image Description</description> 
        </image>
		<item>
			<title>Item 1 Title</title> 
			<link>http://item1.com</link> 
			<description>Item 1 Description</description>
            <pubDate>Sun, 19 May 2002 15:21:36 GMT</pubDate> 
        </item>
		<item>
			<title>Item 2 Title</title> 
			<link>http://item2.com</link> 
			<description>Item 2 Description</description> 
            <pubDate>Mon, 20 May 2002 15:21:36 GMT</pubDate>
		</item>
	</channel>
</rss>";

        [Fact]
        public void Should_throw_ArgumentNullException_when_xml_document_is_null()
        {
            var reader = new RssFeedReader(null);

            var ex = Record.Exception(() => reader.Read()) as ArgumentNullException;

            ex.ShouldNotBeNull();
        }

        [Fact]
        public void Should_throw_ArgumentException_when_feed_contains_no_channels()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<rss />");
            var reader = new RssFeedReader(xmlDoc);

            var ex = Record.Exception(() => reader.Read()) as ArgumentException;

            ex.ShouldNotBeNull();
        }

        [Fact]
        public void Should_read_the_feed_info_correctly()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var reader = new RssFeedReader(xmlDoc);

            SyndicationFeed feed = reader.Read();

            feed.ShouldNotBeNull();
            feed.Title.ShouldEqual("RssSample");
        }

        [Fact]
        public void Should_read_the_items_correctly()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var reader = new RssFeedReader(xmlDoc);

            SyndicationFeed feed = reader.Read();

            feed.Items.Count.ShouldEqual(2);
            feed.Items[0].Title.ShouldEqual("Item 1 Title");
            feed.Items[0].Description.ShouldEqual("Item 1 Description");
            feed.Items[0].Link.ShouldEqual("http://item1.com");
            feed.Items[0].Date.Value.ShouldEqual("Sun, 19 May 2002 15:21:36 GMT");
            feed.Items[1].Title.ShouldEqual("Item 2 Title");
            feed.Items[1].Description.ShouldEqual("Item 2 Description");
            feed.Items[1].Link.ShouldEqual("http://item2.com");
            feed.Items[1].Date.Value.ShouldEqual("Mon, 20 May 2002 15:21:36 GMT");
        }

        [Fact]
        public void Should_read_the_encoded_content_correctly()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(encodedXml);
            var reader = new RssFeedReader(xmlDoc);

            SyndicationFeed feed = reader.Read();

            feed.Items.Count.ShouldEqual(2);
            feed.Items[0].Description.ShouldEqual("<strong>Hello</strong>");
            feed.Items[1].Description.ShouldEqual("<strong>Hello</strong>");
        }
    }
}