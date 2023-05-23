using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobSearch.PL.Startup))]
namespace JobSearch.PL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
