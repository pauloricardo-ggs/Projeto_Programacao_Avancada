using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioService.ViewModels;

namespace UsuarioService.Application.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create(UsuarioViewModel usuarioViewModel)
        {
            return Ok();
        }
    }
}
