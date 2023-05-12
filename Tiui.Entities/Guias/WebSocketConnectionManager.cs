using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

public class WebSocketConnectionManager
{
    private readonly ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

    public WebSocket GetSocketById(string id)
    {
        return _sockets.TryGetValue(id, out WebSocket socket) ? socket : null;
    }

    public ConcurrentDictionary<string, WebSocket> GetAllSockets()
    {
        return _sockets;
    }

    public string AddSocket(WebSocket socket)
    {
        string socketId = Guid.NewGuid().ToString();
        _sockets.TryAdd(socketId, socket);
        return socketId;
    }

    public async Task RemoveSocket(string id)
    {
        if (_sockets.TryRemove(id, out WebSocket socket))
        {
            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                statusDescription: "Socket connection closed",
                cancellationToken: CancellationToken.None);
        }
    }
}
