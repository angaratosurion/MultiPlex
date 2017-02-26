using System;
using System.Collections.Generic;

namespace WikiPlex.Formatting.Renderers
{
    /// <summary>
    /// This will render all text formatting options (bold, italics, underline, headings, strikethrough, subscript, superscript, horizontal rule and escaped markup)
    /// </summary>
    public class TextFormattingRenderer : Renderer
    {
        /// <summary>
        /// Gets the collection of scope names for this <see cref="IRenderer"/>.
        /// </summary>
        protected override ICollection<string> ScopeNames
        {
            get
            {
                return new[] {
                                ScopeName.BoldBegin, ScopeName.BoldEnd, ScopeName.ItalicsBegin, ScopeName.ItalicsEnd,
                                ScopeName.UnderlineBegin, ScopeName.UnderlineEnd, ScopeName.HeadingOneBegin,
                                ScopeName.HeadingOneEnd, ScopeName.HeadingTwoBegin, ScopeName.HeadingTwoEnd,
                                ScopeName.HeadingThreeBegin, ScopeName.HeadingThreeEnd, ScopeName.HeadingFourBegin,
                                ScopeName.HeadingFourEnd, ScopeName.HeadingFiveBegin, ScopeName.HeadingFiveEnd,
                                ScopeName.HeadingSixBegin, ScopeName.HeadingSixEnd, ScopeName.StrikethroughBegin,
                                ScopeName.StrikethroughEnd, ScopeName.SubscriptBegin, ScopeName.SubscriptEnd,
                                ScopeName.SuperscriptBegin, ScopeName.SuperscriptEnd, ScopeName.HorizontalRule,
                                ScopeName.EscapedMarkup, ScopeName.Remove
                             };
            }
        }

        /// <summary>
        /// Will expand the input into the appropriate content based on scope.
        /// </summary>
        /// <param name="scopeName">The scope name.</param>
        /// <param name="input">The input to be expanded.</param>
        /// <param name="htmlEncode">Function that will html encode the output.</param>
        /// <param name="attributeEncode">Function that will html attribute encode the output.</param>
        /// <returns>The expanded content.</returns>
        protected override string PerformExpand(string scopeName, string input, Func<string, string> htmlEncode, Func<string, string> attributeEncode)
        {
            switch (scopeName)
            {
                case ScopeName.BoldBegin:
                    return "<b>";
                case ScopeName.BoldEnd:
                    return "</b>";
                case ScopeName.ItalicsBegin:
                    return "<i>";
                case ScopeName.ItalicsEnd:
                    return "</i>";
                case ScopeName.UnderlineBegin:
                    return "<u>";
                case ScopeName.UnderlineEnd:
                    return "</u>";
                case ScopeName.HeadingOneBegin:
                    return "<h1>";
                case ScopeName.HeadingOneEnd:
                    return "</h1>\r";
                case ScopeName.HeadingTwoBegin:
                    return "<h2>";
                case ScopeName.HeadingTwoEnd:
                    return "</h2>\r";
                case ScopeName.HeadingThreeBegin:
                    return "<h3>";
                case ScopeName.HeadingThreeEnd:
                    return "</h3>\r";
                case ScopeName.HeadingFourBegin:
                    return "<h4>";
                case ScopeName.HeadingFourEnd:
                    return "</h4>\r";
                case ScopeName.HeadingFiveBegin:
                    return "<h5>";
                case ScopeName.HeadingFiveEnd:
                    return "</h5>\r";
                case ScopeName.HeadingSixBegin:
                    return "<h6>";
                case ScopeName.HeadingSixEnd:
                    return "</h6>\r";
                case ScopeName.StrikethroughBegin:
                    return "<del>";
                case ScopeName.StrikethroughEnd:
                    return "</del>";
                case ScopeName.SubscriptBegin:
                    return "<sub>";
                case ScopeName.SubscriptEnd:
                    return "</sub>";
                case ScopeName.SuperscriptBegin:
                    return "<sup>";
                case ScopeName.SuperscriptEnd:
                    return "</sup>";
                case ScopeName.HorizontalRule:
                    return "<hr />";
                case ScopeName.Remove:
                    return string.Empty;
                case ScopeName.EscapedMarkup:
                    return htmlEncode(input);
                default:
                    return null;
            }
        }
    }
}