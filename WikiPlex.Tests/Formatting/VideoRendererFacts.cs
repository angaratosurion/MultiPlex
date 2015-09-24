using Should;
using WikiPlex.Formatting.Renderers;
using Xunit;
using Xunit.Extensions;

namespace WikiPlex.Tests.Formatting
{
    public class VideoRendererFacts
    {
        public class CanExpand
        {
            [Theory]
            [InlineData(ScopeName.Channel9Video)]
            [InlineData(ScopeName.FlashVideo)]
            [InlineData(ScopeName.QuickTimeVideo)]
            [InlineData(ScopeName.RealPlayerVideo)]
            [InlineData(ScopeName.VimeoVideo)]
            [InlineData(ScopeName.WindowsMediaVideo)]
            [InlineData(ScopeName.YouTubeVideo)]
            [InlineData(ScopeName.InvalidVideo)]
            public void Can_resolve_the_video_scope(string scopeName)
            {
                var renderer = new VideoRenderer();

                bool result = renderer.CanExpand(scopeName);

                result.ShouldBeTrue();
            }
        }

        public class Expand
        {
            [Fact]
            public void Should_parse_the_content_and_return_an_unresolved_macro_if_url_is_not_specified()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.FlashVideo, "foo", x => x, x => x);

                output.ShouldEqual("<span class=\"unresolved\">Cannot resolve video macro, invalid parameter 'url'.</span>");
            }

            [Fact]
            public void Should_parse_the_content_and_return_an_unresolved_macro_if_url_is_not_a_valid_url()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.FlashVideo, "url=foo", x => x, x => x);

                output.ShouldEqual("<span class=\"unresolved\">Cannot resolve video macro, invalid parameter 'url'.</span>");
            }

            [Fact]
            public void Should_parse_the_content_and_return_an_unresolved_macro_if_align_is_not_valid()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.FlashVideo, "url=http://localhost/video,type=flash,align=a", x => x, x => x);

                output.ShouldEqual("<span class=\"unresolved\">Cannot resolve video macro, invalid parameter 'align'.</span>");
            }

            [Fact]
            public void Should_parse_the_content_and_return_an_unresolved_macro_if_align_is_not_left_center_or_right()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.FlashVideo, "url=http://localhost/video,type=flash,align=justify", x => x, x => x);

                output.ShouldEqual("<span class=\"unresolved\">Cannot resolve video macro, invalid parameter 'align'.</span>");
            }

            [Fact]
            public void Should_parse_the_content_and_return_an_unresolved_macro_if_url_contains_codeplex()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.FlashVideo, "url=http://www.codeplex.com/video,type=flash", x => x, x => x);

                output.ShouldEqual("<span class=\"unresolved\">Cannot resolve video macro, invalid parameter 'url'.</span>");
            }

            [Theory]
            [InlineData("Left")]
            [InlineData("Center")]
            [InlineData("Right")]
            public void Should_parse_the_content_and_render_the_correct_alignment(string align)
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.FlashVideo, "url=http://localhost/video,type=Flash,align=" + align, x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:" + align + @"""><span class=""player""><object type=""application/x-shockwave-flash"" height=""285px"" width=""320px"" classid=""CLSID:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0""><param name=""movie"" value=""http://localhost/video""></param><embed type=""application/x-shockwave-flash"" height=""285px"" width=""320px"" src=""http://localhost/video"" pluginspage=""http://macromedia.com/go/getflashplayer"" autoplay=""false"" autostart=""false"" /></object></span><br /><span class=""external""><a href=""http://localhost/video"" target=""_blank"">Launch in another window</a></span></div>");
            }

            [Fact]
            public void Should_parse_the_content_and_render_the_Flash_video_type()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.FlashVideo, "url=http://localhost/video,type=Flash", x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:Center""><span class=""player""><object type=""application/x-shockwave-flash"" height=""285px"" width=""320px"" classid=""CLSID:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0""><param name=""movie"" value=""http://localhost/video""></param><embed type=""application/x-shockwave-flash"" height=""285px"" width=""320px"" src=""http://localhost/video"" pluginspage=""http://macromedia.com/go/getflashplayer"" autoplay=""false"" autostart=""false"" /></object></span><br /><span class=""external""><a href=""http://localhost/video"" target=""_blank"">Launch in another window</a></span></div>");
            }

            [Fact]
            public void Should_parse_the_content_and_render_the_Quicktime_video_type()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.QuickTimeVideo, "url=http://localhost/video,type=Quicktime", x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:Center""><span class=""player""><object type=""video/quicktime"" height=""285px"" width=""320px"" classid=""CLSID:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B"" codebase=""http://www.apple.com/qtactivex/qtplugin.cab""><param name=""src"" value=""http://localhost/video""></param><param name=""autoplay"" value=""false""></param><embed type=""video/quicktime"" height=""285px"" width=""320px"" src=""http://localhost/video"" pluginspage=""http://www.apple.com/quicktime/download/"" autoplay=""false"" autostart=""false"" /></object></span><br /><span class=""external""><a href=""http://localhost/video"" target=""_blank"">Launch in another window</a></span></div>");
            }

            [Fact]
            public void Should_parse_the_content_and_render_the_Real_video_type()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.RealPlayerVideo, "url=http://localhost/video,type=Real", x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:Center""><span class=""player""><object type=""audio/x-pn-realaudio-plugin"" height=""285px"" width=""320px"" classid=""CLSID:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA"" codebase=""""><param name=""src"" value=""http://localhost/video""></param><param name=""autostart"" value=""false""></param><embed type=""audio/x-pn-realaudio-plugin"" height=""285px"" width=""320px"" src=""http://localhost/video"" pluginspage="""" autoplay=""false"" autostart=""false"" /></object></span><br /><span class=""external""><a href=""http://localhost/video"" target=""_blank"">Launch in another window</a></span></div>");
            }

            [Fact]
            public void Should_parse_the_content_and_render_the_Windows_video_type()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.WindowsMediaVideo, "url=http://localhost/video,type=Windows", x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:Center""><span class=""player""><object type=""application/x-mplayer2"" height=""285px"" width=""320px"" classid=""CLSID:22D6F312-B0F6-11D0-94AB-0080C74C7E95"" codebase=""http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701""><param name=""fileName"" value=""http://localhost/video""></param><param name=""autostart"" value=""false""></param><embed type=""application/x-mplayer2"" height=""285px"" width=""320px"" src=""http://localhost/video"" pluginspage=""http://www.microsoft.com/windows/windowsmedia/download/default.asp"" autoplay=""false"" autostart=""false"" /></object></span><br /><span class=""external""><a href=""http://localhost/video"" target=""_blank"">Launch in another window</a></span></div>");
            }

            [Fact]
            public void Should_parse_the_content_and_render_the_YouTube_video_type()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.YouTubeVideo, "url=http://www.youtube.com/watch?v=1234,type=YouTube", x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:Center""><span class=""player""><object height=""285px"" width=""320px""><param name=""movie"" value=""http://www.youtube.com/v/1234""></param><param name=""wmode"" value=""transparent""></param><embed height=""285px"" width=""320px"" type=""application/x-shockwave-flash"" wmode=""transparent"" src=""http://www.youtube.com/v/1234"" /></object></span><br /><span class=""external""><a href=""http://www.youtube.com/watch?v=1234"" target=""_blank"">Launch in another window</a></span></div>");
            }

            [Fact]
            public void Should_parse_the_content_and_render_the_Vimeo_video_type()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.VimeoVideo, "url=http://vimeo.com/7195148,type=Vimeo", x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:Center""><span class=""player""><object height=""285px"" width=""320px""><param name=""movie"" value=""http://vimeo.com/moogaloop.swf?clip_id=7195148&amp;server=vimeo.com&amp;show_title=1&amp;show_byline=1&amp;show_portrait=1&amp;color=&amp;fullscreen=1&amp;autoplay=0&amp;loop=0""></param><param name=""wmode"" value=""transparent""></param><param name=""allowfullscreen"" value=""true""></param><param name=""allowscriptaccess"" value=""always""></param><embed height=""285px"" width=""320px"" type=""application/x-shockwave-flash"" wmode=""transparent"" allowfullscreen=""true"" allowscriptaccess=""always"" src=""http://vimeo.com/moogaloop.swf?clip_id=7195148&amp;server=vimeo.com&amp;show_title=1&amp;show_byline=1&amp;show_portrait=1&amp;color=&amp;fullscreen=1&amp;autoplay=0&amp;loop=0"" /></object></span><br /><span class=""external""><a href=""http://vimeo.com/7195148"" target=""_blank"">Launch in another window</a></span></div>");
            }

            [Fact]
            public void Should_parse_the_content_and_render_an_unresolved_macro_for_the_invalid_video_scope()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.InvalidVideo, "whateveritdoesntmatter", x => x, x => x);

                output.ShouldEqual("<span class=\"unresolved\">Cannot resolve video macro, invalid parameter 'type'.</span>");
            }

            [Fact]
            public void Should_parse_the_content_and_height_and_width()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.FlashVideo, "url=http://localhost/video,type=Flash,height=50px,width=250px", x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:Center""><span class=""player""><object type=""application/x-shockwave-flash"" height=""50px"" width=""250px"" classid=""CLSID:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0""><param name=""movie"" value=""http://localhost/video""></param><embed type=""application/x-shockwave-flash"" height=""50px"" width=""250px"" src=""http://localhost/video"" pluginspage=""http://macromedia.com/go/getflashplayer"" autoplay=""false"" autostart=""false"" /></object></span><br /><span class=""external""><a href=""http://localhost/video"" target=""_blank"">Launch in another window</a></span></div>");
            }

            [Fact]
            public void Should_parse_the_content_and_render_the_YouTube_with_height_and_width()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.YouTubeVideo, "url=http://www.youtube.com/watch?v=1234,type=YouTube,height=50px,width=250px", x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:Center""><span class=""player""><object height=""50px"" width=""250px""><param name=""movie"" value=""http://www.youtube.com/v/1234""></param><param name=""wmode"" value=""transparent""></param><embed height=""50px"" width=""250px"" type=""application/x-shockwave-flash"" wmode=""transparent"" src=""http://www.youtube.com/v/1234"" /></object></span><br /><span class=""external""><a href=""http://www.youtube.com/watch?v=1234"" target=""_blank"">Launch in another window</a></span></div>");
            }

            [Fact]
            public void Should_parse_the_content_and_render_the_Channel9_with_height_and_width()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.Channel9Video, "url=http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360,type=Channel9,height=50px,width=250px", x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:Center""><span class=""player""><iframe src=""http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360/player?h=50&w=250"" width=""250px"" height=""50px"" scrolling=""no"" frameborder=""0""></iframe></span><br /><span class=""external""><a href=""http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360"" target=""_blank"">Launch in another window</a></span></div>");
            }

            [Fact]
            public void Should_parse_the_content_and_render_the_Channel9_with_an_end_slash()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.Channel9Video, "url=http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360/,type=Channel9,height=50px,width=250px", x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:Center""><span class=""player""><iframe src=""http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360/player?h=50&w=250"" width=""250px"" height=""50px"" scrolling=""no"" frameborder=""0""></iframe></span><br /><span class=""external""><a href=""http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360/"" target=""_blank"">Launch in another window</a></span></div>");
            }

            [Fact]
            public void Should_parse_the_content_and_render_the_Channel9_with_ending_in_player()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.Channel9Video, "url=http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360/player,type=Channel9,height=50px,width=250px", x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:Center""><span class=""player""><iframe src=""http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360/player/?h=50&w=250"" width=""250px"" height=""50px"" scrolling=""no"" frameborder=""0""></iframe></span><br /><span class=""external""><a href=""http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360/player"" target=""_blank"">Launch in another window</a></span></div>");
                
            }

            [Fact]
            public void Should_parse_the_content_and_render_the_Channel9_with_querystring_params()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.Channel9Video, "url=http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360?foo=bar,type=Channel9,height=50px,width=250px", x => x, x => x);

                output.ShouldEqual(@"<div class=""video"" style=""text-align:Center""><span class=""player""><iframe src=""http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360/player?h=50&w=250"" width=""250px"" height=""50px"" scrolling=""no"" frameborder=""0""></iframe></span><br /><span class=""external""><a href=""http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360?foo=bar"" target=""_blank"">Launch in another window</a></span></div>");

            }

            [Fact]
            public void Should_render_unresolved_macro_when_height_for_Channel9_is_not_px()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.Channel9Video, "url=http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360/,type=Channel9,height=50em", x => x, x => x);

                output.ShouldEqual("<span class=\"unresolved\">Cannot resolve video macro, invalid parameter 'height'. Value can only be pixel based.</span>");
            }

            [Fact]
            public void Should_render_unresolved_macro_when_width_for_Channel9_is_not_px()
            {
                var renderer = new VideoRenderer();

                string output = renderer.Expand(ScopeName.Channel9Video, "url=http://channel9.msdn.com/shows/InsideXbox/Windows-7-and-your-Xbox-360/,type=Channel9,width=50em", x => x, x => x);

                output.ShouldEqual("<span class=\"unresolved\">Cannot resolve video macro, invalid parameter 'width'. Value can only be pixel based.</span>");
            }
        }
    }
}