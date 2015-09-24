using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// This macro will render links.
    /// </summary>
    /// <example><code language="none">
    /// [url:http://www.foo.com]
    /// [url:Go Here|http://www.foo.com]
    /// </code></example>
    public class LinkMacro : IMacro
    {
        /// <summary>
        /// Gets the id of the macro.
        /// </summary>
        public string Id
        {
            get { return "Link"; }
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
                                   @"(?i)(\[url:mailto:)((?>[^]]+))(])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.LinkAsMailto},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?i)(\[url:)((?>[^]|]+))(])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.LinkNoText},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?i)(\[url:)((?>[^]]+))(])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.LinkWithText},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?i)(\[file:)((?>(?:https?)://[^]]+))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.LinkNoText},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?i)(\[file:)((?>[^]\|]+\|(?:https?)://[^]]+))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.LinkWithText},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?i)({anchor:)((?>[^}]+))(})",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.Anchor},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(\[#)((?>[^]]+))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.LinkToAnchor},
                                           {3, ScopeName.Remove}
                                       })
                           };
            }
        }
    }
}