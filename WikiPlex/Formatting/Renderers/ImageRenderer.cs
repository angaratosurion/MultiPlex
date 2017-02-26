using System;
using System.Collections.Generic;
using WikiPlex.Common;

namespace WikiPlex.Formatting.Renderers
{
    /// <summary>
    /// Will render the image scopes.
    /// </summary>
    public class ImageRenderer : Renderer
    {
        private const string ImageAndLink = "<a href=\"{2}\"><img style=\"border:none;\" src=\"{3}\" {4}/></a>";
        private const string ImageAndLinkWithStyle = "<div style=\"clear:both;height:0;\">&nbsp;</div><a style=\"float:{0};{1}\" href=\"{2}\"><img style=\"border:none;\" src=\"{3}\" {4}/></a>";
        private const string ImageLinkAndAlt = "<a href=\"{2}\"><img style=\"border:none;\" src=\"{3}\" alt=\"{4}\" title=\"{4}\" {5}/></a>";
        private const string ImageLinkAndAltWithStyle = "<div style=\"clear:both;height:0;\">&nbsp;</div><a style=\"float:{0};{1}\" href=\"{2}\"><img style=\"border:none;\" src=\"{3}\" alt=\"{4}\" title=\"{4}\" {5}/></a>";
        private const string ImageNoLink = "<img src=\"{2}\" {3}/>";
        private const string ImageNoLinkAndAlt = "<img src=\"{2}\" alt=\"{3}\" title=\"{3}\" {4}/>";
        private const string ImageNoLinkAndAltWithStyle = "<div style=\"clear:both;height:0;\">&nbsp;</div><img style=\"float:{0};{1}\" src=\"{2}\" alt=\"{3}\" title=\"{3}\" {4}/>";
        private const string ImageNoLinkWithStyle = "<div style=\"clear:both;height:0;\">&nbsp;</div><img style=\"float:{0};{1}\" src=\"{2}\" {3}/>";

        /// <summary>
        /// Gets the collection of scope names for this <see cref="IRenderer"/>.
        /// </summary>
        protected override ICollection<string> ScopeNames
        {
            get
            {
                return new[] { 
                                ScopeName.ImageWithLinkNoAltLeftAlign, ScopeName.ImageWithLinkNoAltRightAlign, 
                                ScopeName.ImageWithLinkNoAlt, ScopeName.ImageWithLinkWithAltLeftAlign, 
                                ScopeName.ImageWithLinkWithAltRightAlign, ScopeName.ImageWithLinkWithAlt,
                                ScopeName.ImageLeftAlign, ScopeName.ImageRightAlign, 
                                ScopeName.ImageNoAlign, ScopeName.ImageLeftAlignWithAlt, 
                                ScopeName.ImageRightAlignWithAlt, ScopeName.ImageNoAlignWithAlt,
                                ScopeName.ImageDataWithLinkNoAltLeftAlign, ScopeName.ImageDataWithLinkNoAltRightAlign, 
                                ScopeName.ImageDataWithLinkNoAlt, ScopeName.ImageDataWithLinkWithAltLeftAlign, 
                                ScopeName.ImageDataWithLinkWithAltRightAlign, ScopeName.ImageDataWithLinkWithAlt,
                                ScopeName.ImageDataLeftAlign, ScopeName.ImageDataRightAlign, 
                                ScopeName.ImageDataNoAlign, ScopeName.ImageDataLeftAlignWithAlt, 
                                ScopeName.ImageDataRightAlignWithAlt, ScopeName.ImageDataNoAlignWithAlt 
                             };
            }
        }

        /// <summary>
        /// Gets the invalid macro error text.
        /// </summary>
        protected override string InvalidMacroError
        {
            get { return "Cannot resolve image macro, invalid number of parameters."; }
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
            FloatAlignment alignment = GetAlignment(scopeName);
            var renderMethod = GetRenderMethod(scopeName);

            return RenderException.ConvertAny(() => renderMethod(input, alignment, attributeEncode));
        }

        private static FloatAlignment GetAlignment(string scopeName)
        {
            switch (scopeName)
            {
                case ScopeName.ImageLeftAlign:
                case ScopeName.ImageLeftAlignWithAlt:
                case ScopeName.ImageWithLinkNoAltLeftAlign:
                case ScopeName.ImageWithLinkWithAltLeftAlign:
                case ScopeName.ImageDataLeftAlign:
                case ScopeName.ImageDataLeftAlignWithAlt:
                case ScopeName.ImageDataWithLinkNoAltLeftAlign:
                case ScopeName.ImageDataWithLinkWithAltLeftAlign:
                    return FloatAlignment.Left;
                case ScopeName.ImageRightAlign:
                case ScopeName.ImageRightAlignWithAlt:
                case ScopeName.ImageWithLinkNoAltRightAlign:
                case ScopeName.ImageWithLinkWithAltRightAlign:
                case ScopeName.ImageDataRightAlign:
                case ScopeName.ImageDataRightAlignWithAlt:
                case ScopeName.ImageDataWithLinkNoAltRightAlign:
                case ScopeName.ImageDataWithLinkWithAltRightAlign:
                    return FloatAlignment.Right;
                default:
                    return FloatAlignment.None;
            }
        }

        private static Func<string, FloatAlignment, Func<string, string>, string> GetRenderMethod(string scopeName)
        {
            switch (scopeName)
            {
                case ScopeName.ImageLeftAlign:
                case ScopeName.ImageRightAlign:
                case ScopeName.ImageNoAlign:
                    return RenderImageNoLinkMacro;
                case ScopeName.ImageDataLeftAlign:
                case ScopeName.ImageDataRightAlign:
                case ScopeName.ImageDataNoAlign:
                    return RenderImageDataNoLinkMacro;
                case ScopeName.ImageLeftAlignWithAlt:
                case ScopeName.ImageRightAlignWithAlt:
                case ScopeName.ImageNoAlignWithAlt:
                    return RenderImageWithAltMacro;
                case ScopeName.ImageDataLeftAlignWithAlt:
                case ScopeName.ImageDataRightAlignWithAlt:
                case ScopeName.ImageDataNoAlignWithAlt:
                    return RenderImageDataWithAltMacro;
                case ScopeName.ImageWithLinkNoAlt:
                case ScopeName.ImageWithLinkNoAltLeftAlign:
                case ScopeName.ImageWithLinkNoAltRightAlign:
                    return RenderImageWithLinkMacro;
                case ScopeName.ImageDataWithLinkNoAlt:
                case ScopeName.ImageDataWithLinkNoAltLeftAlign:
                case ScopeName.ImageDataWithLinkNoAltRightAlign:
                    return RenderImageDataWithLinkMacro;
                case ScopeName.ImageWithLinkWithAlt:
                case ScopeName.ImageWithLinkWithAltLeftAlign:
                case ScopeName.ImageWithLinkWithAltRightAlign:
                    return RenderImageWithLinkAndAltMacro;
                case ScopeName.ImageDataWithLinkWithAlt:
                case ScopeName.ImageDataWithLinkWithAltLeftAlign:
                case ScopeName.ImageDataWithLinkWithAltRightAlign:
                    return RenderImageDataWithLinkAndAltMacro;
            }

            return null;
        }

        private static string RenderImageNoLinkMacro(string input, FloatAlignment alignment, Func<string, string> encode)
        {
            string format = alignment == FloatAlignment.None ? ImageNoLink : ImageNoLinkWithStyle;
            ImagePart parts = Utility.ExtractImageParts(input, ImagePartExtras.None);

            return string.Format(format, alignment.GetStyle(), alignment.GetPadding(), encode(parts.ImageUrl), parts.Dimensions);
        }

        private static string RenderImageDataNoLinkMacro(string input, FloatAlignment alignment, Func<string, string> encode)
        {
            string format = alignment == FloatAlignment.None ? ImageNoLink : ImageNoLinkWithStyle;
            ImagePart parts = Utility.ExtractImageParts(input, ImagePartExtras.ContainsData, false);

            return string.Format(format, alignment.GetStyle(), alignment.GetPadding(), encode(parts.ImageUrl), parts.Dimensions);
        }

        private static string RenderImageWithAltMacro(string input, FloatAlignment alignment, Func<string, string> encode)
        {
            string format = alignment == FloatAlignment.None ? ImageNoLinkAndAlt : ImageNoLinkAndAltWithStyle;
            ImagePart parts = Utility.ExtractImageParts(input, ImagePartExtras.ContainsText);
            
            return string.Format(format, alignment.GetStyle(), alignment.GetPadding(), encode(parts.ImageUrl), encode(parts.Text), parts.Dimensions);
        }

        private static string RenderImageDataWithAltMacro(string input, FloatAlignment alignment, Func<string, string> encode)
        {
            string format = alignment == FloatAlignment.None ? ImageNoLinkAndAlt : ImageNoLinkAndAltWithStyle;
            ImagePart parts = Utility.ExtractImageParts(input, ImagePartExtras.ContainsText | ImagePartExtras.ContainsData, false);

            return string.Format(format, alignment.GetStyle(), alignment.GetPadding(), encode(parts.ImageUrl), encode(parts.Text), parts.Dimensions);
        }

        private static string RenderImageWithLinkMacro(string input, FloatAlignment alignment, Func<string, string> encode)
        {
            string format = alignment == FloatAlignment.None ? ImageAndLink : ImageAndLinkWithStyle;
            ImagePart parts = Utility.ExtractImageParts(input, ImagePartExtras.ContainsLink);

            return string.Format(format, alignment.GetStyle(), alignment.GetPadding(), encode(parts.LinkUrl), encode(parts.ImageUrl), parts.Dimensions);
        }

        private static string RenderImageDataWithLinkMacro(string input, FloatAlignment alignment, Func<string, string> encode)
        {
            string format = alignment == FloatAlignment.None ? ImageAndLink : ImageAndLinkWithStyle;
            ImagePart parts = Utility.ExtractImageParts(input, ImagePartExtras.ContainsLink | ImagePartExtras.ContainsData, false);

            return string.Format(format, alignment.GetStyle(), alignment.GetPadding(), encode(parts.LinkUrl), encode(parts.ImageUrl), parts.Dimensions);
        }

        private static string RenderImageWithLinkAndAltMacro(string input, FloatAlignment alignment, Func<string, string> encode)
        {
            string format = alignment == FloatAlignment.None ? ImageLinkAndAlt : ImageLinkAndAltWithStyle;
            ImagePart parts = Utility.ExtractImageParts(input, ImagePartExtras.ContainsLink | ImagePartExtras.ContainsText);

            return string.Format(format, alignment.GetStyle(), alignment.GetPadding(), encode(parts.LinkUrl), encode(parts.ImageUrl), encode(parts.Text), parts.Dimensions);
        }

        private static string RenderImageDataWithLinkAndAltMacro(string input, FloatAlignment alignment, Func<string, string> encode)
        {
            string format = alignment == FloatAlignment.None ? ImageLinkAndAlt : ImageLinkAndAltWithStyle;
            ImagePart parts = Utility.ExtractImageParts(input, ImagePartExtras.ContainsLink | ImagePartExtras.ContainsText | ImagePartExtras.ContainsData, false);

            return string.Format(format, alignment.GetStyle(), alignment.GetPadding(), encode(parts.LinkUrl), encode(parts.ImageUrl), encode(parts.Text), parts.Dimensions);
        }
    }
}