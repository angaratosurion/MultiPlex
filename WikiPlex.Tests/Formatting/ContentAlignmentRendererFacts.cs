using System;
using Should;
using WikiPlex.Formatting.Renderers;
using Xunit;
using Xunit.Extensions;

namespace WikiPlex.Tests.Formatting
{
    public class ContentAlignmentRendererFacts
    {
        public class CanExpand
        {
            [Theory]
            [InlineData(ScopeName.AlignEnd)]
            [InlineData(ScopeName.LeftAlign)]
            [InlineData(ScopeName.RightAlign)]
            public void Should_be_able_to_expand_scope_name(string scopeName)
            {
                var renderer = new ContentAlignmentRenderer();

                bool result = renderer.CanExpand(scopeName);

                result.ShouldBeTrue();
            }
        }

        public class Expand
        {
            [Fact]
            public void Should_expand_the_alignment_end_tag_correctly()
            {
                var renderer = new ContentAlignmentRenderer();

                string result = renderer.Expand(ScopeName.AlignEnd, "in", x => x, x => x);

                result.ShouldEqual("</div><div style=\"clear:both;height:0;\">&nbsp;</div>");
            }

            [Theory]
            [InlineData(ScopeName.LeftAlign, "left")]
            [InlineData(ScopeName.RightAlign, "right")]
            public void Should_expand_the_text_alignment_correctly(string scopeName, string alignment)
            {
                var renderer = new ContentAlignmentRenderer();

                string result = renderer.Expand(scopeName, "in", x => x, x => x);

                result.ShouldEqual(string.Format("<div style=\"text-align:{0};float:{0};\">", alignment));
            }

            [Fact]
            public void Should_raise_ArgumentException_for_invalid_scope_name()
            {
                var renderer = new ContentAlignmentRenderer();

                var ex = Record.Exception(() => renderer.Expand("foo", "in", x => x, x => x)) as ArgumentException;

                ex.ShouldNotBeNull();
                ex.ParamName.ShouldEqual("scopeName");
            }
        }
    }
}