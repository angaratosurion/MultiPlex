using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MultiPlex.Core.Application.Startup))]
namespace MultiPlex.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
