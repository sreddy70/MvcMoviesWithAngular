using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcMovies.Startup))]
namespace MvcMovies
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
