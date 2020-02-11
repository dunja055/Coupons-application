using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AplikacijaKuponi.Startup))]
namespace AplikacijaKuponi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
