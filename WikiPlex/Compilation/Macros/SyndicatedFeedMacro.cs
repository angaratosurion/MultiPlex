using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// This macro will display a syndicated feed.
    /// </summary>
    /// <example><code language="none">
    /// {rss:url=http://www.foo.com/rss}
    /// {atom:url=http://www.foo.com/atom}
    /// {feed:url=http://www.foo.com/rss}
    /// </code></example>
    public class SyndicatedFeedMacro : IMacro
    {
        /// <summary>
        /// Gets the id of the macro.
        /// </summary>
        public string Id
        {
            get { return "Syndicated Feed"; }
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
                                   @"(?i)(\{(?:rss|atom|feed)\:)([^\}]+)(\})",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.SyndicatedFeed},
                                           {3, ScopeName.Remove}
                                       })
                           };
            }
        }
    }
}