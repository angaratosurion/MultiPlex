using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml;
using WikiPlex.Formatting.Renderers;
using WikiPlex.Syndication;

namespace WikiPlex.PerformanceHarnass
{
    internal class Program
    {
        private static readonly string[] inputData;
        private const int maxThreadCount = 50;
        private const int threadDelay = 1000 * 1;
        private static int currentInput = 0;
        static bool everyoneDie;
        private static readonly PerformanceCounter AvgTimeTicks;
        private static readonly PerformanceCounter AvgTimeTicksBase;
        private static readonly PerformanceCounter NumThreads;
        private static readonly PerformanceCounter AvgPerSec;

        static Program()
        {
            inputData = new string[3];
            inputData[0] = GetDataInput("FormatAndLayout");
            inputData[1] = GetDataInput("Macros");
            inputData[2] = GetDataInput("SilverlightProject");

            if (!PerformanceCounterCreation.Exists())
                PerformanceCounterCreation.Create();
            Renderers.Register(new SyndicatedFeedRenderer(new LocalXmlReader(), new SyndicationReader()));

            AvgTimeTicks = new PerformanceCounter("WikiPlex.PerformanceCounters", "Avg Rendering Time (seconds)", string.Empty, false);
            AvgTimeTicksBase = new PerformanceCounter("WikiPlex.PerformanceCounters", "Avg Rendering Time Base", string.Empty, false);
            NumThreads = new PerformanceCounter("WikiPlex.PerformanceCounters", "Num Rendering Threads", string.Empty, false);
            AvgPerSec = new PerformanceCounter("WikiPlex.PerformanceCounters", "Avg Rendering Per Sec", string.Empty, false);
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Please start the performance counter, and press the enter key to continue.");
            Console.ReadLine();

            NumThreads.RawValue = 0;

            var creator = new Thread(ThreadCreator);
            creator.Start();

            Console.WriteLine("Press the enter key to exit and kill every thread.");
            Console.ReadLine();
            everyoneDie = true;

            PerformanceCounterCreation.Remove();
        }

        private static void ThreadCreator()
        {
            for (int i = 0; i < maxThreadCount; i++)
            {
                if (everyoneDie) return;
                var t = new Thread(Worker);
                t.Start();

                Thread.Sleep(threadDelay);
            }
        }

        private static void Worker()
        {
            Console.WriteLine("Creating worker thread.");
            NumThreads.Increment();
            string wikiContent = inputData[currentInput];
            currentInput = (currentInput + 1) == 3 ? 0 : currentInput + 1;

            while (!everyoneDie)
            {
                RenderWiki(wikiContent);
            }

            Console.WriteLine("Killing worker thread.");
            NumThreads.Decrement();
        }

        private static void RenderWiki(string wikiContent)
        {
            var st = new Stopwatch();
            st.Start();
            var engine = new WikiEngine();
            engine.Render(wikiContent);
            st.Stop();

            //Console.WriteLine("Time: {0}", st.ElapsedMilliseconds / 1000.0);
            AvgTimeTicks.IncrementBy(st.ElapsedTicks);
            AvgTimeTicksBase.Increment();
            AvgPerSec.Increment();

            st.Reset();
        }

        private static string GetDataInput(string dataName)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WikiPlex.PerformanceHarnass.Data." + dataName + ".wiki"))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public class LocalXmlReader : IXmlDocumentReader
        {
            public XmlDocument Read(string path)
            {
                if (path.StartsWith("http://local/"))
                    path = path.Substring("http://local/".Length);

                var xdoc = new XmlDocument();
                xdoc.Load(path);
                return xdoc;
            }
        }
    }
}