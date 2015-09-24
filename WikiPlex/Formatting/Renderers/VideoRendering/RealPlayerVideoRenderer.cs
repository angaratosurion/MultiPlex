namespace WikiPlex.Formatting.Renderers
{
    internal class RealPlayerVideoRenderer : PluginVideoRenderer
    {
        const string ClassIdAttributeString = "CLSID:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA";
        const string CodebaseAttributeString = "";
        const string PluginsPageAttributeString = "";
        const string TypeAttributeString = "audio/x-pn-realaudio-plugin";

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
            AddParameterTag("autostart", "false");
        }
    }
}