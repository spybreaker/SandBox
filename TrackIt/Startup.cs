using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrackIt.Startup))]
namespace TrackIt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
