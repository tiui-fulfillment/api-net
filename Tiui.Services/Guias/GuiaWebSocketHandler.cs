using Tiui.Application.Repository.Guias;
using Tiui.Application.Services.websocket;
using Tiui.Application.Repository;
using Tiui.Entities.Guias;
using Npgsql;
using System.Text;
using System.Net.WebSockets;
using System.Text.Json;
using System.Collections.Concurrent;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.Services.Guias;

namespace Tiui.Services.WebSockets
{
  /// <summary>
  /// Servicio para el manejo de los estatus de la guía
  /// </summary>
  public class GuiaWebSocketHandler : IGuiaWebSocketHandler
  {
    private readonly IGuiaRepository _guiaRepository;
    private readonly NpgsqlConnection _connection;
    private readonly IGuiaService _guiaService;
    private readonly string _conectionValue;
    private readonly ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
    private readonly List<GuiaSubscription> _subscriptions;

    public GuiaWebSocketHandler(NpgsqlConnection connection, IGuiaService guiaService, IGuiaRepository guiaRepository)
    {
      this._guiaRepository = guiaRepository;
      this._connection = connection;
      this._subscriptions = new List<GuiaSubscription>();
      this._guiaService = guiaService;
      this._conectionValue = "Host=tiui-prod.cluster-cp0tdihlsymi.us-east-1.rds.amazonaws.com;Database=TiuiDB-dev;Username=postgres;Password=Asdf1234$;";
    }

    public async Task OnConnectedAsync(WebSocketConnection connection)
    {
      Console.WriteLine($"Connection 🟥{connection.WebSocket.State}🟪");

      // Agrega la conexión a la lista de sockets activos
      _sockets.TryAdd(connection.ToString(), connection.WebSocket);

      using var conn = new NpgsqlConnection(this._conectionValue);
      await conn.OpenAsync();
      Console.WriteLine($"🟪Connected to database: {conn.State}");

      await using var cmd = new NpgsqlCommand("LISTEN guias_update;", conn);
      await cmd.ExecuteNonQueryAsync();
      Console.WriteLine($"🟪🟪🟪Subscribed to guias_update notifications");

      // Envía un mensaje de bienvenida al cliente
      var welcomeMessage = new SubscriptionMessage();
      welcomeMessage.Type = "message";
      welcomeMessage.Payload = "Bienvenido al WebSocket de Tiui";
      await SendMessageToAllAsync(welcomeMessage);

      conn.Notification += async (sender, args) =>
      {
        Console.WriteLine($"Hola 🛑🟪🟪🟪 {args.ToString()}");

        if (args.Channel == "guias_update")
        {
          Console.WriteLine($"La guía 🟥{args.Payload}🟪 ha sido actualizada");

          // Envia un mensaje WebSocket a todos los clientes conectados
          var message = new { type = "guid-update", payload = $"La guía {args.Payload} ha sido actualizada" };
          await SendMessageToAllAsync(message);

          Console.WriteLine($"Mensaje WebSocket enviado");
        }
      };

      //await conn.WaitAsync(); // No bloquea el hilo principal

      Console.WriteLine($"🟪Connected to database: {conn.State}");
    }

    public async Task OnDisconnectedAsync(WebSocketConnection connection)
    {
      // Aquí puedes agregar lógica para manejar la desconexión cuando un cliente se desconecta del WebSocket
      Console.WriteLine($"Connection 🟥{connection.WebSocket.State}🟪");
      //await connection.WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
    }

    public async Task OnMessageReceivedAsync(WebSocketConnection connection, string message)
    {
      Console.WriteLine($"Mensaje recibido: {message}");
      // Aquí puedes agregar lógica para manejar los mensajes recibidos desde el cliente

      Console.WriteLine($"Mensaje recibido de {connection}: {message}");

      // Analiza el mensaje y realiza la acción correspondiente
      if (message.StartsWith("subscribe "))
      {
        string folio = message.Substring("subscribe ".Length);
        await SubscribeToGuiaAsync(folio, connection.WebSocket);
      }
      else if (message.StartsWith("unsubscribe "))
      {
        string folio = message.Substring("unsubscribe ".Length);
        //await UnsubscribeFromGuiaAsync(folio, connection.WebSocket);
      }
      else if (message.StartsWith("message "))
      {
        string folio = message.Substring("message ".Length);
        await SendMessageToAllAsync(new { type = "message", payload = $"Mensaje de {connection}: {folio}" });
      }
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    {
      Console.WriteLine(this._sockets.Count.ToString());
      await using var conn = new NpgsqlConnection(this._conectionValue);
      await conn.OpenAsync();
      await using var cmd = new NpgsqlCommand("LISTEN guias_update;", conn);
      await cmd.ExecuteNonQueryAsync();
      conn.Notification += async (sender, args) =>
      {
        if (args.Channel == "guias_update")
        {
          Console.WriteLine($"La guía 🟥{args.Payload}🟪 ha sido actualizada");
          // Envia un mensaje WebSocket a todos los clientes conectados
          await SendMessageToAllAsync($"La guía {args.Payload} ha sido actualizada");
          Console.WriteLine($"Mensaje WebSocket enviado");
        }
      };

      while (!cancellationToken.IsCancellationRequested)
      {
        await conn.WaitAsync(cancellationToken);
      }
    }
    public async Task SendMessageToAllAsync(object message)
    {
      // Serializa el objeto en JSON
      var jsonMessage = JsonSerializer.Serialize(message);

      // Envia el mensaje a cada conexión activa en la lista de sockets
      foreach (var socket in _sockets)
      {
        if (socket.Value.State == WebSocketState.Open)
        {
          await socket.Value.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonMessage)), WebSocketMessageType.Text, true, CancellationToken.None);
        }
      }
    }

    public async Task SendMessageAsync(WebSocket webSocket, object message)
    {
      // Serializa el objeto en JSON
      var jsonMessage = JsonSerializer.Serialize(message);

      // Envia el mensaje al cliente
      await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonMessage)), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    public async Task SubscribeToGuiaAsync(string Folio, WebSocket webSocket)
    {
      // Agrega la suscripción a la lista de suscripciones
      var subscription = new GuiaSubscription { Folio = Folio, WebSocket = webSocket };
      _subscriptions.Add(subscription);

      // Envía un mensaje de confirmación al cliente
      var message = new { type = "subscribe", payload = $"Suscripción a guía {Folio} confirmada" };
      await SendMessageAsync(webSocket, message);
    }
    public async Task HandleMessageAsync(WebSocket webSocket, string message)
    {
      try
      {
        // Deserializa el mensaje a un objeto JSON
        var subscriptionMessage = JsonSerializer.Deserialize<SubscriptionMessage>(message);

        // Verifica que el objeto JSON tenga los campos Type y Payload
        if (subscriptionMessage?.Type != null && subscriptionMessage?.Payload != null)
        {
          // Aquí puedes agregar la lógica para cada tipo de mensaje
          switch (subscriptionMessage.Type)
          {
            case "suscription":
              // Lógica para el tipo de mensaje "subscribe"
              Console.WriteLine($"Mensaje recibido de tipo subscribe: {subscriptionMessage.Payload}");
              var guia = (await this._guiaRepository.Query(g => g.Folio == subscriptionMessage.Payload)).FirstOrDefault();

              if (guia == null)
              {
                // Si la guía no existe, generas un error y terminas la API
                // Envía un mensaje de bienvenida al cliente
                var resMessage = new SubscriptionMessage();
                resMessage.Type = "error";
                resMessage.Payload = $"La guía {subscriptionMessage.Payload} no existe.";
                // El objeto JSON no tiene los campos Type y Payload
                await SendMessageAsync(webSocket, resMessage);
                break;
              }
              Console.WriteLine($"Mensaje recibido de tipo subscribe: {guia.Folio}");
              break;
            case "resolver":
              Console.WriteLine($"Mensaje recibido de tipo subscribe: {subscriptionMessage.Payload}");
              Console.WriteLine($"Mensaje recibido de tipo resolver: {subscriptionMessage.Payload}");
              // Lógica para el tipo de mensaje "subscribe"
              break;
            default:
              // Mensaje desconocido
              // Envía un mensaje de bienvenida al cliente
              var welcomeMessage = new SubscriptionMessage();
              welcomeMessage.Type = "error";
              welcomeMessage.Payload = "Type no reconocido.";
              // El objeto JSON no tiene los campos Type y Payload
              await SendMessageAsync(webSocket, welcomeMessage);
              break;
          }
        }
        else
        {
          // Envía un mensaje de bienvenida al cliente
          var welcomeMessage = new SubscriptionMessage();
          welcomeMessage.Type = "error";
          welcomeMessage.Payload = "El objeto JSON recibido no tiene la estructura valida.";
          // El objeto JSON no tiene los campos Type y Payload
          await SendMessageAsync(webSocket, welcomeMessage);
        }
      }
      catch (JsonException ex)
      {
        // Si se produjo una excepción, entonces el mensaje no es un objeto JSON válido
        var welcomeMessage = new SubscriptionMessage();
        welcomeMessage.Type = "error";
        welcomeMessage.Payload = "El mensaje recibido no es un objeto JSON valido.";
        // El objeto JSON no tiene los campos Type y Payload
        await SendMessageAsync(webSocket, welcomeMessage);
        Console.WriteLine($"JsonException: {ex.Message}");
      }
    }
  }
}