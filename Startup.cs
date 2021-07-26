using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopPhone.Startup))]
namespace ShopPhone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
