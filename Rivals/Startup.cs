using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rivals.Startup))]
namespace Rivals
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
