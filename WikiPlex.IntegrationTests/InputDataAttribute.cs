using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit.Extensions;

namespace WikiPlex.IntegrationTests
{
    public class InputDataAttribute : DataAttribute
    {
        public InputDataAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
        {
            string searchPattern = "WikiPlex.IntegrationTests.Data." + Name + ".";

            return from n in Assembly.GetExecutingAssembly().GetManifestResourceNames()
                   where n.StartsWith(searchPattern) && n.EndsWith(".wiki")
                   select new object[]
                              {
                                  n.Substring(searchPattern.Length),
                                  n.Substring(searchPattern.Length, n.Length - (searchPattern.Length + 5)) + ".html",
                                  searchPattern
                              };
        }

        public static string ReadContent(string prefix, string fileName)
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(prefix + fileName)))
            {
                return reader.ReadToEnd();
            }
        }
    }
}