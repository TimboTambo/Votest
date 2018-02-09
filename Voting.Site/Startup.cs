using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Voting.Startup))]
namespace Voting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
