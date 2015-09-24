using System;
using System.IO;
using System.Text.RegularExpressions;
using WikiPlex.Formatting.Renderers;
using WikiPlex.Syndication;
using Xunit;
using Xunit.Extensions;

namespace WikiPlex.IntegrationTests
{
    public class FormattingFacts : IDisposable
    {
        private static readonly Regex WhitespaceRemovalRegex = new Regex(@"\r|\n|\t|\s{3,4}");

        public FormattingFacts()
        {
            Renderers.Register(new SyndicatedFeedRenderer(new LocalXmlReader(), new SyndicationReader()));
        }

        public void Dispose()
        {
            Renderers.Register<SyndicatedFeedRenderer>();
        }

        [Theory]
        [InputData("ContentAlignmentFormatting")]
        [InputData("TextFormatting")]
        [InputData("LinkFormatting")]
        [InputData("ImageFormatting")]
        [InputData("SourceCodeFormatting")]
        [InputData("ListFormatting")]
        [InputData("SyndicatedFeedFormatting")]
        [InputData("SilverlightFormatting")]
        [InputData("VideoFormatting")]
        [InputData("TableFormatting")]
        [InputData("FullTests")]
        [InputData("CatastrophicBacktracking")]
        [InputData("IndentationFormatting")]
        public void Will_verify_formatting(string inputFile, string expectedFile, string prefix)
        {
            string expectedText = InputDataAttribute.ReadContent(prefix, expectedFile);
            string actualText = new WikiEngine().Render(InputDataAttribute.ReadContent(prefix, inputFile));

            // comment out the following lines if you wish to compare
            // the whitespace correctly
            expectedText = WhitespaceRemovalRegex.Replace(expectedText, string.Empty);
            actualText = WhitespaceRemovalRegex.Replace(actualText, string.Empty);

            Assert.Equal(expectedText, actualText);
        }

        [Theory]
        [InputData("PlainText")]
        public void Will_verify_plain_text_formatting(string inputFile, string expectedFile, string prefix)
        {
            string expectedText = InputDataAttribute.ReadContent(prefix, expectedFile);
            string actualText = new WikiEngine().Render(InputDataAttribute.ReadContent(prefix, inputFile), new[] {new PlainTextRenderer()});

            // comment out the following lines if you wish to compare
            // the whitespace correctly
            expectedText = WhitespaceRemovalRegex.Replace(expectedText, string.Empty);
            actualText = WhitespaceRemovalRegex.Replace(actualText, string.Empty);

            Assert.Equal(expectedText, actualText);
        }

        [Fact(Skip = "This is only used to test 1-off inputs manually.")]
        //[Fact]
        public void TestIt()
        {
            string path = @"TableFormatting\WithLinks";

            string expectedText = File.ReadAllText(@"..\..\Data\" + path + ".html");
            string actualText =
                new WikiEngine().Render(File.ReadAllText(@"..\..\Data\" + path + ".wiki"));

            // comment out the following lines if you wish to compare
            // the whitespace correctly
            expectedText = WhitespaceRemovalRegex.Replace(expectedText, string.Empty);
            actualText = WhitespaceRemovalRegex.Replace(actualText, string.Empty);

            Assert.Equal(expectedText, actualText);
        }
    }
}