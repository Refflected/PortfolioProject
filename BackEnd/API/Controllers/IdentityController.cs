using API.DTOs.Identity;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public IdentityController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult ReadToken()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestToken([FromBody] TokenRequestDto requestDto)
        {
            var authority = _configuration["IdentityServer:Authority"];
            var tokenEndpoint = $"{authority}/connect/token";

            var client = _httpClientFactory.CreateClient();
            var tokenRequest = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "scope", requestDto.Scope }
            };
            var clientId = _configuration["IdentityServer:Clients:M2MClient:ClientId"];
            var clientSecret = _configuration["IdentityServer:Clients:M2MClient:ClientSecret"];
            client.SetBasicAuthentication(clientId, clientSecret);

            var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(tokenRequest));
            var tokenResponse = await response.Content.ReadAsStringAsync();

            return Ok(tokenResponse);
        }
    }
}
