using System.Net.WebSockets;
using System.Text.Json;

namespace RealTimeGame.Server.Routing
{
    public interface IMessageHandler
    {
        Task HandleAsync(WebSocket socket, JsonElement data);
    }
}
