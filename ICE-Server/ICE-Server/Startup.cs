using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ICE_Server.Startup))]

namespace ICE_Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}