using System.Net.WebSockets;
using System.Text.Json;

namespace RealTimeGame.Server.Routing
{
    public class MessageDispatcher
    {
        private readonly Dictionary<string, IMessageHandler> _handlers = new();

        public void RegisterHandler(string type, IMessageHandler handler)
        {
            _handlers[type] = handler;
        }

        public async Task DispatchAsync(WebSocket socket, string message)
        {
            var doc = JsonDocument.Parse(message);
            var root = doc.RootElement;

            var type = root.GetProperty("type").GetString();
            var data = root.GetProperty("data");

            if (_handlers.TryGetValue(type!, out var handler))
            {
                await handler.HandleAsync(socket, data);
            }
            else
            {
                Console.WriteLine($"Unknown message type: {type}");
            }
        }
    }
}
