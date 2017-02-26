using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// This macro will render images.
    /// </summary>
    /// <example><code language="none">
    /// [image:http://www.foo.com/bar.jpg]
    /// </code></example>
    public class ImageMacro : IMacro
    {
        /// <summary>
        /// Gets the id of the macro.
        /// </summary>
        public string Id
        {
            get { return "Image"; }
        }

        /// <summary>
        /// Gets the list of rules for the macro.
        /// </summary>
        public IList<MacroRule> Rules
        {
            get
            {
                return new List<MacroRule>()
                           {
                               new MacroRule(
                                   @"(?i)(<\[image:)((?>https?://[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageWithLinkNoAltLeftAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(<\[image:)((?>data:[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageDataWithLinkNoAltLeftAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(>\[image:)((?>https?://[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageWithLinkNoAltRightAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(>\[image:)((?>data:[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageDataWithLinkNoAltRightAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(\[image:)((?>https?://[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageWithLinkNoAlt},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(\[image:)((?>data:[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageDataWithLinkNoAlt},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(<\[image:)((?>[^\]\|]*\|https?://[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageWithLinkWithAltLeftAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(<\[image:)((?>[^\]\|]*\|data:[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageDataWithLinkWithAltLeftAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(>\[image:)((?>[^\]\|]*\|https?://[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageWithLinkWithAltRightAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(>\[image:)((?>[^\]\|]*\|data:[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageDataWithLinkWithAltRightAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(\[image:)((?>[^\]\|]*\|https?://[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageWithLinkWithAlt},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(\[image:)((?>[^\]\|]*\|data:[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageDataWithLinkWithAlt},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(<\[image:)((?>https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageLeftAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(<\[image:)((?>data:[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageDataLeftAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(>\[image:)((?>https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageRightAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(>\[image:)((?>data:[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageDataRightAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(\[image:)((?>https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageNoAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(\[image:)((?>data:[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageDataNoAlign},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(<\[image:)((?>[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageLeftAlignWithAlt},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(<\[image:)((?>[^\]\|]*\|data:[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageDataLeftAlignWithAlt},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(>\[image:)((?>[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageRightAlignWithAlt},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(>\[image:)((?>[^\]\|]*\|data:[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageDataRightAlignWithAlt},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(\[image:)((?>[^\]\|]*\|https?://[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageNoAlignWithAlt},
                                           {3, ScopeName.Remove}
                                       }
                                   ),
                               new MacroRule(
                                   @"(?i)(\[image:)((?>[^\]\|]*\|data:[^\]\|]*))(\])",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.ImageDataNoAlignWithAlt},
                                           {3, ScopeName.Remove}
                                       }
                                   )
                           };
            }
        }
    }
}