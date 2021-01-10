using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeShop.Startup))]
namespace WeShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
