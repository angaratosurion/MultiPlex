namespace WikiPlex.Formatting.Renderers
{
    internal class FlashVideoRenderer : PluginVideoRenderer
    {
        const string ClassIdAttributeString = "CLSID:D27CDB6E-AE6D-11cf-96B8-444553540000";
        const string CodebaseAttrubuteString = "http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0";
        const string PluginsPageAttributeString = "http://macromedia.com/go/getflashplayer";
        const string TypeAttributeString = "application/x-shockwave-flash";

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
            AddParameterTag("movie", url);
        }
    }
}