using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(goatCode.Startup))]
namespace goatCode
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
