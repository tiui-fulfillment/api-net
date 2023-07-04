using Tiui.Application.Repository.Guias;
using Tiui.Application.Services.websocket;
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
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Tiui.Services.GuiaNotificationClientes
{
  public class GuiaNotificationClientes : IGuiaNotificationClientes
  {
    private readonly HttpClient _httpClient;
    private readonly NpgsqlConnection _connection;
    private readonly string _conectionValue;
    private readonly string _urlDropi;
    private readonly string _keyDropi;
    private readonly JsonSerializerOptions _optionsJSON;
    private readonly ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
    private readonly List<GuiaSubscription> _subscriptions;
    private readonly IConfiguration _configuration;
    public GuiaNotificationClientes(NpgsqlConnection connection, HttpClient httpClient, IConfiguration configuration)
    {
      this._configuration = configuration;
      this._httpClient = httpClient;
      this._connection = connection;
      this._conectionValue = this._configuration["ConnectionTiuiDB"];
      this._urlDropi = this._configuration["URL_DROPI"];
      this._keyDropi = this._configuration["KEY_DROPI"];
      this._optionsJSON = new JsonSerializerOptions
      {
      Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
      };
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
      // await SubscribeToGuiaAsync(folio, connection.WebSocket);
    }
    else if (message.StartsWith("unsubscribe "))
    {
      string folio = message.Substring("unsubscribe ".Length);
      //await UnsubscribeFromGuiaAsync(folio, connection.WebSocket);
    }
    else if (message.StartsWith("message "))
    {
      string folio = message.Substring("message ".Length);
      //await SendMessageToAllAsync(new { type = "message", payload = $"Mensaje de {connection}: {folio}" });
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
        guiaData.EstatusFecha = DateTime.Now;
        resMessage.Type = "update";
        resMessage.Payload = guiaData;
        // Asignar la cadena JSON a la propiedad Payload del objeto resMessage
        if (resMessage.Payload.TiuiAmigoId == 59 && resMessage.Type == "update")
        {
          try
          {
            resMessage.Payload.EstatusFecha = DateTime.Now;
            var request = new HttpRequestMessage(HttpMethod.Post, this._urlDropi);
            request.Headers.Add("tokenunicotiui", this._keyDropi);
            request.Content = new StringContent(JsonSerializer.Serialize(guiaData, _optionsJSON), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("😱😱😱😱😱😱" + responseContent + " 😱");
            response.EnsureSuccessStatusCode();

          }
          catch (System.Exception ex)
          {
            Console.WriteLine("Error: " + ex.Message);
          }
        }
      }
    };

    while (!cancellationToken.IsCancellationRequested)
    {
      await conn.WaitAsync(cancellationToken);
    }
  }
}
}
