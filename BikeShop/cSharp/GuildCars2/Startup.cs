using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuildBikes.Startup))]
namespace GuildBikes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
