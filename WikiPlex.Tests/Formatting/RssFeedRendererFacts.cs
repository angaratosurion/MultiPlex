using System;
using System.Xml;
using Moq;
using WikiPlex.Formatting;
using WikiPlex.Syndication;
using Xunit;
using Xunit.Extensions;

namespace WikiPlex.Tests.Formatting
{
    public class RssFeedRendererFacts
    {
        public class CanExpand
        {
            [Fact]
            public void Should_indicate_that_renderer_can_resolve_rss_feed_scope()
            {
                var renderer = new RssFeedRenderer(new Mock<IXmlDocumentReader>().Object, new Mock<ISyndicationReader>().Object);

                bool result = renderer.CanExpand(ScopeName.RssFeed);

                Assert.True(result);
            }
        }

        public class Expand
        {
            [Fact]
            public void Will_parse_the_content_and_return_an_unresolved_macro_if_url_is_not_specified()
            {
                var renderer = new RssFeedRenderer(new Mock<IXmlDocumentReader>().Object, new Mock<ISyndicationReader>().Object);

                string output = renderer.Expand(ScopeName.RssFeed, "foo", x => x, x => x);

                Assert.Equal("<span class=\"unresolved\">Cannot resolve rss macro, invalid parameter 'url'.</span>", output);
            }

            [Fact]
            public void Will_parse_the_content_and_return_an_unresolved_macro_if_rss_feed_is_not_specified()
            {
                var renderer = new RssFeedRenderer(new Mock<IXmlDocumentReader>().Object, new Mock<ISyndicationReader>().Object);

                string output = renderer.Expand(ScopeName.RssFeed, "url=foo", x => x, x => x);

                Assert.Equal("<span class=\"unresolved\">Cannot resolve rss macro, invalid parameter 'url'.</span>", output);
            }

            [Fact]
            public void Will_parse_the_content_and_return_an_unresolved_macro_if_max_is_not_a_number()
            {
                var renderer = new RssFeedRenderer(new Mock<IXmlDocumentReader>().Object, new Mock<ISyndicationReader>().Object);

                string output = renderer.Expand(ScopeName.RssFeed, "url=http://localhost/rss,max=a", x => x, x => x);

                Assert.Equal("<span class=\"unresolved\">Cannot resolve rss macro, invalid parameter 'max'.</span>", output);
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(0)]
            [InlineData(21)]
            public void Will_parse_the_content_and_return_an_unresolved_macro_if_max_is_not_in_range(int max)
            {
                var renderer = new RssFeedRenderer(new Mock<IXmlDocumentReader>().Object, new Mock<ISyndicationReader>().Object);

                string output = renderer.Expand(ScopeName.RssFeed, "url=http://localhost/rss,max=" + max, x => x, x => x);

                Assert.Equal("<span class=\"unresolved\">Cannot resolve rss macro, invalid parameter 'max'.</span>", output);
            }

            [Fact]
            public void Will_parse_the_content_and_return_an_unresolved_macro_if_titles_only_is_not_a_boolean()
            {
                var renderer = new RssFeedRenderer(new Mock<IXmlDocumentReader>().Object, new Mock<ISyndicationReader>().Object);

                string output = renderer.Expand(ScopeName.RssFeed, "url=http://localhost/rss,titlesOnly=a", x => x, x => x);

                Assert.Equal("<span class=\"unresolved\">Cannot resolve rss macro, invalid parameter 'titlesOnly'.</span>", output);
            }

            [Fact]
            public void Will_parse_the_content_and_return_an_unresolved_macro_xml_document_is_null()
            {
                var xmlLoader = new Mock<IXmlDocumentReader>();
                xmlLoader.Setup(x => x.Read(It.IsAny<string>())).Returns<XmlDocument>(null);
                var renderer = new RssFeedRenderer(xmlLoader.Object, new Mock<ISyndicationReader>().Object);

                string output = renderer.Expand(ScopeName.RssFeed, "url=http://localhost/rss", x => x, x => x);

                Assert.Equal("<span class=\"unresolved\">Cannot resolve rss macro, invalid parameter 'url'.</span>", output);
            }

            [Fact]
            public void Will_parse_the_content_and_render_the_rss_feed()
            {
                var xmlLoader = new Mock<IXmlDocumentReader>();
                var syndicationReader = new Mock<ISyndicationReader>();
                var renderer = new RssFeedRenderer(xmlLoader.Object, syndicationReader.Object);
                xmlLoader.Setup(x => x.Read("http://localhost/rss")).Returns(new XmlDocument());
                var feed = new SyndicationFeed
                               {
                                   Title = "Localhost"
                               };
                feed.Items.Add(new SyndicationItem
                                   {
                                       Title = "Title1",
                                       Description = "Description1",
                                       Link = "http://localhost/1",
                                       Date = new SyndicationDate("Tue, 17 Feb 2009 19:09:32 GMT")
                                   });
                feed.Items.Add(new SyndicationItem
                                   {
                                       Title = "Title2",
                                       Description = "Description2",
                                       Link = "http://localhost/2",
                                       Date = new SyndicationDate("Tue, 17 Feb 2009 19:09:32 GMT")
                                   });
                syndicationReader.Setup(x => x.Read(It.IsAny<XmlDocument>())).Returns(feed);

                string output = renderer.Expand(ScopeName.RssFeed, "url=http://localhost/rss", x => x, x => x);

                Assert.Equal(@"<div class=""rss""><div class=""accentbar""><span class=""left"">&nbsp;</span>Localhost News Feed<span class=""right"">&nbsp;</span></div><div class=""entry""><div class=""title""><a href=""http://localhost/1"">Title1</a></div><div class=""moreinfo""><span class=""date"">Tuesday, February 17, 2009</span> &nbsp;|&nbsp; <span class=""source"">From <a target=""_blank"" href=""http://localhost/rss"">Localhost</a></span></div><p>Description1</p></div><div class=""entry""><div class=""title""><a href=""http://localhost/2"">Title2</a></div><div class=""moreinfo""><span class=""date"">Tuesday, February 17, 2009</span> &nbsp;|&nbsp; <span class=""source"">From <a target=""_blank"" href=""http://localhost/rss"">Localhost</a></span></div><p>Description2</p></div><div class=""accentbar""><span class=""left"">&nbsp;</span>Localhost News Feed<span class=""right"">&nbsp;</span></div></div>"
                             , output);
            }

            [Fact]
            public void Will_parse_the_content_and_use_the_max_value()
            {
                var xmlLoader = new Mock<IXmlDocumentReader>();
                var syndicationReader = new Mock<ISyndicationReader>();
                var renderer = new RssFeedRenderer(xmlLoader.Object, syndicationReader.Object);
                xmlLoader.Setup(x => x.Read("http://localhost/rss")).Returns(new XmlDocument());
                var feed = new SyndicationFeed
                               {
                                   Title = "Localhost"
                               };
                feed.Items.Add(new SyndicationItem
                                   {
                                       Title = "Title1",
                                       Description = "Description1",
                                       Link = "http://localhost/1",
                                       Date = new SyndicationDate("Tue, 17 Feb 2009 19:09:32 GMT")
                                   });
                feed.Items.Add(new SyndicationItem
                                   {
                                       Title = "Title2",
                                       Description = "Description2",
                                       Link = "http://localhost/2",
                                       Date = new SyndicationDate("Tue, 17 Feb 2009 19:09:32 GMT")
                                   });
                syndicationReader.Setup(x => x.Read(It.IsAny<XmlDocument>())).Returns(feed);

                string output = renderer.Expand(ScopeName.RssFeed, "url=http://localhost/rss,max=1", x => x, x => x);

                Assert.Equal(@"<div class=""rss""><div class=""accentbar""><span class=""left"">&nbsp;</span>Localhost News Feed<span class=""right"">&nbsp;</span></div><div class=""entry""><div class=""title""><a href=""http://localhost/1"">Title1</a></div><div class=""moreinfo""><span class=""date"">Tuesday, February 17, 2009</span> &nbsp;|&nbsp; <span class=""source"">From <a target=""_blank"" href=""http://localhost/rss"">Localhost</a></span></div><p>Description1</p></div><div class=""accentbar""><span class=""left"">&nbsp;</span>Localhost News Feed<span class=""right"">&nbsp;</span></div></div>"
                             , output);
            }

            [Fact]
            public void Will_parse_the_content_and_render_only_the_titles()
            {
                var xmlLoader = new Mock<IXmlDocumentReader>();
                var syndicationReader = new Mock<ISyndicationReader>();
                var renderer = new RssFeedRenderer(xmlLoader.Object, syndicationReader.Object);
                xmlLoader.Setup(x => x.Read("http://localhost/rss")).Returns(new XmlDocument());
                var feed = new SyndicationFeed
                               {
                                   Title = "Localhost"
                               };
                feed.Items.Add(new SyndicationItem
                                   {
                                       Title = "Title1",
                                       Description = "Description1",
                                       Link = "http://localhost/1",
                                       Date = new SyndicationDate("Tue, 17 Feb 2009 19:09:32 GMT")
                                   });
                feed.Items.Add(new SyndicationItem
                                   {
                                       Title = "Title2",
                                       Description = "Description2",
                                       Link = "http://localhost/2",
                                       Date = new SyndicationDate("Tue, 17 Feb 2009 19:09:32 GMT")
                                   });
                syndicationReader.Setup(x => x.Read(It.IsAny<XmlDocument>())).Returns(feed);

                string output = renderer.Expand(ScopeName.RssFeed, "url=http://localhost/rss,titlesOnly=true", x => x, x => x);

                Assert.Equal(@"<div class=""rss""><div class=""accentbar""><span class=""left"">&nbsp;</span>Localhost News Feed<span class=""right"">&nbsp;</span></div><div class=""entry""><div class=""title""><a href=""http://localhost/1"">Title1</a></div><div class=""moreinfo""><span class=""date"">Tuesday, February 17, 2009</span> &nbsp;|&nbsp; <span class=""source"">From <a target=""_blank"" href=""http://localhost/rss"">Localhost</a></span></div></div><div class=""entry""><div class=""title""><a href=""http://localhost/2"">Title2</a></div><div class=""moreinfo""><span class=""date"">Tuesday, February 17, 2009</span> &nbsp;|&nbsp; <span class=""source"">From <a target=""_blank"" href=""http://localhost/rss"">Localhost</a></span></div></div><div class=""accentbar""><span class=""left"">&nbsp;</span>Localhost News Feed<span class=""right"">&nbsp;</span></div></div>"
                             , output);
            }

            [Fact]
            public void Will_render_an_unresolved_macro_when_the_syndication_factory_cannot_parse_the_feed()
            {
                var xmlLoader = new Mock<IXmlDocumentReader>();
                var syndicationReader = new Mock<ISyndicationReader>();
                xmlLoader.Setup(x => x.Read("http://localhost/rss")).Returns(new XmlDocument());
                syndicationReader.Setup(x => x.Read(It.IsAny<XmlDocument>())).Throws<ArgumentException>();
                var renderer = new RssFeedRenderer(xmlLoader.Object, syndicationReader.Object);

                string output = renderer.Expand(ScopeName.RssFeed, "url=http://localhost/rss", x => x, x => x);

                Assert.Equal("<span class=\"unresolved\">Cannot resolve rss macro, invalid parameter 'url'.</span>", output);
            }
        }
    }
}