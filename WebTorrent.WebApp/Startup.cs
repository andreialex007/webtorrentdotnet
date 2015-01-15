using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebTorrent.WebApp.Startup))]

namespace WebTorrent.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
