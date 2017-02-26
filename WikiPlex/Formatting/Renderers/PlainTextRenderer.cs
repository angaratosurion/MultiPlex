﻿using System;

namespace WikiPlex.Formatting.Renderers
{
    /// <summary>
    /// Will render all scopes as their plain text variant.
    /// </summary>
    public class PlainTextRenderer : IRenderer
    {
        /// <summary>
        /// Gets the id of a renderer.
        /// </summary>
        public string Id
        {
            get { return "PlainText"; }
        }

        /// <summary>
        /// Determines if this renderer can expand the given scope name.
        /// </summary>
        /// <param name="scopeName">The scope name to check.</param>
        /// <returns>A boolean value indicating if the renderer can or cannot expand the macro.</returns>
        public bool CanExpand(string scopeName)
        {
            return true;
        }

        /// <summary>
        /// Will expand the input into the appropriate content based on scope.
        /// </summary>
        /// <param name="scopeName">The scope name.</param>
        /// <param name="input">The input to be expanded.</param>
        /// <param name="htmlEncode">Function that will html encode the output.</param>
        /// <param name="attributeEncode">Function that will html attribute encode the output.</param>
        /// <returns>The expanded content.</returns>
        public string Expand(string scopeName, string input, Func<string, string> htmlEncode, Func<string, string> attributeEncode)
        {
            if (ShouldRenderAsEmpty(scopeName))
                return string.Empty;

            if (ShouldRenderAsSingleSpace(scopeName))
                return " ";

            if (ShouldRenderAsFriendlyText(scopeName))
            {
                string[] parts = input.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 || parts.Length == 3)
                    return parts[0];
            }

            return input;
        }

        static bool ShouldRenderAsEmpty(string scopeName)
        {
            return (scopeName == ScopeName.Remove
                    || scopeName == ScopeName.AlignEnd
                    || scopeName == ScopeName.Anchor
                    || scopeName == ScopeName.BoldBegin
                    || scopeName == ScopeName.BoldEnd
                    || scopeName == ScopeName.HeadingOneBegin
                    || scopeName == ScopeName.HeadingOneEnd
                    || scopeName == ScopeName.HeadingTwoBegin
                    || scopeName == ScopeName.HeadingTwoEnd
                    || scopeName == ScopeName.HeadingThreeBegin
                    || scopeName == ScopeName.HeadingThreeEnd
                    || scopeName == ScopeName.HeadingFourBegin
                    || scopeName == ScopeName.HeadingFourEnd
                    || scopeName == ScopeName.HeadingFiveBegin
                    || scopeName == ScopeName.HeadingFiveEnd
                    || scopeName == ScopeName.HeadingSixBegin
                    || scopeName == ScopeName.HeadingSixEnd
                    || scopeName == ScopeName.HorizontalRule
                    || scopeName == ScopeName.IndentationBegin
                    || scopeName == ScopeName.IndentationEnd
                    || scopeName == ScopeName.ItalicsBegin
                    || scopeName == ScopeName.ItalicsEnd
                    || scopeName == ScopeName.LeftAlign
                    || scopeName == ScopeName.OrderedListBeginTag
                    || scopeName == ScopeName.OrderedListEndTag
                    || scopeName == ScopeName.RightAlign
                    || scopeName == ScopeName.StrikethroughBegin
                    || scopeName == ScopeName.StrikethroughEnd
                    || scopeName == ScopeName.SubscriptBegin
                    || scopeName == ScopeName.SubscriptEnd
                    || scopeName == ScopeName.SuperscriptBegin
                    || scopeName == ScopeName.SuperscriptEnd
                    || scopeName == ScopeName.TableBegin
                    || scopeName == ScopeName.TableEnd
                    || scopeName == ScopeName.UnderlineBegin
                    || scopeName == ScopeName.UnderlineEnd
                    || scopeName == ScopeName.UnorderedListBeginTag
                    || scopeName == ScopeName.UnorderedListEndTag
                    || scopeName == ScopeName.SyndicatedFeed
                    || scopeName == ScopeName.Silverlight
                    || scopeName == ScopeName.FlashVideo
                    || scopeName == ScopeName.InvalidVideo
                    || scopeName == ScopeName.QuickTimeVideo
                    || scopeName == ScopeName.RealPlayerVideo
                    || scopeName == ScopeName.VimeoVideo
                    || scopeName == ScopeName.WindowsMediaVideo
                    || scopeName == ScopeName.YouTubeVideo
                    || scopeName == ScopeName.Channel9Video
                    || scopeName == ScopeName.ImageLeftAlign
                    || scopeName == ScopeName.ImageNoAlign
                    || scopeName == ScopeName.ImageRightAlign
                    || scopeName == ScopeName.ImageWithLinkNoAlt
                    || scopeName == ScopeName.ImageWithLinkNoAltLeftAlign
                    || scopeName == ScopeName.ImageWithLinkNoAltRightAlign
                    || scopeName == ScopeName.ImageDataLeftAlign
                    || scopeName == ScopeName.ImageDataNoAlign
                    || scopeName == ScopeName.ImageDataRightAlign
                    || scopeName == ScopeName.ImageDataWithLinkNoAlt
                    || scopeName == ScopeName.ImageDataWithLinkNoAltLeftAlign
                    || scopeName == ScopeName.ImageDataWithLinkNoAltRightAlign
                    || scopeName == ScopeName.LinkToAnchor);
        }

        static bool ShouldRenderAsSingleSpace(string scopeName)
        {
            return (scopeName == ScopeName.ListItemBegin
                    || scopeName == ScopeName.ListItemEnd
                    || scopeName == ScopeName.TableCell
                    || scopeName == ScopeName.TableCellHeader
                    || scopeName == ScopeName.TableRowBegin
                    || scopeName == ScopeName.TableRowEnd
                    || scopeName == ScopeName.TableRowHeaderBegin
                    || scopeName == ScopeName.TableRowHeaderEnd);
        }

        static bool ShouldRenderAsFriendlyText(string scopeName)
        {
            return (scopeName == ScopeName.ImageLeftAlignWithAlt
                    || scopeName == ScopeName.ImageRightAlignWithAlt
                    || scopeName == ScopeName.ImageNoAlignWithAlt
                    || scopeName == ScopeName.ImageWithLinkWithAlt
                    || scopeName == ScopeName.ImageWithLinkWithAltLeftAlign
                    || scopeName == ScopeName.ImageWithLinkWithAltRightAlign
                    || scopeName == ScopeName.ImageDataLeftAlignWithAlt
                    || scopeName == ScopeName.ImageDataRightAlignWithAlt
                    || scopeName == ScopeName.ImageDataNoAlignWithAlt
                    || scopeName == ScopeName.ImageDataWithLinkWithAlt
                    || scopeName == ScopeName.ImageDataWithLinkWithAltLeftAlign
                    || scopeName == ScopeName.ImageDataWithLinkWithAltRightAlign
                    || scopeName == ScopeName.LinkWithText);
        }
    }
}
