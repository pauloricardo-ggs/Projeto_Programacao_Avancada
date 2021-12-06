using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UsuarioService.Business.Interfaces;
using UsuarioService.Business.Models;
using UsuarioService.ViewModels;

namespace UsuarioService.Application.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public UsuarioController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        public IActionResult Create(UsuarioViewModel usuarioViewModel)
        {
            var usuario = new Usuario(usuarioViewModel.Email, usuarioViewModel.Senha);
            var encodedToken = _jwtService.GerarJwt(usuario);

            return Ok();
        }
    }
}
