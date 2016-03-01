using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AccessLDAP.Startup))]
namespace AccessLDAP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
