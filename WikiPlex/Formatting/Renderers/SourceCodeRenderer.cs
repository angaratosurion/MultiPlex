using System;
using System.Collections.Generic;
using System.Threading;
using ColorCode;

namespace WikiPlex.Formatting.Renderers
{
    /// <summary>
    /// Will render all source code scopes.
    /// </summary>
    public class SourceCodeRenderer : Renderer
    {
        private readonly ICodeColorizer codeColorizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceCodeRenderer"/> class.
        /// </summary>
        public SourceCodeRenderer()
            : this(new CodeColorizer())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceCodeRenderer"/> class.
        /// </summary>
        /// <param name="codeColorizer">The <see cref="ICodeColorizer"/> to use for syntax highlighting.</param>
        public SourceCodeRenderer(ICodeColorizer codeColorizer)
        {
            this.codeColorizer = codeColorizer;
        }

        /// <summary>
        /// Gets the collection of scope names for this <see cref="IRenderer"/>.
        /// </summary>
        protected override ICollection<string> ScopeNames
        {
            get
            {
                return new[] {
                                ScopeName.SingleLineCode, ScopeName.MultiLineCode, ScopeName.ColorCodeAshx,
                                ScopeName.ColorCodeAspxCs, ScopeName.ColorCodeAspxVb, ScopeName.ColorCodeCpp,
                                ScopeName.ColorCodeCSharp, ScopeName.ColorCodeCss, ScopeName.ColorCodeHtml,
                                ScopeName.ColorCodeJava, ScopeName.ColorCodeJavaScript, ScopeName.ColorCodePhp,
                                ScopeName.ColorCodePowerShell, ScopeName.ColorCodeSql, ScopeName.ColorCodeVbDotNet,
                                ScopeName.ColorCodeXml, ScopeName.ColorCodeTypeScript, ScopeName.ColorCodeFSharp,
                                ScopeName.ColorCodeMarkdown, ScopeName.ColorCodeHaskell, ScopeName.ColorCodeKoka,
                             };
            }
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
            switch (scopeName)
            {
                case ScopeName.SingleLineCode:
                    return string.Format("<span class=\"codeInline\">{0}</span>", htmlEncode(input));
                case ScopeName.MultiLineCode:
                    return FormatSyntax(input, htmlEncode);
                case ScopeName.ColorCodeAshx:
                    return Colorize(input, Languages.Ashx, htmlEncode);
                case ScopeName.ColorCodeAspxCs:
                    return Colorize(input, Languages.AspxCs, htmlEncode);
                case ScopeName.ColorCodeAspxVb:
                    return Colorize(input, Languages.AspxVb, htmlEncode);
                case ScopeName.ColorCodeCpp:
                    return Colorize(input, Languages.Cpp, htmlEncode);
                case ScopeName.ColorCodeCSharp:
                    return Colorize(input, Languages.CSharp, htmlEncode);
                case ScopeName.ColorCodeHtml:
                    return Colorize(input, Languages.Html, htmlEncode);
                case ScopeName.ColorCodeJava:
                    return Colorize(input, Languages.Java, htmlEncode);
                case ScopeName.ColorCodeJavaScript:
                    return Colorize(input, Languages.JavaScript, htmlEncode);
                case ScopeName.ColorCodeTypeScript:
                    return Colorize(input, Languages.Typescript, htmlEncode);
                case ScopeName.ColorCodeFSharp:
                    return Colorize(input, Languages.FSharp, htmlEncode);
                case ScopeName.ColorCodeSql:
                    return Colorize(input, Languages.Sql, htmlEncode);
                case ScopeName.ColorCodeVbDotNet:
                    return Colorize(input, Languages.VbDotNet, htmlEncode);
                case ScopeName.ColorCodeXml:
                    return Colorize(input, Languages.Xml, htmlEncode);
                case ScopeName.ColorCodePhp:
                    return Colorize(input, Languages.Php, htmlEncode);
                case ScopeName.ColorCodeCss:
                    return Colorize(input, Languages.Css, htmlEncode);
                case ScopeName.ColorCodePowerShell:
                    return Colorize(input, Languages.PowerShell, htmlEncode);
                case ScopeName.ColorCodeMarkdown:
                    return Colorize(input, Languages.Markdown, htmlEncode);
                case ScopeName.ColorCodeHaskell:
                    return Colorize(input, Languages.Haskell, htmlEncode);
                case ScopeName.ColorCodeKoka:
                    return Colorize(input, Languages.Koka, htmlEncode);
                default:
                    return null;
            }
        }

        private static string FormatSyntax(string input, Func<string, string> htmlEncode)
        {
            if (input.EndsWith(Environment.NewLine))
                input = input.Substring(0, input.Length - Environment.NewLine.Length);
            return string.Format("<pre>{0}</pre>", htmlEncode(input));
        }

        private string Colorize(string input, ILanguage language, Func<string, string> htmlEncode)
        {
            var colorizeThread = new Thread(InvokeColorize) {IsBackground = true};
            var data = new ColorizeData {Input = input, Language = language};

            colorizeThread.Start(data);
            if (!colorizeThread.Join(5000)) // wait 5 seconds before killing it  
            {
                colorizeThread.Abort();
                data.Output = FormatSyntax(input, htmlEncode);
            }

            return data.Output;
        }

        private void InvokeColorize(object data)
        {
            var colorizeData = data as ColorizeData;
            colorizeData.Output = codeColorizer.Colorize(colorizeData.Input, colorizeData.Language);
        }

        class ColorizeData
        {
            public string Input { get; set; }
            public string Output { get; set; }
            public ILanguage Language { get; set; }
        }
    }
}