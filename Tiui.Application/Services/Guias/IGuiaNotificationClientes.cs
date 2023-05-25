using System.Net.WebSockets;

namespace Tiui.Application.Services.websocket
{
  /// <summary>
  /// Abstracción para el manejo de las Guías
  /// </summary>
  public interface IGuiaNotificationClientes
  {
    Task StartAsync(CancellationToken cancellationToken);
  }
}

