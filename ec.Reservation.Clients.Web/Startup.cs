using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ec.Reservation.Clients.Web.Startup))]
namespace ec.Reservation.Clients.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
