using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// This macro will output a table with defined headers and rows.
    /// </summary>
    /// <example><code language="none">
    /// || header 1 || header 2 ||
    /// | cell 1 | cell 2 |
    /// | cell 3 | cell 4 |
    /// </code></example>
    public class TableMacro : IMacro
    {
        /// <summary>
        /// Gets the ide of the macro.
        /// </summary>
        public string Id
        {
            get { return "Table"; }
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

                               // table headers
                               new MacroRule(@"(^\|\|)", ScopeName.TableRowHeaderBegin),
                               new MacroRule(
                                   @"(?<=^\|\|.*)(?:(\|\|\s*?$)|(?:(\|\|)[^\|\n]*($)))",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.TableRowHeaderEnd },
                                           {2, ScopeName.TableCellHeader },
                                           {3, ScopeName.TableRowHeaderEnd }
                                       }),
                               new MacroRule(@"(?<=^\|\|.*)(\|\|)", ScopeName.TableCellHeader),

                               // table cells
                               new MacroRule(@"(^\|)(?!\|)", ScopeName.TableRowBegin),
                               new MacroRule(
                                   @"(?<=^\|.*)(?:(?<!\|)(\|\s*?$)|(?:(\|)[^\|\n]*($)))",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.TableRowEnd},
                                           {2, ScopeName.TableCell},
                                           {3, ScopeName.TableRowEnd}
                                       }),
                               new MacroRule(@"(?<=^\|.*)(?:(?<!\|)(\|)(?!\|$))", ScopeName.TableCell)
                           };
            }
        }
    }
}