using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QrWebApp.Startup))]
namespace QrWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
