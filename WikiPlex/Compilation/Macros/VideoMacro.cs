using System.Collections.Generic;

namespace WikiPlex.Compilation.Macros
{
    /// <summary>
    /// This macro will embed a video.
    /// </summary>
    /// <example><code language="none">
    /// {video:url=http://www.foo.com,type=Flash}
    /// {video:url=http://www.foo.com,type=QuickTime}
    /// {video:url=http://www.foo.com,type=Real}
    /// {video:url=http://www.foo.com,type=Windows}
    /// {video:url=http://www.foo.com,type=YouTube}
    /// {video:url=http://www.foo.com,type=Channel9}
    /// {video:url=http://www.foo.com,type=Vimeo}
    /// </code></example>
    public class VideoMacro : IMacro
    {
        /// <summary>
        /// Gets the id of the macro.
        /// </summary>
        public string Id
        {
            get { return "Video"; }
        }

        /// <summary>
        /// Gets the list of rules for the macro.
        /// </summary>
        public IList<MacroRule> Rules
        {
            get
            {
                return new List<MacroRule>
                           {
                               new MacroRule(
                                   @"(?i)(\{video\:)([^\}]*type=Flash[^\}]*)(\})",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.FlashVideo},
                                           {3, ScopeName.Remove}
                                       }),
                                new MacroRule(
                                   @"(?i)(\{video\:)([^\}]*type=QuickTime[^\}]*)(\})",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.QuickTimeVideo},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?i)(\{video\:)([^\}]*type=Real[^\}]*)(\})",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.RealPlayerVideo},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?i)(\{video\:)([^\}]*type=Windows[^\}]*)(\})",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.WindowsMediaVideo},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?i)(\{video\:)([^\}]*type=YouTube[^\}]*)(\})",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Remove},
                                           {2, ScopeName.YouTubeVideo},
                                           {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?i)(\{video\:)([^\}]*type=(?:C9|Channel9)[^\}]*)(\})",
                                   new Dictionary<int, string>
                                       {
                                          {1, ScopeName.Remove},
                                          {2, ScopeName.Channel9Video},
                                          {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?i)(\{video\:)([^\}]*type=Vimeo[^\}]*)(\})",
                                   new Dictionary<int, string>
                                       {
                                          {1, ScopeName.Remove},
                                          {2, ScopeName.VimeoVideo},
                                          {3, ScopeName.Remove}
                                       }),
                               new MacroRule(
                                   @"(?i)\{video\:[^\}]*[^\}]*\}",
                                   new Dictionary<int, string>
                                       {
                                           {0, ScopeName.InvalidVideo}
                                       })
                           };
            }
        }
    }
}