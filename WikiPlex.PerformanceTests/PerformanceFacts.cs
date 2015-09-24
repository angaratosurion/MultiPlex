using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using WikiPlex.Compilation;
using WikiPlex.Compilation.Macros;
using WikiPlex.Formatting.Renderers;
using WikiPlex.IntegrationTests;
using WikiPlex.Parsing;
using WikiPlex.Syndication;
using Xunit;
using ThreadState = System.Threading.ThreadState;

namespace WikiPlex.PerformanceTests
{
    public class PerformanceFacts : IDisposable
    {
        private readonly WikiEngine engine;

        class PerformanceWikiEngine : WikiEngine
        {
            public PerformanceWikiEngine() : base(new Parser(new MacroCompiler()))
            {}
        }

        public PerformanceFacts()
        {
            // clean the wiki engine
            foreach (IMacro macro in Macros.All.ToList())
                Macros.Unregister(macro);

            // register the local rss reader
            Renderers.Register(new SyndicatedFeedRenderer(new LocalXmlReader(), new SyndicationReader()));

            engine = new PerformanceWikiEngine();
        }

        public void Dispose()
        {
            RegisterMacros();

            Renderers.Register<SyndicatedFeedRenderer>();
        }

        private void ExecutePerformanceTest(string fileName, int millisecondsToFinish)
        {
            ExecutePerformanceTest(fileName, millisecondsToFinish, Renderers.All);
        }

        private void ExecutePerformanceTest(string fileName, int millisecondsToFinish, IEnumerable<IRenderer> renderers)
        {
            string content = ReadContent("WikiPlex.PerformanceTests.Data.", fileName);
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            engine.Render(content, renderers);
            stopWatch.Stop();

            Trace.WriteLine(fileName + ": " + stopWatch.ElapsedMilliseconds + "ms, expected under" + millisecondsToFinish + "ms");

            Assert.True(stopWatch.ElapsedMilliseconds < millisecondsToFinish, "Finished in " + stopWatch.ElapsedMilliseconds + "ms, expected under " + millisecondsToFinish + "ms");
        }

        private static string ReadContent(string prefix, string fileName)
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(prefix + fileName)))
                return reader.ReadToEnd();
        }

        [Fact]
        public void Should_format_bold_performantly()
        {
            Macros.Register<BoldMacro>();
            ExecutePerformanceTest("Bold.wiki", 500);
            ExecutePerformanceTest("Bold.wiki", 500);
            Macros.Unregister<BoldMacro>();
        }

        [Fact]
        public void Should_format_italics_performantly()
        {
            Macros.Register<ItalicsMacro>();
            ExecutePerformanceTest("Italics.wiki", 500);
            ExecutePerformanceTest("Italics.wiki", 500);
            Macros.Unregister<ItalicsMacro>();
        }

        [Fact]
        public void Should_format_underline_performantly()
        {
            Macros.Register<UnderlineMacro>();
            ExecutePerformanceTest("Underline.wiki", 500);
            ExecutePerformanceTest("Underline.wiki", 500);
            Macros.Unregister<UnderlineMacro>();
        }

        [Fact]
        public void Should_format_headings_performantly()
        {
            Macros.Register<HeadingsMacro>();
            ExecutePerformanceTest("Headings.wiki", 750);
            ExecutePerformanceTest("Headings.wiki", 750);
            Macros.Unregister<HeadingsMacro>();
        }

        [Fact]
        public void Should_format_strikethrough_performantly()
        {
            Macros.Register<StrikethroughMacro>();
            ExecutePerformanceTest("StrikeThrough.wiki", 750);
            ExecutePerformanceTest("StrikeThrough.wiki", 750);
            Macros.Unregister<StrikethroughMacro>();
        }

        [Fact]
        public void Should_format_subscript_performantly()
        {
            Macros.Register<SubscriptMacro>();
            ExecutePerformanceTest("Subscript.wiki", 600);
            ExecutePerformanceTest("Subscript.wiki", 600);
            Macros.Unregister<SubscriptMacro>();
        }

        [Fact]
        public void Should_format_superscript_performantly()
        {
            Macros.Register<SuperscriptMacro>();
            ExecutePerformanceTest("Superscript.wiki", 850);
            ExecutePerformanceTest("Superscript.wiki", 850);
            Macros.Unregister<SuperscriptMacro>();
        }

        [Fact]
        public void Should_format_content_alignment_performantly()
        {
            Macros.Register<ContentLeftAlignmentMacro>();
            Macros.Register<ContentRightAlignmentMacro>();
            ExecutePerformanceTest("ContentAlignment.wiki", 1100);
            ExecutePerformanceTest("ContentAlignment.wiki", 1100);
            Macros.Unregister<ContentLeftAlignmentMacro>();
            Macros.Unregister<ContentRightAlignmentMacro>();
        }

        [Fact]
        public void Should_format_ordered_list_performantly()
        {
            Macros.Register<ListMacro>();
            ExecutePerformanceTest("OrderedList.wiki", 2250);
            ExecutePerformanceTest("OrderedList.wiki", 2250);
            Macros.Unregister<ListMacro>();
        }

        [Fact]
        public void Should_format_unordered_list_performantly()
        {
            Macros.Register<ListMacro>();
            ExecutePerformanceTest("UnorderedList.wiki", 2250);
            ExecutePerformanceTest("UnorderedList.wiki", 2250);
            Macros.Unregister<ListMacro>();
        }

        [Fact]
        public void Should_format_mixed_list_performantly()
        {
            Macros.Register<ListMacro>();
            ExecutePerformanceTest("MixedList.wiki", 2250);
            ExecutePerformanceTest("MixedList.wiki", 2250);
            Macros.Unregister<ListMacro>();
        }

        [Fact]
        public void Should_format_indentation_performantly()
        {
            Macros.Register<IndentationMacro>();
            ExecutePerformanceTest("Indentation.wiki", 1000);
            ExecutePerformanceTest("Indentation.wiki", 1000);
            Macros.Unregister<IndentationMacro>();
        }

        [Fact]
        public void Should_format_table_performantly()
        {
            Macros.Register<TableMacro>();
            ExecutePerformanceTest("Table.wiki", 2750);
            ExecutePerformanceTest("Table.wiki", 2750);
            Macros.Unregister<TableMacro>();
        }

        [Fact]
        public void Should_format_image_performantly()
        {
            Macros.Register<ImageMacro>();
            ExecutePerformanceTest("Image.wiki", 2250);
            ExecutePerformanceTest("Image.wiki", 2250);
            Macros.Unregister<ImageMacro>();
        }

        [Fact]
        public void Should_format_horizontal_line_performantly()
        {
            Macros.Register<HorizontalLineMacro>();
            ExecutePerformanceTest("HorizontalLine.wiki", 500);
            ExecutePerformanceTest("HorizontalLine.wiki", 500);
            Macros.Unregister<HorizontalLineMacro>();
        }

        [Fact]
        public void Should_format_all_performantly()
        {
            RegisterMacros();

            ExecutePerformanceTest("AllFormatting.wiki", 3500);
            ExecutePerformanceTest("AllFormatting.wiki", 3250);
        }

        [Fact]
        public void Should_format_video_performantly()
        {
            Macros.Register<VideoMacro>();
            ExecutePerformanceTest("Video.wiki", 1500);
            ExecutePerformanceTest("Video.wiki", 1500);
            Macros.Unregister<VideoMacro>();
        }

        [Fact]
        public void Should_format_all_as_plaintext_performantly()
        {
            RegisterMacros();

            ExecutePerformanceTest("AllFormatting.wiki", 3500, new[] {new PlainTextRenderer()});
            ExecutePerformanceTest("AllFormatting.wiki", 3250, new[] { new PlainTextRenderer() });
        }

        [Fact(Skip = "Threading Not Supported")]
        public void Should_format_five_threads_concurrently_1_million_times_each_with_each_format_in_under_50ms_each()
        {
            RegisterMacros();

            var thread1 = new Thread(ExecuteThreadPerfTest);
            var thread2 = new Thread(ExecuteThreadPerfTest);
            var thread3 = new Thread(ExecuteThreadPerfTest);
            var thread4 = new Thread(ExecuteThreadPerfTest);
            var thread5 = new Thread(ExecuteThreadPerfTest);

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();

            while (thread1.ThreadState == ThreadState.Running
                   || thread2.ThreadState == ThreadState.Running
                   || thread3.ThreadState == ThreadState.Running
                   || thread4.ThreadState == ThreadState.Running
                   || thread5.ThreadState == ThreadState.Running)
            {
                Thread.Sleep(10);
            }
        }

        private void ExecuteThreadPerfTest()
        {
            for (int i = 0; i < 1000000; i++)
                ExecutePerformanceTest("FormatAndLayout.wiki", 50);
        }

        private static void RegisterMacros()
        {
            Macros.Register<BoldMacro>();
            Macros.Register<ItalicsMacro>();
            Macros.Register<UnderlineMacro>();
            Macros.Register<HeadingsMacro>();
            Macros.Register<StrikethroughMacro>();
            Macros.Register<SubscriptMacro>();
            Macros.Register<SuperscriptMacro>();
            Macros.Register<HorizontalLineMacro>();
            Macros.Register<LinkMacro>();
            Macros.Register<ImageMacro>();
            Macros.Register<SourceCodeMacro>();
            Macros.Register<ListMacro>();
            Macros.Register<EscapedMarkupMacro>();
            Macros.Register<SyndicatedFeedMacro>();
            Macros.Register<SilverlightMacro>();
            Macros.Register<VideoMacro>();
            Macros.Register<TableMacro>();
            Macros.Register<ContentLeftAlignmentMacro>();
            Macros.Register<ContentRightAlignmentMacro>();
            Macros.Register<IndentationMacro>();
        }
    }
}