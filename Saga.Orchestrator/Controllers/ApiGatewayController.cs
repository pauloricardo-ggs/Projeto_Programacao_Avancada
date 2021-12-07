using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Saga.Orchestrator.Business.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UsuarioService.Application.ViewModels;

namespace Saga.Orchestrator.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ApiGatewayController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ApiGatewayController(IHttpClientFactory httpClientFactory,
                                    IJwtAuthenticationManager jwtAuthenticationManager,
                                    UserManager<IdentityUser> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _userManager = userManager;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] RegisterUserVm registerUserVm)
        {
            var response = await GatewayPara("UsuarioApi", "usuario/cadastrar", registerUserVm);

            var responseStatusCode = (int)response.StatusCode;
            var data = await response.Content.ReadAsStringAsync(); 

            if (responseStatusCode == 201) return CreatedAtAction("CadastrarUsuario", null);
            return StatusCode(responseStatusCode, data);
        }

        [AllowAnonymous]
        [HttpPost("entrar")]
        public async Task<IActionResult> EntrarUsuario([FromBody] LoginUserVm loginUserVm)
        {
            var response = await GatewayPara("UsuarioApi", "usuario/entrar", loginUserVm);

            var responseStatusCode = (int)response.StatusCode;
            var data = await response.Content.ReadAsStringAsync();

            if (responseStatusCode == 200) return Ok(new { token = await GerarToken(loginUserVm.Email) });
            return StatusCode(responseStatusCode, data);
        }

        private async Task<HttpResponseMessage> GatewayPara(string apiName, string uri, object parametro)
        {
            var content = ConverterParaStringContent(parametro);
            var client = _httpClientFactory.CreateClient(apiName);
            var response = await client.PostAsync(uri, content);

            return response;
        }

        private StringContent ConverterParaStringContent(object objeto)
        {
            var request = JsonConvert.SerializeObject(objeto);
            return new StringContent(request, Encoding.UTF8, "application/JSON");
        }

        private async Task<string> GerarToken(string email)
        {
            return await _jwtAuthenticationManager.GerarToken(email, _userManager);
        }
    }
}
