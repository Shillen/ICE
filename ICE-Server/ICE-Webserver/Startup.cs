using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ICE_Webserver.Startup))]
namespace ICE_Webserver
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
