using Microsoft.Owin;
using MultiPlex.Web;
using Owin;

[assembly: OwinStartupAttribute(typeof(Startup))]
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
