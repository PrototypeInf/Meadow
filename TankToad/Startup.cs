using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TankToad.Startup))]
namespace TankToad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

            ConfigureAuth(app);
        }
    }
}
