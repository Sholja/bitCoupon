using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute("MVCStartup", typeof(BitCoupon.MVC.Startup))]
namespace BitCoupon.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
