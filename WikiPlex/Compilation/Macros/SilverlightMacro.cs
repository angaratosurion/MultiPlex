using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// This macro will embed a silverlight application.
    /// </summary>
    /// <example><code language="none">
    /// {silverlight:url=http://www.foo.com}
    /// </code></example>
    public class SilverlightMacro : IMacro
    {
        /// <summary>
        /// Gets the id of the macro.
        /// </summary>
        public string Id
        {
            get { return "Silverlight"; }
        }

        /// <summary>
        /// Gets the list of rules for the macro.
        /// </summary>
        public IList<MacroRule> Rules
        {
            get
            {
                return new List<MacroRule>
                           {
                               new MacroRule(
                                   @"(?i)(\{silverlight\:)([^\}]+)(\})",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.Silverlight},
                                           {3, ScopeName.Remove}
                                       })
                           };
            }
        }
    }
}