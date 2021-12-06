using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UsuarioService.ViewModels;

namespace Saga.Orchestrator.Controllers
{
    [ApiController]
    [Route("saga")]
    public class SagaController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SagaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuarioViewModel usuarioViewModel)
        {
            var request = JsonConvert.SerializeObject(usuarioViewModel);
            // Validar jwt
            var usuarioClient = _httpClientFactory.CreateClient("Usuario");
            var usuarioResponse = await usuarioClient.PostAsync("/usuario", new StringContent(request, Encoding.UTF8, "application/JSON"));
            var usuarioActionResult = (int)usuarioResponse.StatusCode;
            // Se tiver cadastrado, pegar usuário
            // Se não tiver cadastrado, cadastrar usuário
            // Calcular folha pagamento
            return StatusCode(usuarioActionResult);
        }
    }
}
