using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(REMS.Startup))]
namespace REMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
