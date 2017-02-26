using System.Collections.Generic;
using WikiPlex.Compilation.Macros;

namespace WikiPlex.Parsing
{
    /// <summary>
    /// Handles augmenting the scopes for the <see cref="TableMacro"/>.
    /// </summary>
    public class TableScopeAugmenter : IScopeAugmenter
    {
        /// <summary>
        /// This will insert new, remove, or re-order scopes.
        /// </summary>
        /// <param name="macro">The current macro.</param>
        /// <param name="capturedScopes">The list of captured scopes.</param>
        /// <param name="content">The wiki content being parsed.</param>
        /// <returns>A new list of augmented scopes.</returns>
        public virtual IList<Scope> Augment(IMacro macro, IList<Scope> capturedScopes, string content)
        {
            IList<Scope> augmentedScopes = new List<Scope>();

            for (int i = 0; (i + 1) < capturedScopes.Count; i++)
            {
                Scope current = capturedScopes[i];
                Scope peek = capturedScopes[i + 1];

                if (EnsureBlockStarted(i, augmentedScopes, current))
                    continue;

                if (EnsureStartingNewBlockWithEndingRow(augmentedScopes, current, peek, content)
                    || EnsureStartingNewBlock(augmentedScopes, current, peek)
                    || EnsureEndingRow(augmentedScopes, current, peek, content))
                {
                    i++;
                    continue;
                }

                augmentedScopes.Add(current);
            }

            EnsureLastScope(capturedScopes, augmentedScopes, content);

            return augmentedScopes;
        }

        private static void EnsureLastScope(IList<Scope> capturedScopes, IList<Scope> augmentedScopes, string content)
        {
            // explicitly add the last scope as it was intentionally skipped
            Scope last = capturedScopes[capturedScopes.Count - 1];
            augmentedScopes.Add(last);

            if (last.Name != ScopeName.TableRowEnd && last.Name != ScopeName.TableRowHeaderEnd)
            {
                last = (last.Name == ScopeName.TableRowBegin || last.Name == ScopeName.TableCell)
                           ? CreateEndRowScope(last, content)
                           : CreateEndRowHeaderScope(last, content);

                augmentedScopes.Add(last);
            }

            augmentedScopes.Add(CreateEndScope(last));
        }

        private static bool EnsureEndingRow(IList<Scope> augmentedScopes, Scope current, Scope peek, string content)
        {
            if ((current.Name == ScopeName.TableCell || current.Name == ScopeName.TableCellHeader) 
                && peek.Name == ScopeName.TableRowBegin
                && (current.Index + current.Length + 2) == peek.Index)
            {
                // missing end table row, adding one
                augmentedScopes.Add(current);
                augmentedScopes.Add(current.Name == ScopeName.TableCell
                                        ? CreateEndRowScope(current, content)
                                        : CreateEndRowHeaderScope(current, content));
                augmentedScopes.Add(peek);
                return true;
            }

            return false;
        }

        private static bool EnsureStartingNewBlock(IList<Scope> augmentedScopes, Scope current, Scope peek)
        {
            if ((current.Name == ScopeName.TableRowEnd || current.Name == ScopeName.TableRowHeaderEnd)
                && (current.Index + current.Length + 1) < peek.Index)
            {
                // ending a block and starting a new block
                augmentedScopes.Add(current);
                augmentedScopes.Add(CreateEndScope(current));
                augmentedScopes.Add(CreateStartScope(peek));
                augmentedScopes.Add(peek);
                return true;
            }

            return false;
        }

        private static bool EnsureStartingNewBlockWithEndingRow(IList<Scope> augmentedScopes, Scope current, Scope peek, string content)
        {
            if ((current.Name == ScopeName.TableCell || current.Name == ScopeName.TableCellHeader)
                && (peek.Name == ScopeName.TableRowBegin || peek.Name == ScopeName.TableRowHeaderBegin)
                && (current.Index + current.Length + 2) < peek.Index)
            {
                Scope endRow = current.Name == ScopeName.TableCell
                                   ? CreateEndRowScope(current, content)
                                   : CreateEndRowHeaderScope(current, content);

                // missing end table row, and ending a block and starting a new block
                augmentedScopes.Add(current);
                augmentedScopes.Add(endRow);
                augmentedScopes.Add(CreateEndScope(endRow));
                augmentedScopes.Add(CreateStartScope(peek));
                augmentedScopes.Add(peek);
                return true;
            }

            return false;
        }

        private static bool EnsureBlockStarted(int index, IList<Scope> augmentedScopes, Scope current)
        {
            if (index != 0)
                return false;

            // this is the first item, ensure the block has started
            augmentedScopes.Add(CreateStartScope(current));
            augmentedScopes.Add(current);
            return true;
        }

        private static Scope CreateEndRowScope(Scope scope, string content)
        {
            int index = content.IndexOf('\n', scope.Index + scope.Length);

            return index > 0 
                ? new Scope(ScopeName.TableRowEnd, index, 1) 
                : new Scope(ScopeName.TableRowEnd, content.Length);
        }

        private static Scope CreateEndRowHeaderScope(Scope scope, string content)
        {
            int index = content.IndexOf('\n', scope.Index + scope.Length);

            return index > 0
                ? new Scope(ScopeName.TableRowHeaderEnd, index, 1)
                : new Scope(ScopeName.TableRowHeaderEnd, content.Length);
        }

        private static Scope CreateStartScope(Scope scope)
        {
            return new Scope(ScopeName.TableBegin, scope.Index, scope.Length);
        }

        private static Scope CreateEndScope(Scope scope)
        {
            return new Scope(ScopeName.TableEnd, scope.Index + scope.Length);
        }
    }
}