namespace Tiui.Services.Comun
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using System.Threading;
  using System.Linq;
  using System.IO;
  using Newtonsoft.Json;
  using System.Text;
  using Microsoft.Extensions.Configuration;
  using GraphQL;
  using GraphQL.Client.Http;
  using GraphQL.Client.Serializer.Newtonsoft;
  using Tiui.Application.Services.Comun;
  using Tiui.Utils.Exceptions;
  using Newtonsoft.Json.Linq;
  public class GraphqlService : IGraphqlService
  {
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    public GraphqlService(IConfiguration configuration, HttpClient httpClient)
    {
      this._configuration = configuration;
      this._httpClient = httpClient;
    }

    public async Task<string> SendGraphQlRequestAsync(string query, string variables)
    {
      var request = new HttpRequestMessage(HttpMethod.Post, _configuration["URL_GQL"]);
      var requestBody = new
      {
        query,
        variables = JObject.Parse(variables)
      };
      request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
      var response = await _httpClient.SendAsync(request);

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStringAsync();
      Console.WriteLine("🍑💙 Content:💙💙💙💙💙💙 " + responseContent);

      return responseContent;
    }
  }

}