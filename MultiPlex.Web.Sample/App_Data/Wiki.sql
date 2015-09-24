USE Wiki
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Title](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Slug] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Title] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Title] ON
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (1, N'Home', N'home')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (2, N'Silverlight Support', N'silverlight-support')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (3, N'Video Support', N'video-support')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (4, N'RSS Support', N'rss-support')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (5, N'Syntax Highlighting Support', N'syntax-highlighting')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (6, N'Formatting and Layout', N'formatting-and-layout')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (7, N'Basic Text Formatting', N'basic-text-formatting')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (8, N'Headings', N'headings')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (9, N'Code Blocks', N'code-blocks')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (10, N'Escaped Markup', N'escaped-markup')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (11, N'Lists', N'lists')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (12, N'Tables', N'tables')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (13, N'Horizontal Rules', N'horizontal-rules')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (14, N'Text Alignment', N'text-alignment')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (15, N'Links', N'links')
INSERT [dbo].[Title] ([Id], [Name], [Slug]) VALUES (16, N'Images', N'images')
SET IDENTITY_INSERT [dbo].[Title] OFF
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TitleId] [int] NOT NULL,
	[Source] [nvarchar](max) NOT NULL,
	[Version] [int] NOT NULL,
	[VersionDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Content] ON
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (1, 1, N'!! Welcome to the WikiPlex Sample Application

This application showcases the WikiPlex engine. Below are links to different sections of the site showing the various macros supported.

* [Formatting and Layout]
* [Syntax Highlighting]
* [RSS Support]
* [Video Support]
* [Silverlight Support]', 1, CAST(0x00009BDB017AD902 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (2, 2, N'!! Silverlight Macro

_The Silverlight macro allows you to insert Silverlight applications into your wiki page_

!!!!! Required Parameters
|| Name || Description || Range ||
| url | absolute url to xap file | n/a |

!!!!! Optional Parameters
|| Name || Description || Range || Default ||
| height | the height of the Silverlight object | px, %, em, etc. | 200px |
| width | the width of the Silverlight object | px, %, em, etc. | 200px |
| version | the version of Silverlight to use | 2/3/4/5 | 5 |
| gpuAcceleration | enables gpu acceleration for Silverlight > 2 | true/false | false|

!!!! Initialized Parameters
If your Silverlight object requires initParams to be set, you can add arbitrary key/value pairs to the markup. They will be combined and rendered correctly at runtime.

!!!!! Source Markup
{{
{silverlight:url=http://silverlight.microsoft.com/Assets/ToolkitBanner.xap,height=280,width=880,param1=a,param2=b}
}}
!!!!! Rendered Markup
{silverlight:url=http://silverlight.microsoft.com/Assets/ToolkitBanner.xap,height=280,width=880,param1=a,param2=b}', 1, CAST(0x00009BDB017AD902 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (3, 3, N'!! Video Macro
_The video macro allows you to insert video media into your wiki page_
!!!!! Required Parameters
|| Name || Description || Range ||
| url | absolute url to video media | n/a |
| type | media type of the video | flash/quicktime/real/windows/youtube |

!!!!! Optional Parameters
|| Name || Description || Range || Default ||
| align | alignment of the video | left/center/right | center |

!!!!! Source Markup
{{
{video:url=mms://wm.microsoft.com/ms/msnse/0607/28366/CodePlexTeam_Final_MBR.wmv,type=windows}
}}
!!!!! Rendered Markup
{video:url=mms://wm.microsoft.com/ms/msnse/0607/28366/CodePlexTeam_Final_MBR.wmv,type=windows}', 1, CAST(0x00009BDB017AD902 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (4, 4, N'!! Rss Macro
_The rss macro allows for the importing of external rss feeds into your wiki page._
!!!!! Required Parameters
|| Name || Description || Range ||
| url | absolute url to rss feed | n/a |

!!!!! Optional Parameters
|| Name || Description || Range || Default ||
| max | maximum number of posts to display up to 20 | 0-20 | 20 |
| titlesOnly | show only the date and title of each post | true/false | false |

!!!!! Source Markup
{{
{rss:url=http://blogs.msdn.com/codeplex/rss.xml,max=1,titlesOnly=false}
}}
!!!!! Rendered Markup
{rss:url=http://blogs.msdn.com/codeplex/rss.xml,max=1,titlesOnly=false}', 1, CAST(0x00009BDB017AD902 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (5, 5, N'!! Syntax Highlighted Code Snippet
_Allows for a code snippet to be styled according to the language specified._

!!!!! Source Markup
{{
{code:c#}
using System;
public class HelloWorld
{
   public static void Main(params string[] args)
   {
   Console.WriteLine("Hello World!");
   }
}
{code:c#}
}}

!!!!! Rendered Markup
{code:c#}
using System;
public class HelloWorld
{
   public static void Main(params string[] args)
   {
   Console.WriteLine("Hello World!");
   }
}
{code:c#}

!!!!! Syntax Highlighting Supported Languages

Using {"{code:language}"} as shown above.

|| Language || Notes ||
| aspx c# | Use for code snippets from .aspx, .ascx, .asax, .asmx, .master, and .skin files that have embedded C# code |
| aspx vb.net | Use for code snippets from .aspx, .ascx, .asax, .asmx, .master, and .skin files that have embedded VB.NET code |
| ashx | Use for code snippets from .ashx files |
| c# | Use for C# code snippets |
| vb.net | Use for VB.NET code snippets |
| html | Use for HTML code snippets |
| sql | Use for SQL code snippets |
| javascript | Use for JavaScript code snippets |
| xml | Use for snippets from .xml, .config, .dbml, and .xsd files |
| php | Use for PHP code snippets |
| css | Use for CSS code snippets |', 1, CAST(0x00009BDB017AD902 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (6, 6, N'!! Formatting and Layout
This page provides a guide for doing simple formatting utilizing various macros.

* [Basic Text Formatting]
* [Headings]
* [Code Blocks]
* [Escaped Markup]
* [Lists]
* [Tables]

!!!! Note on block level tags
_Block level tags include headings, lists, tables, and multi-line formatted text_
* Tag must be the first thing on the line
* Closing tag must be the last thing on the line
* There should be no leading or trailing whitespace', 1, CAST(0x00009BDB017AD902 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (7, 7, N'!! Basic Formatting
_Allows for simple formatting of wiki text._

!!!!! Source Markup
{{
*bold*
_italics_
+underline+
--strikethrough--
}}

!!!!! Rendered Markup
*bold*
_italics_
+underline+
--strikethrough--', 1, CAST(0x00009BDB017AD902 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (8, 8, N'!! Headings
_Allows formatting text as a header._

!!!!! Source Markup
{{
! Heading 1
!! Heading 2
!!! Heading 3
!!!! Heading 4
!!!!! Heading 5
!!!!!! Heading 6
}}

!!!!! Rendered Markup
! Heading 1   
!! Heading 2
!!! Heading 3
!!!! Heading 4
!!!!! Heading 5
!!!!!! Heading 6', 1, CAST(0x00009BDB017AD902 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (9, 9, N'!! Code Block
_Allows for text to be formatted as code for languages in which syntax highlighting is not supported._

!!!!! Source Markup
{{
// multi-line
{{
   public static void Main()
   {
     // code goes here
   }
}}

// single line
{{ single line of text }}
}}

!!!!! Rendered Markup
// multi-line
{{
   public static void Main()
   {
     // code goes here
   }
}}

// single line
{{ single line of text }}', 1, CAST(0x00009BDB017AD902 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (10, 10, N'!! Escaped Markup
_Allows use of wiki formatting characters as literal text._

!!!!! Source Markup
{{
{"text with *unrendered* wiki markup"}
}}

!!!!! Rendered Text
{"text with *unrendered* wiki markup"}

*Note: Only use vertical (not curly) quotes to ensure that formatting will not apply.*', 1, CAST(0x00009BDB017AD902 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (11, 11, N'!! Lists
_Allows for the creation of bullted or numbered lists._

!!!!! Source Markup
{{
* Bulleted List 1
** Bulleted List 2
*** Bulleted List 3

# Numbered List 1
## Numbered List 1.1
### Numbered List 1.1.1
# Numbered List 2
## Numbered List 2.1
}}

!!!!! Rendered Markup
* Bulleted List 1
** Bulleted List 2
*** Bulleted List 3

# Numbered List 1
## Numbered List 1.1
### Numbered List 1.1.1
# Numbered List 2
## Numbered List 2.1', 1, CAST(0x00009BDB017AD902 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (12, 12, N'!! Tables
_Allows for the creation of simple table layouts._

!!!!! Source Markup
{{
|| Table Heading 1 || Table Heading 2 || Table Heading 3 ||
| Row 1 - Cell 1 | Row 1 - Cell 2 | Row 1 - Cell 3 |
| Row 2 - Cell 1 | Row 2 - Cell 2 | Row 2 - Cell 3 |
}}

!!!!! Rendered Markup
|| Table Heading 1 || Table Heading 2 || Table Heading 3 ||
| Row 1 - Cell 1 | Row 1 - Cell 2 | Row 1 - Cell 3 |
| Row 2 - Cell 1 | Row 2 - Cell 2 | Row 2 - Cell 3 |', 1, CAST(0x00009BDB017AD902 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (13, 6, N'[Home] > Formatting and Layout
!! Formatting and Layout
This page provides a guide for doing simple formatting utilizing various macros.

* [Basic Text Formatting]
* [Headings]
* [Code Blocks]
* [Escaped Markup]
* [Lists]
* [Tables]

!!!! Note on block level tags
_Block level tags include headings, lists, tables, and multi-line formatted text_
* Tag must be the first thing on the line
* Closing tag must be the last thing on the line
* There should be no leading or trailing whitespace', 2, CAST(0x00009BDB017D8227 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (14, 7, N'[Home] > [Formatting and Layout] > Basic Formatting
!! Basic Formatting
_Allows for simple formatting of wiki text._

!!!!! Source Markup
{{
*bold*
_italics_
+underline+
--strikethrough--
}}

!!!!! Rendered Markup
*bold*
_italics_
+underline+
--strikethrough--', 2, CAST(0x00009BDB017D9E63 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (15, 8, N'[Home] > [Formatting and Layout] > Headings
!! Headings
_Allows formatting text as a header._

!!!!! Source Markup
{{
! Heading 1
!! Heading 2
!!! Heading 3
!!!! Heading 4
!!!!! Heading 5
!!!!!! Heading 6
}}

!!!!! Rendered Markup
! Heading 1   
!! Heading 2
!!! Heading 3
!!!! Heading 4
!!!!! Heading 5
!!!!!! Heading 6', 2, CAST(0x00009BDB017DBA26 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (16, 9, N'[Home] > [Formatting and Layout] > Code Blocks
!! Code Block
_Allows for text to be formatted as code for languages in which syntax highlighting is not supported._

!!!!! Source Markup
{{
// multi-line
{{
   public static void Main()
   {
     // code goes here
   }
}}

// single line
{{ single line of text }}
}}

!!!!! Rendered Markup
// multi-line
{{
   public static void Main()
   {
     // code goes here
   }
}}

// single line
{{ single line of text }}', 2, CAST(0x00009BDB017DC7C0 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (17, 10, N'[Home] > [Formatting and Layout] > Escaped Markup
!! Escaped Markup
_Allows use of wiki formatting characters as literal text._

!!!!! Source Markup
{{
{"text with *unrendered* wiki markup"}
}}

!!!!! Rendered Text
{"text with *unrendered* wiki markup"}

*Note: Only use vertical (not curly) quotes to ensure that formatting will not apply.*', 2, CAST(0x00009BDB017DD5B1 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (18, 11, N'[Home] > [Formatting and Layout] > Lists
!! Lists
_Allows for the creation of bullted or numbered lists._

!!!!! Source Markup
{{
* Bulleted List 1
** Bulleted List 2
*** Bulleted List 3

# Numbered List 1
## Numbered List 1.1
### Numbered List 1.1.1
# Numbered List 2
## Numbered List 2.1
}}

!!!!! Rendered Markup
* Bulleted List 1
** Bulleted List 2
*** Bulleted List 3

# Numbered List 1
## Numbered List 1.1
### Numbered List 1.1.1
# Numbered List 2
## Numbered List 2.1', 2, CAST(0x00009BDB017DE4AC AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (19, 12, N'[Home] > [Formatting and Layout] > Tables
!! Tables
_Allows for the creation of simple table layouts._

!!!!! Source Markup
{{
|| Table Heading 1 || Table Heading 2 || Table Heading 3 ||
| Row 1 - Cell 1 | Row 1 - Cell 2 | Row 1 - Cell 3 |
| Row 2 - Cell 1 | Row 2 - Cell 2 | Row 2 - Cell 3 |
}}

!!!!! Rendered Markup
|| Table Heading 1 || Table Heading 2 || Table Heading 3 ||
| Row 1 - Cell 1 | Row 1 - Cell 2 | Row 1 - Cell 3 |
| Row 2 - Cell 1 | Row 2 - Cell 2 | Row 2 - Cell 3 |', 2, CAST(0x00009BDB017DF05B AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (20, 5, N'[Home] > Syntax Highlighting
!! Syntax Highlighted Code Snippet
_Allows for a code snippet to be styled according to the language specified._

!!!!! Source Markup
{{
{code:c#}
using System;
public class HelloWorld
{
   public static void Main(params string[] args)
   {
   Console.WriteLine("Hello World!");
   }
}
{code:c#}
}}

!!!!! Rendered Markup
{code:c#}
using System;
public class HelloWorld
{
   public static void Main(params string[] args)
   {
   Console.WriteLine("Hello World!");
   }
}
{code:c#}

!!!!! Syntax Highlighting Supported Languages

Using {"{code:language}"} as shown above.

|| Language || Notes ||
| aspx c# | Use for code snippets from .aspx, .ascx, .asax, .asmx, .master, and .skin files that have embedded C# code |
| aspx vb.net | Use for code snippets from .aspx, .ascx, .asax, .asmx, .master, and .skin files that have embedded VB.NET code |
| ashx | Use for code snippets from .ashx files |
| c# | Use for C# code snippets |
| vb.net | Use for VB.NET code snippets |
| html | Use for HTML code snippets |
| sql | Use for SQL code snippets |
| javascript | Use for JavaScript code snippets |
| xml | Use for snippets from .xml, .config, .dbml, and .xsd files |
| php | Use for PHP code snippets |
| css | Use for CSS code snippets |', 2, CAST(0x00009BDB017E23C9 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (21, 4, N'[Home] > Rss Macro
!! Rss Macro
_The rss macro allows for the importing of external rss feeds into your wiki page._
!!!!! Required Parameters
|| Name || Description || Range ||
| url | absolute url to rss feed | n/a |

!!!!! Optional Parameters
|| Name || Description || Range || Default ||
| max | maximum number of posts to display up to 20 | 0-20 | 20 |
| titlesOnly | show only the date and title of each post | true/false | false |

!!!!! Source Markup
{{
{rss:url=http://blogs.msdn.com/codeplex/rss.xml,max=1,titlesOnly=false}
}}
!!!!! Rendered Markup
{rss:url=http://blogs.msdn.com/codeplex/rss.xml,max=1,titlesOnly=false}', 2, CAST(0x00009BDB017E3422 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (22, 3, N'[Home] > Video Support
!! Video Support
_The video macro allows you to insert video media into your wiki page_
!!!!! Required Parameters
|| Name || Description || Range ||
| url | absolute url to video media | n/a |
| type | media type of the video | flash/quicktime/real/windows/youtube |

!!!!! Optional Parameters
|| Name || Description || Range || Default ||
| align | alignment of the video | left/center/right | center |

!!!!! Source Markup
{{
{video:url=mms://wm.microsoft.com/ms/msnse/0607/28366/CodePlexTeam_Final_MBR.wmv,type=windows}
}}
!!!!! Rendered Markup
{video:url=mms://wm.microsoft.com/ms/msnse/0607/28366/CodePlexTeam_Final_MBR.wmv,type=windows}', 2, CAST(0x00009BDB017E4860 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (23, 4, N'[Home] > Rss Support
!! Rss Support
_The rss macro allows for the importing of external rss feeds into your wiki page._
!!!!! Required Parameters
|| Name || Description || Range ||
| url | absolute url to rss feed | n/a |

!!!!! Optional Parameters
|| Name || Description || Range || Default ||
| max | maximum number of posts to display up to 20 | 0-20 | 20 |
| titlesOnly | show only the date and title of each post | true/false | false |

!!!!! Source Markup
{{
{rss:url=http://blogs.msdn.com/codeplex/rss.xml,max=1,titlesOnly=false}
}}
!!!!! Rendered Markup
{rss:url=http://blogs.msdn.com/codeplex/rss.xml,max=1,titlesOnly=false}', 3, CAST(0x00009BDB017E571F AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (24, 2, N'[Home] > Silverlight Support
!! Silverlight Macro

_The Silverlight macro allows you to insert Silverlight applications into your wiki page_

!!!!! Source Markup
{{
{silverlight:url=http://slkit.blob.core.windows.net/xaps/ToolkitBanner.xap,height=280,width=880}
}}
!!!!! Rendered Markup
{silverlight:url=http://slkit.blob.core.windows.net/xaps/ToolkitBanner.xap,height=280,width=880}', 2, CAST(0x00009BDB017E6BC4 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (25, 6, N'[Home] > Formatting and Layout
!! Formatting and Layout
This page provides a guide for doing simple formatting utilizing various macros.

* [Basic Text Formatting]
* [Headings]
* [Code Blocks]
* [Escaped Markup]
* [Lists]
* [Tables]
* [Horizontal Rules]

!!!! Note on block level tags
_Block level tags include headings, lists, tables, and multi-line formatted text_
* Tag must be the first thing on the line
* Closing tag must be the last thing on the line
* There should be no leading or trailing whitespace', 3, CAST(0x00009BDB017E89FB AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (26, 13, N'[Home] > [Formatting and Layout] > Horizontal Rules
!! Horizontal Rules
_Allows for a visual break in your wiki content_

*Source Markup*
{{
top content
----
bottom content
}}

*Rendered Markup*

top content
----
bottom content

*Notes:*
The horizontal rule must be the first thing on a line. Content can follow it, but when rendered - will render on a new line.', 1, CAST(0x00009BDB017F8BCC AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (27, 6, N'[Home] > Formatting and Layout
!! Formatting and Layout
This page provides a guide for doing simple formatting utilizing various macros.

* [Basic Text Formatting]
* [Text Alignment]
* [Headings]
* [Code Blocks]
* [Escaped Markup]
* [Lists]
* [Tables]
* [Horizontal Rules]

!!!! Note on block level tags
_Block level tags include headings, lists, tables, and multi-line formatted text_
* Tag must be the first thing on the line
* Closing tag must be the last thing on the line
* There should be no leading or trailing whitespace', 4, CAST(0x00009BE500F8DEEB AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (28, 14, N'[Home] > [Formatting and Layout] > Text Alignment

!! Text Alignment
_Allows for alignment of text on the page._

!!!!! Source Markup
{{
<{this is left aligned content}<
>{this is right aligned content}>
><{this is center aligned content}><
<>{this is justified aligned content}<>
}}

!!!!! Rendered Markup
<{this is left aligned content}<
>{this is right aligned content}>
><{this is center aligned content}><
<>{this is justified aligned content}<>', 1, CAST(0x00009BE500F91E8E AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (29, 14, N'[Home] > [Formatting and Layout] > Text Alignment

!! Text Alignment
_Allows for alignment of text on the page._

!!!!! Source Markup
{{
<{this is left aligned content}<
>{this is right aligned content}>
}}

!!!!! Rendered Markup
<{this is left aligned content}<
>{this is right aligned content}>', 2, CAST(0x00009C2A0106ACC0 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (30, 6, N'[Home] > Formatting and Layout
!! Formatting and Layout
This page provides a guide for doing simple formatting utilizing various macros.

* [Basic Text Formatting]
* [Links]
* [Images]
* [Text Alignment]
* [Headings]
* [Code Blocks]
* [Escaped Markup]
* [Lists]
* [Tables]
* [Horizontal Rules]

!!!! Note on block level tags
_Block level tags include headings, lists, tables, and multi-line formatted text_
* Tag must be the first thing on the line
* Closing tag must be the last thing on the line
* There should be no leading or trailing whitespace', 5, CAST(0x00009C4701825721 AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (31, 15, N'[Home] > [Formatting and Layout] > Links
!! External Links
_External links allow you to link to any url outside your project._

!!!!! Source Markup
{{
[url:http://wikiplex.codeplex.com]
[url:WikiPlex|http://wikiplex.codeplex.com]
[url:Email Someone|mailto:someone@someurl.com]
}}

!!!!! Rendered Markup
[url:http://wikiplex.codeplex.com]
[url:WikiPlex|http://wikiplex.codeplex.com]
[url:Email Someone|mailto:someone@someurl.com]

!! File Links
_File links allow you to link to files that will prompt the user to download._

!!!!! Source Markup
{{
[file:http://wikiplex.codeplex.com/SourceControl/ListDownloadableCommits.aspx#DownloadLatest]
[file:Download Latest Source|http://wikiplex.codeplex.com/SourceControl/ListDownloadableCommits.aspx#DownloadLatest]
}}

[file:http://wikiplex.codeplex.com/SourceControl/ListDownloadableCommits.aspx#DownloadLatest]
[file:Download Latest Source|http://wikiplex.codeplex.com/SourceControl/ListDownloadableCommits.aspx#DownloadLatest]

!! Anchors
_Anchors are links that allow you to link to certain content via a hashtag._

!!!!! Source Markup
{{
// create an anchor
{anchor:anchorName}

// link to an anchor
[#anchorName] 
}}

// create an anchor
{anchor:anchorName}

// link to an anchor
[#anchorName] ', 1, CAST(0x00009C470184859B AS DateTime))
INSERT [dbo].[Content] ([Id], [TitleId], [Source], [Version], [VersionDate]) VALUES (32, 16, N'[Home] > [Formatting and Layout] > Images
!! Images
_Images allow you to link to images hosted elsewhere and linked from the wiki._

!!!!! Source Markup
{{
[image:http://download.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=WikiPlex&DownloadId=74720&Build=15321]
[image:WikiPlex Logo|http://download.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=WikiPlex&DownloadId=74720&Build=15321]
}}

!!!!! Rendered Markup
[image:http://download.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=WikiPlex&DownloadId=74720&Build=15321]
[image:WikiPlex Logo|http://download.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=WikiPlex&DownloadId=74720&Build=15321]

!! Hyperlinking Images
_Images can be hyperlinked using the format below._

!!!!! Source Markup
{{
[image:<Friendly Name>|<image name or link to the image>|<URL pointed to by image>]

For example:

[image:My Image|http://www.domain.com/myimage.jpg|http://www.domain.com]
}}
!!!!! Rendered Markup
[image:WikiPlex|http://download.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=WikiPlex&DownloadId=74720&Build=15321|http://wikiplex.codeplex.com/]

!! Wrapped Content
_Images also allow you to wrap content around the left or right side of it._

!!!!! Source Markup
{{
<[image:http://download.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=WikiPlex&DownloadId=74720&Build=15321] This content will flow around the image on the right hand side.
>[image:http://download.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=WikiPlex&DownloadId=74720&Build=15321] This content will flow around the image on the left hand side.
}}

!!!!! Rendered Markup
<[image:http://download.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=WikiPlex&DownloadId=74720&Build=15321] This content will flow around the image on the right hand side.
>[image:http://download.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=WikiPlex&DownloadId=74720&Build=15321] This content will flow around the image on the left hand side.', 1, CAST(0x00009C4701871347 AS DateTime))
SET IDENTITY_INSERT [dbo].[Content] OFF
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_Version]  DEFAULT ((1)) FOR [Version]
GO
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_VersionDate]  DEFAULT (getdate()) FOR [VersionDate]
GO
ALTER TABLE [dbo].[Content]  WITH CHECK ADD  CONSTRAINT [FK_Content_Title] FOREIGN KEY([TitleId])
REFERENCES [dbo].[Title] ([Id])
GO
ALTER TABLE [dbo].[Content] CHECK CONSTRAINT [FK_Content_Title]
GO
