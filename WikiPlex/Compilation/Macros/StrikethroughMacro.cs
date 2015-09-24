using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// This macro will display text with a strikethrough.
    /// </summary>
    /// <example><code language="none">
    /// --i have a line through me--
    /// </code></example>
    public class StrikethroughMacro : IMacro
    {
        /// <summary>
        /// Gets the id of the macro.
        /// </summary>
        public string Id
        {
            get { return "Strikethrough"; }
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
                               new MacroRule(EscapeRegexPatterns.FullEscape),
                               new MacroRule(
                                   @"(?-s)((?<!-)--(?!-))(?>[^{[\n]*?)(?>(?:{{?|\[)(?>[^}\]\n]*)(?>(?:}}?|\])*)|.)*?(?>[^{[\n]*?)((?<!-)--(?!-))",
                                   new Dictionary<int, string>
                                       {
                                           { 1, ScopeName.StrikethroughBegin },
                                           { 2, ScopeName.StrikethroughEnd }
                                       })
                           };
            }
        }
    }
}