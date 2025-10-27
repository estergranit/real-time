using RealTimeGame.Server.Handlers;
using RealTimeGame.Server.Routing;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/server.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var server = new HttpListener();
server.Prefixes.Add("http://localhost:5000/");
server.Start();

Log.Logger.Information("WebSocket Server started on ws://localhost:5000/");

var dispatcher = new MessageDispatcher();
dispatcher.RegisterHandler("Echo", new EchoMessageHandler());


while (true)
{
    var context = await server.GetContextAsync();

    if (context.Request.IsWebSocketRequest)
    {
        var wsContext = await context.AcceptWebSocketAsync(null);
        var socket = wsContext.WebSocket;

        Log.Logger.Information("Client connected");

        var buffer = new byte[1024 * 4];

        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                Log.Logger.Information("Client disconnected");
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", CancellationToken.None);
            }
            else
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Log.Logger.Debug($"Received: {message}");

                await dispatcher.DispatchAsync(socket, message);
            }
        }
    }
    else
    {
        context.Response.StatusCode = 400;
        context.Response.Close();
    }
}
