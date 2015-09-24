using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// This macro will left align content.
    /// </summary>
    public class ContentLeftAlignmentMacro : IMacro
    {
        /// <summary>
        /// Gets the id of the macro.
        /// </summary>
        public string Id
        {
            get { return "ContentLeftAlignment"; }
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
                                   @"(?<!.+)(?s)(<{(?:\r?\n)?).*?(?=}<)(}<(?:\r?\n)?)(?-s)(?![<>])",
                                   new Dictionary<int, string>
                                       {
                                           { 1, ScopeName.LeftAlign },
                                           { 2, ScopeName.AlignEnd }
                                       })
                           };
            }
        }
    }
}