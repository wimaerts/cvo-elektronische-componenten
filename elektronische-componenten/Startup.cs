using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(elektronische_componenten.Startup))]
namespace elektronische_componenten
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
