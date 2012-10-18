using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace MonkeySpace.Samples.Connections
{
    public class ChatConnection : PersistentConnection
    {
        protected override Task OnReceivedAsync(IRequest request, string connectionId, string data)
        {
            return Connection.Broadcast(data);
        }
    }
}