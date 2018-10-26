using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserWebClient.Startup))]
namespace UserWebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
