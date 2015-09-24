using System;
using Should;
using WikiPlex.Syndication;
using Xunit;

namespace WikiPlex.Tests.Syndication
{
    public class SyndicationDateFacts
    {
        [Fact]
        public void Should_parse_date_from_rss_format_correctly()
        {
            const string date = "Sun, 19 May 2002 15:21:36 GMT";
            var syndDate = new SyndicationDate(date);

            string result = syndDate.ToString();

            result.ShouldEqual("Sunday, May 19, 2002");
        }

        [Fact]
        public void Should_parse_date_from_atom_format_correctly()
        {
            const string date = "2003-12-13T18:30:02Z";
            var syndDate = new SyndicationDate(date);

            string result = syndDate.ToString();

            result.ShouldEqual("Saturday, December 13, 2003");
        }

        [Fact]
        public void Should_parse_month_day_as_this_year()
        {
            const string date = "Feb 03";
            var syndDate = new SyndicationDate(date);

            string result = syndDate.ToString();

            result.ShouldEqual(DateTime.Parse(date).ToLongDateString());
        }

        [Fact]
        public void Should_parse_invalid_date_format_and_return_explicit_value()
        {
            const string date = "is it right";
            var syndDate = new SyndicationDate(date);

            string result = syndDate.ToString();

            result.ShouldEqual(date);
        }
    }
}