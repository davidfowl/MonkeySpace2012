using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace MonkeySpace.Samples
{
    public class Chat : Hub
    {
        public void Send(string message)
        {
            Clients.All.send(message);
        }
    }
}