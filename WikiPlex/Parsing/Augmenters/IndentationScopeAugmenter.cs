using System.Collections.Generic;
using WikiPlex.Common;
using WikiPlex.Compilation.Macros;

namespace WikiPlex.Parsing
{
    /// <summary>
    /// Handles augmenting the scopes for the <see cref="IndentationMacro" />.
    /// </summary>
    public class IndentationScopeAugmenter : IScopeAugmenter
    {
        /// <summary>
        /// This will insert new, remove, or re-order scopes.
        /// </summary>
        /// <param name="macro">The current macro.</param>
        /// <param name="capturedScopes">The list of captured scopes.</param>
        /// <param name="content">The wiki content being parsed.</param>
        /// <returns>A new list of augmented scopes.</returns>
        public IList<Scope> Augment(IMacro macro, IList<Scope> capturedScopes, string content)
        {
            var augmentedScopes = new List<Scope>(capturedScopes.Count);

            int insertAt = 0;
            for (int i = 0; i < capturedScopes.Count; i = i + 2)
            {
                Scope begin = capturedScopes[i];
                Scope end = capturedScopes[i + 1];

                string beginContent = content.Substring(begin.Index, begin.Length);
                int depth = Utility.CountChars(':', beginContent);

                for (int j = 0; j < depth; j++)
                {
                    augmentedScopes.Insert(insertAt, begin);
                    augmentedScopes.Add(end);
                }

                insertAt = augmentedScopes.Count;
            }

            return augmentedScopes;
        }
    }
}