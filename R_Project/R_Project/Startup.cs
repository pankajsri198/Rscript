using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(R_Project.Startup))]
namespace R_Project
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
