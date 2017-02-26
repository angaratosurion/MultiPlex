namespace WikiPlex.Formatting.Renderers
{
    internal class WindowsMediaPlayerVideoRenderer : PluginVideoRenderer
    {
        const string ClassIdAttributeString = "CLSID:22D6F312-B0F6-11D0-94AB-0080C74C7E95";
        const string CodebaseAttrubuteString = "http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701";
        const string PluginsPageAttributeString = "http://www.microsoft.com/windows/windowsmedia/download/default.asp";
        const string TypeAttributeString = "application/x-mplayer2";

        protected override string ClassIdAttribute
        {
            get { return ClassIdAttributeString; }
        }

        protected override string CodebaseAttribute
        {
            get { return CodebaseAttrubuteString; }
        }

        protected override string PluginsPageAttribute
        {
            get { return PluginsPageAttributeString; }
        }

        protected override string TypeAttribute
        {
            get { return TypeAttributeString; }
        }

        protected override void AddParameterTags(string url)
        {
            AddParameterTag("fileName", url);
            AddParameterTag("autostart", "false");
        }
    }
}