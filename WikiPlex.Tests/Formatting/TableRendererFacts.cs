using System;
using Should;
using WikiPlex.Formatting.Renderers;
using Xunit;
using Xunit.Extensions;

namespace WikiPlex.Tests.Formatting
{
    public class TableRendererFacts
    {
        public class CanExpand
        {
            [Theory]
            [InlineData(ScopeName.TableBegin)]
            [InlineData(ScopeName.TableEnd)]
            [InlineData(ScopeName.TableCell)]
            [InlineData(ScopeName.TableCellHeader)]
            [InlineData(ScopeName.TableRowBegin)]
            [InlineData(ScopeName.TableRowEnd)]
            [InlineData(ScopeName.TableRowHeaderBegin)]
            [InlineData(ScopeName.TableRowHeaderEnd)]
            public void Should_be_able_to_resolve_the_scope_name(string scopeName)
            {
                var renderer = new TableRenderer();

                bool result = renderer.CanExpand(scopeName);

                result.ShouldBeTrue();
            }
        }

        public class Expand
        {
            [Theory]
            [InlineData(ScopeName.TableBegin, "<table>")]
            [InlineData(ScopeName.TableEnd, "</table>")]
            [InlineData(ScopeName.TableCell, "</td><td>")]
            [InlineData(ScopeName.TableCellHeader, "</th><th>")]
            [InlineData(ScopeName.TableRowBegin, "<tr><td>")]
            [InlineData(ScopeName.TableRowEnd, "</td></tr>")]
            [InlineData(ScopeName.TableRowHeaderBegin, "<tr><th>")]
            [InlineData(ScopeName.TableRowHeaderEnd, "</th></tr>")]
            public void Should_resolve_the_scope_correctly(string scopeName, string expectedResult)
            {
                var renderer = new TableRenderer();

                string actualResult = renderer.Expand(scopeName, string.Empty, x => x, x => x);

                actualResult.ShouldEqual(expectedResult);
            }

            [Fact]
            public void Should_throw_ArgumentException_for_invalid_scope_name()
            {
                var renderer = new TableRenderer();

                var ex = Record.Exception(() => renderer.Expand("foo", "in", x => x, x => x)) as ArgumentException;

                ex.ShouldNotBeNull();
                ex.ParamName.ShouldEqual("scopeName");
            }
        }
    }
}