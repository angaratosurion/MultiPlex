using System;
using System.Collections.Generic;

namespace WikiPlex.Formatting.Renderers
{
    /// <summary>
    /// This will render the indentation scopes.
    /// </summary>
    public class IndentationRenderer : Renderer
    {
        /// <summary>
        /// Gets the collection of scope names for this <see cref="IRenderer"/>.
        /// </summary>
        protected override ICollection<string> ScopeNames
        {
            get { return new[] { ScopeName.IndentationBegin, ScopeName.IndentationEnd }; }
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
            if (scopeName == ScopeName.IndentationBegin)
                return "<blockquote>";
            if (scopeName == ScopeName.IndentationEnd)
                return "</blockquote>";

            return null;
        }
    }
}