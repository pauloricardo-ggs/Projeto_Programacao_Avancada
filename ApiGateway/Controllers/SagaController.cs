using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UsuarioService.Application.ViewModels;
using UsuarioService.Business.Interfaces;

namespace ApiGateway.Controllers
{
    [Route("saga")]
    public class SagaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly UserManager<IdentityUser> _userManager;

        public SagaController(IHttpClientFactory httpClientFactory,
                              IJwtAuthenticationManager jwtAuthenticationManager,
                              UserManager<IdentityUser> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("cadastrar")]
        public IActionResult CadastrarUsuario()
        {
            return View();
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarUsuario(RegisterUserViewModel registerUserViewModel)
        {
            // Converte o registerUserViewModel para um string Json
            var request = JsonConvert.SerializeObject(registerUserViewModel);
            var content = new StringContent(request, Encoding.UTF8, "application/JSON");

            // Cria o client do UsuarioService e adiciona o jwt do usuário logado ao header para autorização
            var client = _httpClientFactory.CreateClient("Usuario");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer "+ _jwtAuthenticationManager.Token);

            // Recebe o response da action Signin da controller Usuario
            var usuarioResponse = await client.PostAsync("/usuario/signin", content);
            var data = await usuarioResponse.Content.ReadAsStringAsync();

            // Retorna um ActionResult
            return StatusCode((int)usuarioResponse.StatusCode, data);
        }

        [Route("logar")]
        public IActionResult LogarUsuario()
        {
            return View();
        }

        [HttpPost("logar")]
        public async Task<IActionResult> LogarUsuario(LoginUserViewModel loginUserViewModel)
        {
            var request = JsonConvert.SerializeObject(loginUserViewModel);
            var content = new StringContent(request, Encoding.UTF8, "application/JSON");

            var client = _httpClientFactory.CreateClient("Usuario");

            var usuarioResponse = await client.PostAsync("/usuario/login", content);
            var data = await usuarioResponse.Content.ReadAsStringAsync();

            if ((int)usuarioResponse.StatusCode == 200)
            {
                var _token = await _jwtAuthenticationManager.GerarToken(loginUserViewModel.Email, _userManager);
                _jwtAuthenticationManager.AlterarToken(_token);

                return Ok(new { token = _token });
            }

            return StatusCode((int)usuarioResponse.StatusCode, data);
            // Se tiver cadastrado, pegar usuário
            // Se não tiver cadastrado, cadastrar usuário
            // Calcular folha pagamento
        }
    }
}
