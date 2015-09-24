namespace WikiPlex.Formatting.Renderers
{
    internal class Silverlight2Renderer : BaseSilverlightRenderer
    {
        public override string DataMimeType
        {
            get { return "data:application/x-silverlight,"; }
        }

        public override string ObjectType
        {
            get { return "application/x-silverlight"; }
        }

        public override string DownloadUrl
        {
            get { return "http://go.microsoft.com/fwlink/?LinkID=124807"; }
        }
    }
}