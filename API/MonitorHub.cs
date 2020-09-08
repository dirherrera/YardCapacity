using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace API
{
    public class MonitorHub : Hub
    {

        [HubMethodName("send")]
        public static void Send(string name, string message)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MonitorHub>();
            context.Clients.All.send();
        }
    }
}