using System.Collections.Generic;
using MultiPlex.Compilation;
using MultiPlex.Compilation.Macros;

namespace MultiPlex.Web.Sample.Wiki
{
    public class TitleLinkMacro : IMacro
    {
        public string Id
        {
            get { return "Title Link"; }
        }

        public IList<MacroRule> Rules
        {
            get
            {
                return new List<MacroRule>
                           {
                               new MacroRule(EscapeRegexPatterns.CurlyBraceEscape),
                               new MacroRule(@"(?i)(\[)(?!\#|[a-z]+:)((?>[^\]]+))(\])",
                                             new Dictionary<int, string>
                                                 {
                                                     {1, ScopeName.Remove},
                                                     {2, WikiScopeName.WikiLink},
                                                     {3, ScopeName.Remove}
                                                 }
                                   )
                           };
            }
        }
    }
}