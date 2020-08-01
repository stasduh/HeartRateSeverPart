using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HeartRateWeb.Startup))]
namespace HeartRateWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
