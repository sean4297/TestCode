using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuotationAppV1.Startup))]
namespace QuotationAppV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
