using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using BitCoupon.DAL.Models;
using BitCoupon.DAL.Migrations;

[assembly: OwinStartup("WebAPIStartup", typeof(BitCoupon.API.Startup))]

namespace BitCoupon.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
