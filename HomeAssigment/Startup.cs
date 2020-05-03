using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeAssigment.Startup))]
namespace HomeAssigment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
