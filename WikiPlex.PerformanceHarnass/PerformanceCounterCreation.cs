using System;
using System.Diagnostics;

namespace WikiPlex.PerformanceHarnass
{
    public static class PerformanceCounterCreation
    {
        public static void Create()
        {
            Remove();

            var perfCounterCollection = new CounterCreationDataCollection();

            var averageTicksPerfCounter = new CounterCreationData
                                              {
                                                  CounterName = "Avg Rendering Time (seconds)", 
                                                  CounterHelp = "The average time, in seconds, a wiki rendering takes.", 
                                                  CounterType = PerformanceCounterType.AverageTimer32
                                              };

            var averageTicksBasePerfCounter = new CounterCreationData
                                                  {
                                                      CounterName = "Avg Rendering Time Base", 
                                                      CounterHelp = "Base counter for the Avg Rendering Time (seconds) perf counter.", 
                                                      CounterType = PerformanceCounterType.AverageBase
                                                  };

            var numThreads = new CounterCreationData
                                 {
                                     CounterName = "Num Rendering Threads", 
                                     CounterHelp = "The number of threads currently rendering.", 
                                     CounterType = PerformanceCounterType.NumberOfItemsHEX32
                                 };

            var avgReleaseQueriesPerSec = new CounterCreationData
                                              {
                                                  CounterName = "Avg Rendering Per Sec", 
                                                  CounterHelp = "The number of renders per second.", 
                                                  CounterType = PerformanceCounterType.SampleCounter
                                              };

            perfCounterCollection.Add(averageTicksPerfCounter);
            perfCounterCollection.Add(averageTicksBasePerfCounter);
            perfCounterCollection.Add(avgReleaseQueriesPerSec);
            perfCounterCollection.Add(numThreads);

            PerformanceCounterCategory category = PerformanceCounterCategory.Create(
                "WikiPlex.PerformanceCounters",
                "WikiPlex perf counters.",
                PerformanceCounterCategoryType.SingleInstance,
                perfCounterCollection);
        }

        public static void Remove()
        {
            if (Exists())
            {
                Console.WriteLine("Deleting existing WikiPlex.PerformanceCounters perf counters.");

                PerformanceCounterCategory.Delete("WikiPlex.PerformanceCounters");
            }
        }

        public static bool Exists()
        {
            return PerformanceCounterCategory.Exists("WikiPlex.PerformanceCounters");
        }
    }
}