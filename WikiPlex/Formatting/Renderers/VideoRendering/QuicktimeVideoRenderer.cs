namespace WikiPlex.Formatting.Renderers
{
    internal class QuickTimeVideoRenderer : PluginVideoRenderer
    {
        const string ClassIdAttributeString = "CLSID:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B";
        const string CodebaseAttributeString = "http://www.apple.com/qtactivex/qtplugin.cab";
        const string PluginsPageAttributeString = "http://www.apple.com/quicktime/download/";
        const string TypeAttributeString = "video/quicktime";

        protected override string ClassIdAttribute
        {
            get { return ClassIdAttributeString; }
        }

        protected override string CodebaseAttribute
        {
            get { return CodebaseAttributeString; }
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
            AddParameterTag("src", url);
            AddParameterTag("autoplay", "false");
        }
    }
}