using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArDesignForminhas_Web.Startup))]
namespace ArDesignForminhas_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}