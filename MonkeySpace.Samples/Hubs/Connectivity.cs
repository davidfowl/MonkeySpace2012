using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace MonkeySpace.Samples.Hubs
{
    public class Connectivity : Hub
    {
        private static ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();

        public override Task OnConnected()
        {
            _connections.TryAdd(Context.ConnectionId, null);
            return Clients.All.updateCount(_connections.Count);
        }

        public override Task OnReconnected()
        {
            _connections.GetOrAdd(Context.ConnectionId, (string)null);
            return Clients.All.updateCount(_connections.Count);
        }

        public override Task OnDisconnected()
        {
            string val;
            _connections.TryRemove(Context.ConnectionId, out val);
            return Clients.All.updateCount(_connections.Count);
        }
    }
}