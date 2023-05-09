using System.Net.WebSockets;

public class WebSocketConnection
{
    public string ConnectionId { get; }
    public WebSocket WebSocket { get; }

    public WebSocketConnection(string connectionId, WebSocket webSocket)
    {
        ConnectionId = connectionId;
        WebSocket = webSocket;
    }
}
