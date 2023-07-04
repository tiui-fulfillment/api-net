using Tiui.Application.Repository.Guias;
using Tiui.Application.Services.websocket;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Text;
using System.Net.WebSockets;
using System.Text.Json;
using System.Collections.Concurrent;
using Tiui.Application.DTOs.Guias;
using System.Text.Encodings.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Tiui.Services.WebSockets
{
  /// <summary>
  /// Servicio para el manejo de los estatus de la guía
  /// </summary>
  public class GuiaWebSocketHandler : IGuiaWebSocketHandler
  {
    private readonly IGuiaInfoSuscriptionRepository _guiaInfoSuscriptionRepository;
    private readonly NpgsqlConnection _connection;
    private readonly string _conectionValue;
    private readonly JsonSerializerOptions _optionsJSON;
    private readonly ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
    private readonly List<GuiaSubscription> _subscriptions;
    private readonly IConfiguration _configuration;
    public GuiaWebSocketHandler(NpgsqlConnection connection, IGuiaInfoSuscriptionRepository guiaInfoSuscriptionRepositoryRepository, IConfiguration configuration)
    {
      this._configuration = configuration;
      this._guiaInfoSuscriptionRepository = guiaInfoSuscriptionRepositoryRepository;
      this._connection = connection;
      this._subscriptions = new List<GuiaSubscription>();
      Console.WriteLine($"Connection  ▶{connection.State}▶ 🟥🟥🟥🟥" + this._configuration["ConnectionTiuiDB"]);
      this._conectionValue = this._configuration["ConnectionTiuiDB"];
            this._optionsJSON = new JsonSerializerOptions
      {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,      };
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
      welcomeMessage.Payload = "Bienvenido al WebSocket de Tiui 🚚📦💪🏼";
      await SendMessageAsync(connection.WebSocket, JsonSerializer.Serialize(welcomeMessage, this._optionsJSON));
      Console.WriteLine($"🟪Connected to database: {conn.State}");
    }

    public async Task OnDisconnectedAsync(WebSocketConnection connection)
    {
      // Aquí puedes agregar lógica para manejar la desconexión cuando un cliente se desconecta del WebSocket
      Console.WriteLine($"Connection 🟥{connection.WebSocket.State}🟪");
      await connection.WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
      var message = new SubscriptionMessage();
      message.Type = "message";
      message.Payload = "Desconectado del WebSocket de Tiui 🚚📦💪🏼";
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
      await using var conn = new NpgsqlConnection(this._conectionValue);
      await conn.OpenAsync();
      await using var cmd = new NpgsqlCommand("LISTEN guias_update;", conn);
      await cmd.ExecuteNonQueryAsync();
      conn.Notification += async (sender, args) =>
      {
        if (args.Channel == "guias_update")
        {
          var guiaData = JsonSerializer.Deserialize<GuiaInfoSuscription>(args.Payload);
          var resMessage = new SubscriptionMessageGuiaInfo();
          resMessage.Type = "update";
          var jsonString = guiaData;

          // Asignar la cadena JSON a la propiedad Payload del objeto resMessage
          resMessage.Payload = jsonString;
          foreach (var subscription in _subscriptions)
          {
            if (subscription.Folio == guiaData.Folio.ToString())
            {
              await SendMessageAsync(subscription.WebSocket, JsonSerializer.Serialize(resMessage, this._optionsJSON));
            }
          }
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
      var jsonMessage = JsonSerializer.Serialize(message, this._optionsJSON);

      // Envia el mensaje a cada conexión activa en la lista de sockets
      foreach (var socket in _sockets)
      {
        if (socket.Value.State == WebSocketState.Open)
        {
          await socket.Value.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonMessage)), WebSocketMessageType.Text, true, CancellationToken.None);
        }
      }
    }

    public async Task SendMessageAsync(WebSocket webSocket, string message)
    {
      // Envia el mensaje al cliente
      await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    public async Task SubscribeToGuiaAsync(string Folio, WebSocket webSocket)
    {
      // Agrega la suscripción a la lista de suscripciones
      var subscription = new GuiaSubscription { Folio = Folio, WebSocket = webSocket };
      this._subscriptions.Add(subscription);
      // Envía un mensaje de confirmación al cliente
      var message = new { type = "subscribe", payload = $"Suscripción a guía {Folio} confirmada" };
      await SendMessageAsync(webSocket, JsonSerializer.Serialize(message, this._optionsJSON));
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
              var guia = (await this._guiaInfoSuscriptionRepository.Query(g => g.Folio == subscriptionMessage.Payload)).FirstOrDefault();

              if (guia == null)
              {
                // Si la guía no existe, generas un error y terminas la API
                // Envía un mensaje de bienvenida al cliente
                var resMessage = new SubscriptionMessage();
                resMessage.Type = "error";
                resMessage.Payload = $"La guía {subscriptionMessage.Payload} no existe.";
                // El objeto JSON no tiene los campos Type y Payload
                await SendMessageAsync(webSocket, JsonSerializer.Serialize(resMessage, this._optionsJSON));
                break;
              }
              Console.WriteLine($"Mensaje recibido de tipo subscribe: {guia.Folio}");
              //agregar suscripcion de websocketid + folio
              await SubscribeToGuiaAsync(guia.Folio, webSocket);

              break;
            case "guia":
              var guiaDto = (await this._guiaInfoSuscriptionRepository.GetGuiaInfo(subscriptionMessage.Payload));
              if (guiaDto == null)
              {
                // Si la guía no existe, generas un error y terminas la API
                // Envía un mensaje de bienvenida al cliente
                var resMessage = new SubscriptionMessage();
                resMessage.Type = "error";
                resMessage.Payload = $"La guía {subscriptionMessage.Payload} no existe.";
                // El objeto JSON no tiene los campos Type y Payload
                await SendMessageAsync(webSocket, JsonSerializer.Serialize(resMessage, this._optionsJSON));
                break;
              }
              // Obtener el arreglo JSON como una cadena
 // Obtener el arreglo JSON como una cadena
          string[] evidenciasJsonArray = guiaDto.Evidencias;

          // Deserializar cada elemento del arreglo en un objeto Evidencia
          List<Evidencia> evidencias = new List<Evidencia>();
          foreach (string evidenciaJson in evidenciasJsonArray)
          {
            Evidencia evidencia = JsonSerializer.Deserialize<Evidencia>(evidenciaJson);
            evidencias.Add(evidencia);
          }
              var _guia = new GuiaInfoSuscription
              {
                GuiaId = guiaDto.GuiaId,
                Folio = guiaDto.Folio,
                EstatusId = guiaDto.EstatusId,
                Estatus = guiaDto.Estatus,
                CantidadPaquetes = guiaDto.CantidadPaquetes,
                CobroContraEntrega = guiaDto.CobroContraEntrega,
                Comentario = guiaDto.Comentario,
                EsDevolucion = guiaDto.EsDevolucion,
                Consecutivo = guiaDto.Consecutivo,
                CostoOperativo = guiaDto.CostoOperativo,
                EsPagoContraEntrega = guiaDto.EsPagoContraEntrega,
                EstatusFecha = guiaDto.EstatusFecha,
                FechaConciliacion = guiaDto.FechaConciliacion,
                FechaEstimadaEntrega = guiaDto.FechaEstimadaEntrega,
                FechaReagendado = guiaDto.FechaReagendado,
                FolioDevolucion = guiaDto.FolioDevolucion,
                ImporteCalculoSeguro = guiaDto.ImporteCalculoSeguro,
                ImporteContraEntrega = guiaDto.ImporteContraEntrega,
                ImportePaqueteria = guiaDto.ImportePaqueteria,
                ImporteSeguroMercancia = guiaDto.ImporteSeguroMercancia,
                IntentosDeEntrega = guiaDto.IntentosDeEntrega,
                IntentosRecoleccion = guiaDto.IntentosRecoleccion,
                IVA = guiaDto.IVA,
                NombreProducto = guiaDto.NombreProducto,
                NovedadDescripcion = guiaDto.NovedadDescripcion,
                NovedadId = guiaDto.NovedadId,
                PaqueteId = guiaDto.PaqueteId,
                ProcesoCancelacion = guiaDto.ProcesoCancelacion,
                PaqueteriaId = guiaDto.PaqueteriaId,
                RequiereVerificacion = guiaDto.RequiereVerificacion,
                SubTotal = guiaDto.SubTotal,
                TieneSeguroMercancia = guiaDto.TieneSeguroMercancia,
                TipoProcesoCancelacion = guiaDto.TipoProcesoCancelacion,
                Total = guiaDto.Total,
                Destinatario = JsonSerializer.Deserialize<DireccionesGuia>(guiaDto.Destinatario),
                Remitente = JsonSerializer.Deserialize<DireccionesGuia>(guiaDto.Remitente),
                Evidencias = evidencias
          };
              // Lógica para el tipo de mensaje "subscribe"
              var resMessageConsult = new SubscriptionMessageGuiaInfo();
              resMessageConsult.Type = "guia";
              // Deserializar el objeto GuiaInfoSuscription desde la carga útil de args
              resMessageConsult.Payload = _guia;
              // Enviar Mensaje
              await SendMessageAsync(webSocket, JsonSerializer.Serialize(resMessageConsult, this._optionsJSON));
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
              await SendMessageAsync(webSocket, JsonSerializer.Serialize(welcomeMessage, this._optionsJSON));
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
          await SendMessageAsync(webSocket, JsonSerializer.Serialize(welcomeMessage, this._optionsJSON));
        }
      }
      catch (JsonException ex)
      {
        // Si se produjo una excepción, entonces el mensaje no es un objeto JSON válido
        var welcomeMessage = new SubscriptionMessage();
        welcomeMessage.Type = "error";
        welcomeMessage.Payload = "El mensaje recibido no es un objeto JSON valido.";
        // El objeto JSON no tiene los campos Type y Payload
        await SendMessageAsync(webSocket, JsonSerializer.Serialize(welcomeMessage, this._optionsJSON));
        Console.WriteLine($"JsonException: {ex.Message}");
      }
    }
  }
}