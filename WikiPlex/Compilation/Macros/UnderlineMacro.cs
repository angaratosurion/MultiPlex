using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// This macro will display text as underlined.
    /// </summary>
    /// <example><code language="none">
    /// +I am underlined+
    /// </code></example>
    public class UnderlineMacro : IMacro
    {
        /// <summary>
        /// Gets the id of the macro.
        /// </summary>
        public string Id
        {
            get { return "Underline"; }
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
                               new MacroRule(@"\+\+"),
                               new MacroRule(
                                   @"(?-s)(?:(\+)(?>[^{\[\+\n]*)(?>(?:{{?|\[)(?>[^}\]\n]*)(?>(?:}}?|\])*)|.)*?(?>[^{\[\+\n]*)(\+))",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.UnderlineBegin},
                                           {2, ScopeName.UnderlineEnd}
                                       })
                           };
            }
        }
    }
}