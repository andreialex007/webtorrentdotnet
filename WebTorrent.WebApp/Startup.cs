using Microsoft.Owin;
using Owin;
using WebTorrent.WebApp;

[assembly: OwinStartup(typeof(Startup))]
namespace WebTorrent.WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
