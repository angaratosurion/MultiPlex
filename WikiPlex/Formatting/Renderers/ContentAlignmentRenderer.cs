using System;
using System.Collections.Generic;

namespace WikiPlex.Formatting.Renderers
{
    /// <summary>
    /// This will render the content alignment scopes.
    /// </summary>
    public class ContentAlignmentRenderer : Renderer
    {
        /// <summary>
        /// Gets the collection of scope names for this <see cref="IRenderer"/>.
        /// </summary>
        protected override ICollection<string> ScopeNames
        {
            get { return new[] { ScopeName.AlignEnd, ScopeName.LeftAlign, ScopeName.RightAlign }; }
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
            if (scopeName == ScopeName.AlignEnd)
                return "</div><div style=\"clear:both;height:0;\">&nbsp;</div>";
            if (scopeName == ScopeName.LeftAlign)
                return "<div style=\"text-align:left;float:left;\">";
            if (scopeName == ScopeName.RightAlign)
                return "<div style=\"text-align:right;float:right;\">";

            return null;
        }
    }
}