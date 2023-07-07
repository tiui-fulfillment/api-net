using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tiui.Application.DTOs.Comun;
using Tiui.Application.Services.Comun;

namespace Tiui.Api.Controllers
{
    [Route("gql")]
    [ApiController]
    public class GraphqlController : ControllerBase
    {
        private readonly IGraphqlService _graphqlService;

        public GraphqlController(IGraphqlService graphqlService)
        {
            this._graphqlService = graphqlService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLRequest request)
        {
            var response = await this._graphqlService.SendGraphQlRequestAsync(request.Query, request.Variables.ToString());
            Console.WriteLine(response);
            Ok();
            return Content(response.ToString(), "application/json");
        }

    }
}
