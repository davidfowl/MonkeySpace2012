using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MonkeySpace.Samples.Connections;
using MonkeySpace.Samples.Modules;

namespace MonkeySpace.Samples
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteTable.Routes.MapConnection<ChatConnection>("echo", "echo/{*operation}");            
            
            // Uncomment to use redis
            // GlobalHost.DependencyResolver.UseRedis("127.0.0.1", 6379, "", new[] { "MonkeySpace" });
        }
    }
}