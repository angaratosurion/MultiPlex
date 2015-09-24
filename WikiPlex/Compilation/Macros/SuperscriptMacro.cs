using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// This macro will display text as superscript.
    /// </summary>
    /// <example><code language="none">
    /// I am normal^^but I am subscript^^
    /// </code></example>
    public class SuperscriptMacro : IMacro
    {
        /// <summary>
        /// Gets the id of the macro.
        /// </summary>
        public string Id
        {
            get { return "Superscript"; }
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
                                   @"(?-s)((?<!\^)\^\^(?!\^))(?>[^{\[\n]*?)(?>(?:{{?|\[)(?>[^}\]\n]*)(?>(?:}}?|\])*)|.)*?(?>[^{\[\n]*?)((?<!\^)\^\^(?!\^))",
                                   new Dictionary<int, string>
                                       {
                                           { 1, ScopeName.SuperscriptBegin },
                                           { 2, ScopeName.SuperscriptEnd }
                                       })
                           };
            }
        }
    }
}