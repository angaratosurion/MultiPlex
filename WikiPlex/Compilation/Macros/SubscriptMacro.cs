using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// This macro will display text as subscript.
    /// </summary>
    /// <example><code language="none">
    /// I am normal,,but I am subscript,,
    /// </code></example>
    public class SubscriptMacro : IMacro
    {
        /// <summary>
        /// Gets the id of the macro.
        /// </summary>
        public string Id
        {
            get { return "Subscript"; }
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
                                   @"(?-s)((?<!,),,(?!,))(?>[^{\[\n]*?)(?>(?:{{?|\[)(?>[^}\]\n]*)(?>(?:}}?|\])*)|.)*?(?>[^{\[\n]*?)((?<!,),,(?!,))",
                                   new Dictionary<int, string>
                                       {
                                           { 1, ScopeName.SubscriptBegin },
                                           { 2, ScopeName.SubscriptEnd }
                                       })
                           };
            }
        }
    }
}