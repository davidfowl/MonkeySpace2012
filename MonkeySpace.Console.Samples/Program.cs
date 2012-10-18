using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace MonkeySpace.ConsoleApp.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        private static async Task RunAsync()
        {
            var connection = new HubConnection("http://localhost:17318/");
            IHubProxy chat = connection.CreateProxy("Chat");

            chat.On<string>("send", Console.WriteLine);

            await connection.Start();

            string line = null;
            while ((line = Console.ReadLine()) != null)
            {
                await chat.Invoke("send", "Console: " + line);
            }
        }
    }
}
