using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// Will output source code rendered as plain text or as syntax highighted for certain languages.
    /// </summary>
    /// <example><code language="none">
    /// {{this is a single-line example}}
    /// {{
    /// this is a multi-line example with no syntax highlighting
    /// }}
    /// {code:aspx c#} ASPX C# {code:aspx c#}
    /// {code:aspx vb.net} ASPX VB.Net {code:aspx vb.net}
    /// {code:ashx} ASHX {code:ashx}
    /// {code:c++} C++ {code:c++}
    /// {code:c#} C# {code:c#}
    /// {code:vb.net} VB.Net {code:vb.net}
    /// {code:html} HTML {code:html}
    /// {code:sql} SQL {code:sql}
    /// {code:java} Java {code:java}
    /// {code:javascript} Javascript {code:javascript}
    /// {code:xml} XML {code:xml}
    /// {code:php} PHP {code:php}
    /// {code:css} CSS {code:css}
    /// {code:powershell} Powershell {code:powershell}
    /// {code:typescript} TypeScript {code:typescript}
    /// {code:fsharp} FSharp {code:fsharp}
    /// </code></example>
    public class SourceCodeMacro : IMacro
    {
        /// <summary>
        /// Gets the id of the macro.
        /// </summary>
        public string Id
        {
            get { return "SourceCode"; }
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
                               new MacroRule(@"(?s)(?:(?:{"").*?(?:""}))"),
                               new MacroRule(
                                   @"(?m)({{)(.*?)(}})",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.SingleLineCode},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?s)({{(?:\s+\r?\n)?)((?>(?:(?!}}|{{).)*)(?>(?:{{(?>(?:(?!}}|{{).)*)}}(?>(?:(?!}}|{{).)*))*).*?)((?:\r?\n)?}})",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.MultiLineCode},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*aspx c#\}\r?\n)(.*?)(\r?\n\{code:\s*aspx c#\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeAspxCs},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*aspx vb.net\}\r?\n)(.*?)(\r?\n\{code:\s*aspx vb.net\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeAspxVb},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*ashx\}\r?\n)(.*?)(\r?\n\{code:\s*ashx\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeAshx},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*c\+\+\}\r?\n)(.*?)(\r?\n\{code:\s*c\+\+\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeCpp},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*c#\}\r?\n)(.*?)(\r?\n\{code:\s*c#\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeCSharp},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*vb.net\}\r?\n)(.*?)(\r?\n\{code:\s*vb.net\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeVbDotNet},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*html\}\r?\n)(.*?)(\r?\n\{code:\s*html\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeHtml},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*sql\}\r?\n)(.*?)(\r?\n\{code:\s*sql\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeSql},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*java\}\r?\n)(.*?)(\r?\n\{code:\s*java\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeJava},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*javascript\}\r?\n)(.*?)(\r?\n\{code:\s*javascript\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeJavaScript},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*xml\}\r?\n)(.*?)(\r?\n\{code:\s*xml\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeXml},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*php\}\r?\n)(.*?)(\r?\n\{code:\s*php\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodePhp},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*css\}\r?\n)(.*?)(\r?\n\{code:\s*css\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeCss},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*powershell\}\r?\n)(.*?)(\r?\n\{code:\s*powershell\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodePowerShell},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*typescript\}\r?\n)(.*?)(\r?\n\{code:\s*typescript\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeTypeScript},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*fsharp\}\r?\n)(.*?)(\r?\n\{code:\s*fsharp\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeFSharp},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*markdown\}\r?\n)(.*?)(\r?\n\{code:\s*markdown\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeMarkdown},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*haskell\}\r?\n)(.*?)(\r?\n\{code:\s*haskell\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeHaskell},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:\s*koka\}\r?\n)(.*?)(\r?\n\{code:\s*koka\}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ColorCodeKoka},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?si)(\{code:[^\}]+\}\r?\n)(.*?)(\r?\n\{code:[^\}]+}(?:\r?\n)?)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.MultiLineCode},
                                           {3, ScopeName.Remove}
                                       })
                           };
            }
        }
    }
}