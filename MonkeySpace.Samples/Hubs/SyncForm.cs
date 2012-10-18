using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace MonkeySpace.Samples
{
    public class SyncForm : Hub
    {
        private static ConcurrentDictionary<string, UserData> _mapping = new ConcurrentDictionary<string, UserData>();
        private static int _id;

        public string Join()
        {
            return _mapping[Context.ConnectionId].Id;
        }

        public override Task OnConnected()
        {            
            string id = "Person_" + Interlocked.Increment(ref _id);

            var user = new UserData
            {
                Id = id
            };

            _mapping.TryAdd(Context.ConnectionId, user);

            Clients.All.updateUsers(_mapping.Count);

            return base.OnConnected();
        }

        public Task LockField(string fieldName)
        {
            UserData user;
            if (_mapping.TryGetValue(Context.ConnectionId, out user))
            {
                lock (user.OwnedFields)
                {
                    user.OwnedFields.Add(fieldName);
                }

                return Clients.Others.lockField(user.Id, fieldName);
            }

            return null;
        }

        public Task UnlockField(string fieldName, string value)
        {
            UserData user;
            if (_mapping.TryGetValue(Context.ConnectionId, out user))
            {
                lock (user.OwnedFields)
                {
                    user.OwnedFields.Remove(fieldName);
                }

                return Clients.Others.unlockField(user.Id, fieldName, value);
            }

            return null;
        }

        public override Task OnDisconnected()
        {            
            UserData user;
            if (_mapping.TryRemove(Context.ConnectionId, out user))
            {
                Clients.All.updateUsers(_mapping.Count);

                return Clients.Others.unlockFields(user.OwnedFields);
            }

            return base.OnDisconnected();
        }

        private class UserData
        {
            public string Id { get; set; }
            public HashSet<string> OwnedFields { get; private set; }

            public UserData()
            {
                OwnedFields = new HashSet<string>();
            }
        }
    }
}