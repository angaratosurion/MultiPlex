using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// This macro will display text as aligned to the right.
    /// </summary>
    /// <example><code language="none">
    /// >{I am right aligned}>
    /// </code></example>
    public class ContentRightAlignmentMacro : IMacro
    {
        /// <summary>
        /// Gets the id of the macro.
        /// </summary>
        public string Id
        {
            get { return "ContentRightAlignment"; }
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
                                   @"(?<!.+)(?s)(>{(?:\r?\n)?).*?(?=}>)(}>(?:\r?\n)?)(?-s)(?![<>])",
                                   new Dictionary<int, string>
                                       {
                                           { 1, ScopeName.RightAlign },
                                           { 2, ScopeName.AlignEnd }
                                       })
                           };
            }
        }
    }
}