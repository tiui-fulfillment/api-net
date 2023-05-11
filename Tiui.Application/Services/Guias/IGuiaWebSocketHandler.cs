using System.Net.WebSockets;

namespace Tiui.Application.Services.websocket
{
  /// <summary>
  /// Abstracción para el manejo de las Guías
  /// </summary>
  public interface IGuiaWebSocketHandler
  {
    Task OnConnectedAsync(WebSocketConnection connection);
    Task OnDisconnectedAsync(WebSocketConnection connection);
    Task OnMessageReceivedAsync(WebSocketConnection connection, string message);
    Task StartAsync(CancellationToken cancellationToken);
    Task SendMessageAsync(WebSocket webSocket, string message);
    Task SendMessageToAllAsync(object message);
    Task SubscribeToGuiaAsync(string Folio, WebSocket webSocket);
    Task HandleMessageAsync(WebSocket webSocket, string message);
  }
}
