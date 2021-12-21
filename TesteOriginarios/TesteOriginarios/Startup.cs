using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TesteOriginarios.Startup))]
namespace TesteOriginarios
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
