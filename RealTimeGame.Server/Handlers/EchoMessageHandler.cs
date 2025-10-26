using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using RealTimeGame.Server.Routing;

namespace RealTimeGame.Server.Handlers
{
    public class EchoMessageHandler : IMessageHandler
    {
        public async Task HandleAsync(WebSocket socket, JsonElement data)
        {
            var text = data.GetProperty("text").GetString() ?? "";
            var response = Encoding.UTF8.GetBytes($"Echo-Routed: {text}");
            await socket.SendAsync(response, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
